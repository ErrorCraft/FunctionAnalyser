﻿using CommandParser.Results;
using Newtonsoft.Json;

namespace CommandParser.Arguments
{
    public class DoubleArgument : IArgument<double>
    {
        [JsonProperty("min")]
        private readonly double Minimum;
        [JsonProperty("max")]
        private readonly double Maximum;

        public DoubleArgument(double minimum = double.MinValue, double maximum = double.MaxValue)
        {
            Minimum = minimum;
            Maximum = maximum;
        }

        public ReadResults Parse(IStringReader reader, out double result)
        {
            int start = reader.GetCursor();
            ReadResults readResults = reader.ReadDouble(out result);
            if (readResults.Successful)
            {
                if (result < Minimum)
                {
                    reader.SetCursor(start);
                    return new ReadResults(false, CommandError.DoubleTooLow(result, Minimum).WithContext(reader));
                }

                if (result > Maximum)
                {
                    reader.SetCursor(start);
                    return new ReadResults(false, CommandError.DoubleTooHigh(result, Maximum).WithContext(reader));
                }
            }

            return readResults;
        }
    }
}
