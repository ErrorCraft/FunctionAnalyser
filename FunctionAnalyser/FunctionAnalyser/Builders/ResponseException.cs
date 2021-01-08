using System;
using System.Globalization;
using System.Net.Http;

namespace FunctionAnalyser.Builders
{
    public class ResponseException : Exception
    {
        public ResponseException(string fileName, HttpResponseMessage response)
            : base($"{fileName}: {response.ReasonPhrase} ({((int)response.StatusCode).ToString(NumberFormatInfo.InvariantInfo)})") { }
    }
}
