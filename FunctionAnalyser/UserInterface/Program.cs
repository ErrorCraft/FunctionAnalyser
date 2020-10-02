using CommandVerifier;
using CommandVerifier.Commands.Collections;
using System;
using System.IO;

namespace UserInterface
{
    class Program
    {
        private const int PROGRAM_VERSION = 0;

        [STAThread]
        static void Main()
        {
            // Get version.json
            // if version::program_version > PROGRAM_VERSION
            //  Make user download new version
            //  Close program
            // else
            //  Get files from the web (all)

            /*string commands_json_path = @"C:\Users\Ramon\Desktop\MCC and such\commands.json";
            string entity_selector_arguments_json_path = @"C:\Users\Ramon\Desktop\MCC and such\selector_arguments.json";
            string particles_json_path = @"C:\Users\Ramon\Desktop\MCC and such\particles.json";
            string items_json_path = @"C:\Users\Ramon\Desktop\MCC and such\items.json";
            string entities_json_path = @"C:\Users\Ramon\Desktop\MCC and such\entities.json";
            string scoreboard_criteria_json_path = @"C:\Users\Ramon\Desktop\MCC and such\scoreboard_criteria.json";
            string scoreboard_slots_json_path = @"C:\Users\Ramon\Desktop\MCC and such\scoreboard_slots.json";
            string blocks_json_path = @"C:\Users\Ramon\Desktop\MCC and such\blocks.json";
            string effects_json_path = @"C:\Users\Ramon\Desktop\MCC and such\effects.json";
            string enchantments_json_path = @"C:\Users\Ramon\Desktop\MCC and such\enchantments.json";

            string commands_json = File.ReadAllText(commands_json_path);
            string entity_selector_arguments_json = File.ReadAllText(entity_selector_arguments_json_path);
            string particles_json = File.ReadAllText(particles_json_path);
            string items_json = File.ReadAllText(items_json_path);
            string entities_json = File.ReadAllText(entities_json_path);
            string scoreboard_criteria_json = File.ReadAllText(scoreboard_criteria_json_path);
            string scoreboard_slots_json = File.ReadAllText(scoreboard_slots_json_path);
            string blocks_json = File.ReadAllText(blocks_json_path);
            string effects_json = File.ReadAllText(effects_json_path);
            string enchantments_json = File.ReadAllText(enchantments_json_path);

            CommandReader.SetCommands(commands_json);
            EntitySelectorOptions.SetOptions(entity_selector_arguments_json);
            Particles.SetOptions(particles_json);
            Items.SetOptions(items_json);
            Entities.SetOptions(entities_json);
            ScoreboardCriteria.SetSlots(scoreboard_criteria_json);
            ScoreboardSlots.SetOptions(scoreboard_slots_json);
            Blocks.SetOptions(blocks_json);
            Effects.SetOptions(effects_json);
            Enchantments.SetOptions(enchantments_json);*/

            App app = new App();
            app.InitializeComponent();
            app.Run();
        }
    }
}
