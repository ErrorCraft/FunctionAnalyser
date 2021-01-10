using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ProgramUpdater
{
    public class Update
    {
        private readonly HttpClient Client;
        public System.Version UpdateVersion { get; }
        public string Changelog { get; }
        private readonly Assets FileAssets;

        public Update(System.Version updateVersion, string changelog, Assets fileAssets)
        {
            UpdateVersion = updateVersion;
            Changelog = changelog;
            FileAssets = fileAssets;
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/octet-stream"));
        }

        public async Task<(string tempFilePath, string newFilePath)> Download()
        {
            string originalFilePath = Process.GetCurrentProcess().MainModule.FileName;
            string temporaryFilePath = GetTemporaryFilePath(originalFilePath);
            File.Move(originalFilePath, temporaryFilePath, true);

            HttpResponseMessage responseMessage = await Client.GetAsync(FileAssets.GetDownloadUrl());
            HttpContent contents = responseMessage.Content;

            string newFilePath = Path.Combine(Path.GetDirectoryName(originalFilePath), FileAssets.GetName());
            using FileStream file = File.Create(newFilePath);
            Stream contentsStream = await contents.ReadAsStreamAsync();
            await contentsStream.CopyToAsync(file);

            return (temporaryFilePath, newFilePath);
        }

        private static string GetTemporaryFilePath(string originalFilePath)
        {
            string directoryPath = Path.GetDirectoryName(originalFilePath);
            string fileName = Path.GetFileNameWithoutExtension(originalFilePath);
            string fileExtension = Path.GetExtension(originalFilePath);
            return Path.Combine(directoryPath, $"{fileName}_TEMP{fileExtension}");
        }
    }
}
