using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.IO;

namespace ArdalisRating
{
    public class ConsoleLogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
