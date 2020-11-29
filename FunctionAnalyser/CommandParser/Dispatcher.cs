﻿using CommandParser.Context;
using CommandParser.Results;
using CommandParser.Tree;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommandParser
{
    public class Dispatcher
    {
        private const char ARGUMENT_SEPARATOR = ' ';
        [JsonProperty("root")]
        private readonly RootNode Root;
        [JsonProperty("name")]
        private readonly string Name;

        public Dispatcher(string name)
        {
            Root = new RootNode();
            Name = name;
        }

        public string GetName()
        {
            return Name;
        }

        public RootNode GetRoot()
        {
            return Root;
        }

        public void Register(LiteralNode contents)
        {
            Root.AddChild(contents);
        }

        public static Dispatcher FromJson(string json)
        {
            return JsonConvert.DeserializeObject<Dispatcher>(json);
        }

        public CommandResults Parse(string command)
        {
            StringReader reader = new StringReader(command);
            CommandContext contextBuilder = new CommandContext(reader.Cursor);
            ParseResults parseResults = ParseNodes(Root, reader, contextBuilder);

            if (parseResults.Reader.CanRead())
            {
                if (parseResults.Errors.Count > 0)
                {
                    return new CommandResults(false, parseResults.Errors[^1], parseResults.Context.Results);
                } else if (parseResults.Context.Range.IsEmpty())
                {
                    return new CommandResults(false, CommandError.UnknownOrIncompleteCommand().WithContext(parseResults.Reader), parseResults.Context.Results);
                } else
                {
                    return new CommandResults(false, CommandError.IncorrectArgument().WithContext(parseResults.Reader), parseResults.Context.Results);
                }
            }

            if (parseResults.Context.Executable)
            {
                if (parseResults.Errors.Count > 0)
                {
                    return new CommandResults(false, parseResults.Errors[^1], parseResults.Context.Results);
                }
            } else
            {
                return new CommandResults(false, CommandError.UnknownOrIncompleteCommand().WithContext(parseResults.Reader), parseResults.Context.Results);
            }

            return new CommandResults(true, null, parseResults.Context.Results);
        }

        private ParseResults ParseNodes(Node node, StringReader originalReader, CommandContext contextSoFar)
        {
            List<CommandError> errors = null;
            List<ParseResults> potentials = null;

            foreach (Node child in node.GetRelevantNodes(originalReader))
            {
                CommandContext context = contextSoFar.Copy();
                StringReader reader = originalReader.Copy();

                ReadResults readResults = child.Parse(reader, context);

                if (readResults.Successful)
                {
                    if (reader.CanRead())
                    {
                        if (reader.Peek() != ARGUMENT_SEPARATOR)
                        {
                            readResults = new ReadResults(false, CommandError.ExpectedArgumentSeparator().WithContext(reader));
                        }
                    }
                }

                if (!readResults.Successful)
                {
                    if (errors == null)
                    {
                        errors = new List<CommandError>();
                    }

                    errors.Add(readResults.Error);
                    continue;
                }

                context.Executes(child.GetExecutable());
                if (reader.CanRead(child.GetRedirect() == null ? 2 : 1))
                {
                    reader.Skip();

                    if (child.GetRedirect() != null)
                    {
                        Node redirectedNode = Root;
                        for (int i = 0; i < child.GetRedirect().Length; i++)
                        {
                            redirectedNode = redirectedNode.Children[child.GetRedirect()[i]];
                        }
                        return ParseNodes(redirectedNode, reader, context.Copy());
                    } else
                    {
                        ParseResults parseResults = ParseNodes(child, reader, context);
                        if (potentials == null)
                        {
                            potentials = new List<ParseResults>(1);
                        }
                        potentials.Add(parseResults);
                    }
                } else
                {
                    if (potentials == null)
                    {
                        potentials = new List<ParseResults>(1);
                    }
                    potentials.Add(new ParseResults(context, reader, new List<CommandError>()));
                }
            }

            if (potentials != null)
            {
                if (potentials.Count > 1)
                {
                    potentials.Sort((a, b) =>
                    {
                        if (!a.Reader.CanRead() && b.Reader.CanRead())
                        {
                            return -1;
                        }

                        if (a.Reader.CanRead() && !b.Reader.CanRead())
                        {
                            return 1;
                        }

                        if (a.Errors.Count == 0 && b.Errors.Count > 0)
                        {
                            return -1;
                        }

                        if (a.Errors.Count > 0 && b.Errors.Count == 0)
                        {
                            return 1;
                        }

                        return 0;
                    });
                }
                return potentials[0];
            }

            return new ParseResults(contextSoFar, originalReader, errors ?? new List<CommandError>());
        }
    }
}