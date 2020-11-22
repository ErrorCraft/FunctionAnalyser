using AdvancedText;
using CommandParser.Collections;
using FunctionAnalyser;
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
            FunctionReader.SetVersions(commandsJson);

            string selectorArgumentsJson = await GetFile("selector_arguments.json");
            EntitySelectorOptions.Set(selectorArgumentsJson);

            string gamemodesJson = await GetFile("gamemodes.json");
            Gamemodes.Set(gamemodesJson);

            string sortsJson = await GetFile("sorts.json");
            Sorts.Set(sortsJson);

            string entitiesJson = await GetFile("entities.json");
            Entities.Set(entitiesJson);

            string itemsJson = await GetFile("items.json");
            Items.Set(itemsJson);

            string blocksJson = await GetFile("blocks.json");
            Blocks.Set(blocksJson);

            string timeScalarsJson = await GetFile("time_scalars.json");
            TimeScalars.Set(timeScalarsJson);

            string coloursJson = await GetFile("colours.json");
            Colours.Set(coloursJson);

            string slotsJson = await GetFile("item_slots.json");
            ItemSlots.Set(slotsJson);

            string particlesJson = await GetFile("particles.json");
            Particles.Set(particlesJson);

            string mobEffectsJson = await GetFile("mob_effects.json");
            MobEffects.Set(mobEffectsJson);

            string enchantmentsJson = await GetFile("enchantments.json");
            Enchantments.Set(enchantmentsJson);

            string objectiveCriteriaJson = await GetFile("objective_criteria.json");
            ObjectiveCriteria.Set(objectiveCriteriaJson);

            string scoreboardSlotsJson = await GetFile("scoreboard_slots.json");
            ScoreboardSlots.Set(scoreboardSlotsJson);

            string componentsJson = await GetFile("components.json");
            Components.Set(componentsJson);

            string anchorsJson = await GetFile("anchors.json");
            Anchors.Set(anchorsJson);

            string operationsJson = await GetFile("operations.json");
            Operations.Set(operationsJson);

            Writer.WriteLine(new TextComponent("All done!").WithColour(Colour.BuiltinColours.GREEN));
        }

        private async Task<string> GetFile(string file)
        {
            Writer.Write(new TextComponent("Getting ").WithColour(Colour.BuiltinColours.GREY));
            Writer.Write(new TextComponent(file).WithColour(Colour.BuiltinColours.GOLD));
            Writer.WriteLine(new TextComponent("...").WithColour(Colour.BuiltinColours.GREY));
            return await LoadFile(file);
        }

        private async Task<string> LoadFile(string fileName)
        {
            try
            {
                using HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(fileName);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new FileProcessorException(fileName, response);
                }
            }
            catch (HttpRequestException)
            {
                Writer.Write(new TextComponent("Was not able to get ").WithColour(Colour.BuiltinColours.RED));
                Writer.Write(new TextComponent(fileName).WithColour(Colour.BuiltinColours.GOLD));
                Writer.WriteLine(new TextComponent("!").WithColour(Colour.BuiltinColours.RED));
                throw;
            }
        }
    }
}
