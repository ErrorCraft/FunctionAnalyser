using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CommandFilesApi
{
    public static class ApiHelper
    {
        public static HttpClient ApiClient { get; set; }

        public static void Initialise()
        {
            ApiClient = new HttpClient();
            ApiClient.BaseAddress = new Uri("https://raw.githubusercontent.com/ErrorCraft/FunctionAnalyser/master/files/");
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
