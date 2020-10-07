using AdvancedText;
using CommandVerifier;
using CommandVerifier.Commands.Collections;
using System.Net.Http;
using System.Threading.Tasks;

namespace CommandFilesApi
{
    public class FileProcessor
    {
        private readonly IWriter Writer;

        public FileProcessor(IWriter writer)
        {
            Writer = writer;
        }

        public async Task GetFiles()
        {
            string commandsJson = await GetFile("commands.json");
            CommandReader.SetCommands(commandsJson);

            string selectorsArgumentsJson = await GetFile("selector_arguments.json");
            EntitySelectorOptions.SetOptions(selectorsArgumentsJson);

            string particlesJson = await GetFile("particles.json");
            Particles.SetOptions(particlesJson);

            string itemsJson = await GetFile("items.json");
            Items.SetOptions(itemsJson);

            string entitiesJson = await GetFile("entities.json");
            Entities.SetOptions(entitiesJson);

            string scoreboardCriteriaJson = await GetFile("scoreboard_criteria.json");
            ScoreboardCriteria.SetOptions(scoreboardCriteriaJson);

            string scoreboardSlotsJson = await GetFile("scoreboard_slots.json");
            ScoreboardSlots.SetOptions(scoreboardSlotsJson);

            string blocksJson = await GetFile("blocks.json");
            Blocks.SetOptions(blocksJson);

            string effectsJson = await GetFile("effects.json");
            Effects.SetOptions(effectsJson);

            string enchantmentsJson = await GetFile("enchantments.json");
            Enchantments.SetOptions(enchantmentsJson);

            Writer.WriteLine(new TextComponent("All done!", Colour.BuiltinColours.GREEN));
        }

        private async Task<string> GetFile(string file)
        {
            Writer.Write(new TextComponent("Getting ", Colour.BuiltinColours.GREY));
            Writer.Write(new TextComponent(file, Colour.BuiltinColours.GOLD));
            Writer.WriteLine(new TextComponent("...", Colour.BuiltinColours.GREY));
            return await LoadFile(file);
        }

        private async Task<string> LoadFile(string fileName)
        {
            try
            {
                using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(fileName))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        throw new FileProcessorException(fileName, response);
                    }
                }
            }
            catch (HttpRequestException)
            {
                Writer.Write(new TextComponent("Was not able to get ", Colour.BuiltinColours.RED));
                Writer.Write(new TextComponent(fileName, Colour.BuiltinColours.GOLD));
                Writer.WriteLine(new TextComponent("!", Colour.BuiltinColours.RED));
                throw;
            }
        }
    }
}
