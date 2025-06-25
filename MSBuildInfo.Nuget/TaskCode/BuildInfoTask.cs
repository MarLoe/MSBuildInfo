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

        public override bool Execute()
        {
            var buildInfo = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            foreach (var param in BuildInfo)
            {
                var key = param.ItemSpec?.Trim();
                if (string.IsNullOrEmpty(key))
                {
                    Log.LogWarning($"<BuildInfo /> without 'Include' observed - ignoring");
                    continue;
                }

                if (buildInfo.ContainsKey(key))
                {
                    Log.LogWarning($"Duplicate key '{key}' found. Overwriting previous value.");
                }

                buildInfo[key] = param.GetMetadata("Value")?.Trim() ?? string.Empty;
            }


            var json = JsonSerializer.Serialize(buildInfo, _jsonOptions);

            // Make sure the directory is present before writing the file
            Directory.CreateDirectory(Path);
            File.WriteAllText(System.IO.Path.Combine(Path, Name), json);

            return true;
        }
    }
}