using System;

namespace CommandFilesApi
{
    public class Update
    {
        public Version UpdateVersion { get; }
        public string Changelog { get; }
        public string FileUrl { get; }

        public Update(Version updateVersion, string changelog, string fileUrl)
        {
            UpdateVersion = updateVersion;
            Changelog = changelog;
            FileUrl = fileUrl;
        }
    }
}
