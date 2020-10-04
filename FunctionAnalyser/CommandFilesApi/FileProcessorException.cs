using System;
using System.Globalization;
using System.Net.Http;

namespace CommandFilesApi
{
    class FileProcessorException : Exception
    {
        public FileProcessorException(string fileName, HttpResponseMessage response)
            : base($"{fileName}: {response.ReasonPhrase} ({((int)response.StatusCode).ToString(NumberFormatInfo.InvariantInfo)})") { }
    }
}
