using AdvancedText;
using CommandParser.Collections;
using FunctionAnalyser;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CommandFilesApi
{
    public class FileProcessor
    {
        private readonly HttpClient Client;
        private readonly ILogger Logger;

        public FileProcessor(ILogger logger)
        {
            Logger = logger;

            Client = new HttpClient()
            {
                BaseAddress = new Uri("https://raw.githubusercontent.com/ErrorCraft/FunctionAnalyser/master/files/")
            };
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task GetFiles()
        {
            string anchorsJson = await GetFile("anchors.json");
            Anchors.Set(anchorsJson);

            string timeScalarsJson = await GetFile("time_scalars.json");
            TimeScalars.Set(timeScalarsJson);

            string operationsJson = await GetFile("operations.json");
            Operations.Set(operationsJson);

            string sortsJson = await GetFile("sorts.json");
            Sorts.Set(sortsJson);

            string gamemodesJson = await GetFile("gamemodes.json");
            Gamemodes.Set(gamemodesJson);

            string scoreboardSlotsJson = await GetFile("scoreboard_slots.json");
            ScoreboardSlots.Set(scoreboardSlotsJson);

            string coloursJson = await GetFile("colours.json");
            Colours.Set(coloursJson);

            string mobEffectsJson = await GetFile("mob_effects.json");
            MobEffects.Set(mobEffectsJson);

            string enchantmentsJson = await GetFile("enchantments.json");
            Enchantments.Set(enchantmentsJson);

            string entitiesJson = await GetFile("entities.json");
            Entities.Set(entitiesJson);

            string selectorArgumentsJson = await GetFile("selector_arguments.json");
            EntitySelectorOptions.Set(selectorArgumentsJson);

            string objectiveCriteriaJson = await GetFile("objective_criteria.json");
            ObjectiveCriteria.Set(objectiveCriteriaJson);

            string componentsJson = await GetFile("components.json");
            Components.Set(componentsJson);

            string particlesJson = await GetFile("particles.json");
            Particles.Set(particlesJson);

            string slotsJson = await GetFile("item_slots.json");
            ItemSlots.Set(slotsJson);

            string itemsJson = await GetFile("items.json");
            Items.Set(itemsJson);

            string blocksJson = await GetFile("blocks.json");
            Blocks.Set(blocksJson);

            string commandsJson = await GetFile("commands.json");
            FunctionReader.SetVersions(commandsJson);

            Logger.Log(new TextComponent("All done!").WithColour(Colour.BuiltinColours.GREEN));
        }

        private async Task<string> GetFile(string file)
        {
            Logger.Log(new TextComponent("Getting ").WithColour(Colour.BuiltinColours.GREY).With(
                new TextComponent(file).WithColour(Colour.BuiltinColours.GOLD).With(
                    new TextComponent("...").WithColour(Colour.BuiltinColours.GREY)
                    )
                ));
            return await LoadFile(file);
        }

        private async Task<string> LoadFile(string fileName)
        {
            try
            {
                using HttpResponseMessage response = await Client.GetAsync(fileName);
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
                Logger.Log(new TextComponent("Was not able to get ").WithColour(Colour.BuiltinColours.RED).With(
                new TextComponent(fileName).WithColour(Colour.BuiltinColours.GOLD).With(
                    new TextComponent("!").WithColour(Colour.BuiltinColours.RED)
                    )
                ));
                throw;
            }
        }
    }
}
