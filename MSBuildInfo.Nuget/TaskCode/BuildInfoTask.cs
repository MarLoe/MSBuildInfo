using System.IO;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System.Collections.Generic;
using System;
using System.Text.Json;
using System.Text.Encodings.Web;

namespace MSBuildInfo.Nuget.TaskCode
{
    public class MSBuildInfoTask : Task
    {

        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        };

        [Required]
        public string Path { get; set; } = string.Empty;

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public ITaskItem[] BuildInfo { get; set; } = [];

        // public string? BuildInfo1 { get; set; } = default;

        public override bool Execute()
        {
            var fileName = System.IO.Path.Combine(Path, Name);
            Log.LogMessage(MessageImportance.Normal, $@"Writing BuildInfo to: {fileName}");

            // Access all environment variables
            // var envVars = Environment.GetEnvironmentVariables();

            // foreach (DictionaryEntry de in envVars)
            // {
            //     Log.LogMessage(MessageImportance.High, $"{de.Key} = {de.Value}");
            // }

            // Log.LogMessage(MessageImportance.High, $"😀 Parameters: {BuildInfo}");

            // if (BuildInfo1 is not null)
            {
                // Log.LogMessage(MessageImportance.High, $"😀 {nameof(BuildInfo1)}: {BuildInfo1}");
            }

            var buildInfo = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            foreach (var param in BuildInfo)
            {
                var key = param.ItemSpec?.Trim();
                if (string.IsNullOrEmpty(key))
                {
                    Log.LogWarning($"Property with value {param.ItemSpec} does not have a Key. Use <BuildInfo Key=\"Name\"");
                    continue;
                }

                if (buildInfo.ContainsKey(key))
                {
                    Log.LogWarning($"Duplicate key '{key}' found. Overwriting previous value.");
                }

                buildInfo[key] = param.GetMetadata("Value")?.Trim() ?? string.Empty;
            }


            var json = JsonSerializer.Serialize(buildInfo, _jsonOptions);
            File.WriteAllText(fileName, json);

            return true;
        }
    }
}