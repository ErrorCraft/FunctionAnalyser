using AdvancedText;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CommandFilesApi
{
    public class FileProcessor
    {
        public static async Task<string> LoadFile(string fileName, IWriter output)
        {
            try
            {
                using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(fileName))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        throw new FileProcessorException(fileName, response);
                    }
                }
            }
            catch (HttpRequestException)
            {
                output.Write(new TextComponent("Was not able to get ", Colour.BuiltinColours.RED));
                output.Write(new TextComponent(fileName, Colour.BuiltinColours.GOLD));
                output.WriteLine(new TextComponent("!", Colour.BuiltinColours.RED));
                throw;
            }
        }
    }
}
