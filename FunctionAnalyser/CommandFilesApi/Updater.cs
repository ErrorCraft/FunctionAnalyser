using AdvancedText;
using CommandFilesApi.GitHub;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;

namespace CommandFilesApi
{
    public class Updater
    {
        private static readonly string CHANGELOG_LABEL = "changelog";
        private static readonly string FILE_LABEL = "file";
        private readonly HttpClient Client;
        private readonly ILogger Logger;

        public Updater(ILogger logger)
        {
            Logger = logger;

            Client = new HttpClient();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
        }

        public async Task<Update> CheckForUpdate()
        {
#if DEBUG
            return null;
#endif
            Logger.Log(new TextComponent("Checking for updates...").WithColour(Colour.BuiltinColours.GREY));
            
            string versionsJson = await GetJsonAsync("https://api.github.com/repos/ErrorCraft/FunctionAnalyser/releases");
            if (versionsJson == null) return null;
            GitHubVersion[] allVersions = JsonConvert.DeserializeObject<GitHubVersion[]>(versionsJson);
            
            Version currentVersion = Assembly.GetEntryAssembly().GetName().Version;
            GitHubVersion[] newerVersions = GetNewerVersions(allVersions, currentVersion);

            if (newerVersions.Length == 0)
            {
                Logger.Log(new TextComponent("Up to date!").WithColour(Colour.BuiltinColours.GREEN));
                return null;
            }

            GitHubVersion latestVersion = GetLatestVersion(newerVersions);
            Logger.Log(new TextComponent($"Update available: {latestVersion.GetVersionTag()}!").WithColour(Colour.BuiltinColours.GREEN));

            string changelog = await GetChangelog(latestVersion);
            GitHubAssets fileAssets = GetFileAssets(latestVersion);
            return new Update(latestVersion.GetVersionTag(), changelog, fileAssets);
        }

        private async Task<string> GetChangelog(GitHubVersion update)
        {
            try
            {
                GitHubAssets changelogAssets = update.GetAssets().FirstOrDefault(a => a.GetLabel() == CHANGELOG_LABEL);
                using HttpResponseMessage response = await Client.GetAsync(changelogAssets.GetDownloadUrl());
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (HttpRequestException) { }

            return null;
        }

        private static GitHubAssets GetFileAssets(GitHubVersion update)
        {
            return update.GetAssets().FirstOrDefault(a => a.GetLabel() == FILE_LABEL);
        }

        private async Task<string> GetJsonAsync(string address)
        {
            try
            {
                using HttpResponseMessage response = await Client.GetAsync(address);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (HttpRequestException)
            {
                Logger.Log(new TextComponent("Failed to check for updates!").WithColour(Colour.BuiltinColours.RED));
            }

            return null;
        }

        private static GitHubVersion[] GetNewerVersions(GitHubVersion[] versions, Version currentVersion)
        {
            return versions.Where(v => v.GetVersionTag() > currentVersion).ToArray();
        }

        private static GitHubVersion GetLatestVersion(GitHubVersion[] versions)
        {
            GitHubVersion latest = null;
            for (int i = 0; i < versions.Length; i++)
            {
                if (latest == null || versions[i].GetVersionTag() > latest.GetVersionTag())
                {
                    latest = versions[i];
                }
            }
            return latest;
        }
    }
}
