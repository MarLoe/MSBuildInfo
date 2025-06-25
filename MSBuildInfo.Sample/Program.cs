using System;
using System.IO;

namespace MSBuildInfo.Sample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var files = Directory.EnumerateFiles(".", "BuildInfo.json", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var lines = File.ReadAllLines(file);
                Console.WriteLine(string.Join(Environment.NewLine, lines));
            }
        }
    }
}