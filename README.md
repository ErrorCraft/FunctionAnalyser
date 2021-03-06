# Function Analyser
With this tool you can analyse your functions and get a report that includes details about them. It also includes an auto-updater, so you don't have to check this page all the time. There's a (really) small FAQ below.

## Credits
Thanks to [Ersatz](https://github.com/Ersatz77) for allowing me to use [bedrock-data](https://github.com/Ersatz77/bedrock-data) for Bedrock!

## Using the function analyser
You can select a folder and press "Analyse" to analyse all the functions under that folder. It ignores the path name/casing, so a path like `foo/Bar!/B@Z.mcfunction` is valid, even though if used in-game it would not be a valid path. This way you can analyse more than one data pack at once, or even multiple data packs in different worlds! Do note that it may take a second if you want to analyse a lot of functions.

## The generated report
The analyser parses all the commands in the functions, and can thus find errors in your commands as well, [even more than](https://bugs.mojang.com/browse/MC-165773) [the game can](https://bugs.mojang.com/browse/MC-198113)! It also reports various things, such as all of the commands used, the number of selectors, and NBT access.

There are options to spice up the report as well:
|Option|Description|
|-|-|
| Skip function on error | If enabled, it will skip the function if it contains a command error. The results (if any) will not contribute to the information found. |
| Show command errors | Shows the command errors if they are found. A function may contain multiple errors. |
| Show empty functions | Shows empty functions if they are found. A function is empty if it does not contain any commands. |
| Version | The version to use when parsing the commands. |
| Sort type | How to sort the commands in the report. The possible options are "Times used", "Alphabetical", and "Command length". |

You can also export the results to a text file with the "Export Data" button.


# FAQ
**"I get a message saying I have to install .NET, what do I do?"**
Go to [this](https://dotnet.microsoft.com/download/dotnet-core/5.0/runtime/?utm_source=getdotnetcore&utm_medium=referral) page and select the correct version from "Run desktop apps".
The reason people get this message might be because .NET 5 is fairly new. (November 2020)

**"Will there be a Bedrock version/port?"**
There is a Bedrock version included. It's definitely not complete and bug free though, so please tell me if I need to change something, preferably via an [issue](https://github.com/ErrorCraft/FunctionAnalyser/issues).
