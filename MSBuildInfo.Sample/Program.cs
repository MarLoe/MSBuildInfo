using System;
using System.IO;

namespace MSBuildInfo.Sample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var buildInfo = Path.Combine("publish", "BuildInfo.json");
            if (File.Exists(buildInfo))
            {
                var lines = File.ReadAllLines(buildInfo);
                Console.WriteLine(string.Join(Environment.NewLine, lines));
            }
        }
    }
}