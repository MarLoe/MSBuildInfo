using System;
using System.IO;
using System.Text.Json;

namespace MSBuildInfo.Sample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var files = Directory.EnumerateFiles(".", "BuildInfo.json", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var json = JsonDocument.Parse(File.ReadAllText(file));

                var str =  JsonSerializer.Serialize(json, new JsonSerializerOptions { WriteIndented = true });
                Console.WriteLine(str);
            }
        }
    }
}