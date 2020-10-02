using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CommandFilesApi
{
    public class FileProcessor
    {
        public static async Task<string> LoadFile(string fileName)
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(fileName))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                } else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
