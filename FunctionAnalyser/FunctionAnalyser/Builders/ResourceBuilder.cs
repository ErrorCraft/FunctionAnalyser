using AdvancedText;
using static FunctionAnalyser.MessageProvider;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CommandParser;
using FunctionAnalyser.Builders.Collections;
using FunctionAnalyser.Builders.Versions;

namespace FunctionAnalyser.Builders
{
    public class ResourceBuilder
    {
        private readonly HttpClient Client;
        private readonly ILogger Logger;

        public ResourceBuilder(ILogger logger)
        {
            Client = new HttpClient()
            {
                BaseAddress = new Uri("https://api.github.com/repos/ErrorCraft/FunctionAnalyser/contents/resources/")
            };
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");
            Logger = logger;
        }

        public async Task<Dictionary<string, Dispatcher>> GetResources()
        {
            DispatcherResourcesBuilder resources = new DispatcherResourcesBuilder()
            {
                Anchors = await GetResources<AnchorsBuilder>("anchors"),
                Blocks = await GetResources<BlocksBuilder>("blocks"),
                Colours = await GetResources<ColoursBuilder>("colours"),
                Commands = await GetResources<CommandsBuilder>("commands"),
                Components = await GetResources<ComponentsBuilder>("components"),
                Enchantments = await GetResources<EnchantmentsBuilder>("enchantments"),
                Entities = await GetResources<EntitiesBuilder>("entities"),
                Gamemodes = await GetResources<GamemodesBuilder>("gamemodes"),
                ItemSlots = await GetResources<ItemSlotsBuilder>("item_slots"),
                Items = await GetResources<ItemsBuilder>("items"),
                MobEffects = await GetResources<MobEffectsBuilder>("mob_effects"),
                ObjectiveCriteria = await GetResources<ObjectiveCriteriaBuilder>("objective_criteria"),
                Operations = await GetResources<OperationsBuilder>("operations"),
                Particles = await GetResources<ParticlesBuilder>("particles"),
                ScoreboardSlots = await GetResources<ScoreboardSlotsBuilder>("scoreboard_slots"),
                SelectorArguments = await GetResources<SelectorArgumentsBuilder>("selector_arguments"),
                Sorts = await GetResources<SortsBuilder>("sorts"),
                TimeScalars = await GetResources<TimeScalarsBuilder>("time_scalars")
            };
            VersionsBuilder versionsBuilder = await GetData();

            Logger.Log(AllDone());
            return versionsBuilder.Build(resources);
        }

        private async Task<VersionsBuilder> GetData()
        {
            string fileContents = await GetContents("https://raw.githubusercontent.com/ErrorCraft/FunctionAnalyser/master/resources/data.json");
            return VersionsBuilder.FromJson(fileContents);
        }

        private async Task<Dictionary<string, T>> GetResources<T>(string from)
        {
            Logger.Log(GettingFile(from));
            return await GetResource<T>(from, "");
        }

        private async Task<Dictionary<string, T>> GetResource<T>(string from, string name)
        {
            Dictionary<string, T> results = new Dictionary<string, T>();

            string contents = await GetContents(from, name);
            Path[] paths = JsonConvert.DeserializeObject<Path[]>(contents);

            for (int i = 0; i < paths.Length; i++)
            {
                string newName = Utilities.CombinePaths(name, paths[i].GetName());
                if (paths[i].GetContentType() == PathType.File)
                {
                    string fileContents = await GetContents(paths[i].GetDownloadUrl());
                    T result = JsonConvert.DeserializeObject<T>(fileContents);
                    results[newName] = result;
                } else
                {
                    Dictionary<string, T> nestedResults = await GetResource<T>(from, newName);
                    results.AddRange(nestedResults);
                }
            }
            return results;
        }

        private async Task<string> GetContents(string from, string name)
        {
            return await GetContents(Utilities.CombinePaths(from, name));
        }

        private async Task<string> GetContents(string from)
        {
            using HttpResponseMessage response = await Client.GetAsync(from);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new ResponseException(from, response);
            }
        }
    }
}
