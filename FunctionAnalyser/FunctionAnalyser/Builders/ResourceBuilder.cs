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
using static Utilities.Generic;

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
                BaseAddress = new Uri("https://raw.githubusercontent.com/ErrorCraft/FunctionAnalyser/master/resources/")
            };
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Logger = logger;
        }

        public async Task<Dictionary<string, Dispatcher>> GetResources()
        {
            Definitions definitions = await GetDefinitions();
            DispatcherResourcesBuilder resources = new DispatcherResourcesBuilder()
            {
                Anchors = await GetResources<AnchorsBuilder>("anchors", definitions.GetAnchors()),
                Blocks = await GetResources<BlocksBuilder>("blocks", definitions.GetBlocks()),
                Colours = await GetResources<ColoursBuilder>("colours", definitions.GetColours()),
                Commands = await GetResources<CommandsBuilder>("commands", definitions.GetCommands()),
                Components = await GetResources<ComponentsBuilder>("components", definitions.GetComponents()),
                Enchantments = await GetResources<EnchantmentsBuilder>("enchantments", definitions.GetEnchantments()),
                Entities = await GetResources<EntitiesBuilder>("entities", definitions.GetEntities()),
                Gamemodes = await GetResources<GamemodesBuilder>("gamemodes", definitions.GetGamemodes()),
                Items = await GetResources<ItemsBuilder>("items", definitions.GetItems()),
                ItemComponents = await GetResources<ComponentsBuilder>("item_components", definitions.GetItemComponents()),
                ItemSlots = await GetResources<ItemSlotsBuilder>("item_slots", definitions.GetItemSlots()),
                MobEffects = await GetResources<MobEffectsBuilder>("mob_effects", definitions.GetMobEffects()),
                ObjectiveCriteria = await GetResources<ObjectiveCriteriaBuilder>("objective_criteria", definitions.GetObjectiveCriteria()),
                Operations = await GetResources<OperationsBuilder>("operations", definitions.GetOperations()),
                Particles = await GetResources<ParticlesBuilder>("particles", definitions.GetParticles()),
                ScoreboardSlots = await GetResources<ScoreboardSlotsBuilder>("scoreboard_slots", definitions.GetScoreboardSlots()),
                SelectorArguments = await GetResources<SelectorArgumentsBuilder>("selector_arguments", definitions.GetSelectorArguments()),
                Sorts = await GetResources<SortsBuilder>("sorts", definitions.GetSorts()),
                TimeScalars = await GetResources<TimeScalarsBuilder>("time_scalars", definitions.GetTimeScalars()),
                StructureRotations = await GetResources<StructureRotationsBuilder>("structure_rotations", definitions.GetStructureRotations()),
                StructureMirrors = await GetResources<StructureMirrorsBuilder>("structure_mirrors", definitions.GetStructureMirrors())
            };
            VersionsBuilder versionsBuilder = await GetData();

            Logger.Log(AllDone());
            return versionsBuilder.Build(resources);
        }

        private async Task<Definitions> GetDefinitions()
        {
#if DEBUG
            string json = await GetContents("definitions-debug.json");
#else
            string json = await GetContents("definitions.json");
#endif
            return JsonConvert.DeserializeObject<Definitions>(json);
        }

        private async Task<VersionsBuilder> GetData()
        {
#if DEBUG
            string json = await GetContents("data-debug.json");
#else
            string json = await GetContents("data.json");
#endif
            return VersionsBuilder.FromJson(json);
        }

        private async Task<Dictionary<string, T>> GetResources<T>(string from, string[] paths)
        {
            Logger.Log(GettingFile(from));
            Dictionary<string, T> resources = new Dictionary<string, T>();
            if (paths == null || paths.Length == 0) return resources;
            for (int i = 0; i < paths.Length; i++)
            {
                string url = CombinePaths(from, paths[i] + ".json");
                string json = await GetContents(url);
                resources.Add(paths[i], JsonConvert.DeserializeObject<T>(json));
            }
            return resources;
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
