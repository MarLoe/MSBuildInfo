using System.IO;
using System.Collections;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

public class BuildInfoTask : Task
{
    [Required]
    public string Path { get; set; } = string.Empty;

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public ITaskItem[] BuildInfo { get; set; } = [];

    public override bool Execute()
    {
        Log.LogMessage(MessageImportance.High, $@"🧠 BuildInfoTask path: {System.IO.Path.Combine(Path, Name)}");

        // You can also access MSBuild properties passed as parameters here.
        foreach (var param in BuildInfo)
        {
            var metaData = param.GetMetadata("Key");
            if (string.IsNullOrEmpty(metaData))
            {
                Log.LogWarning($"Property with value {param.ItemSpec} does not have a Key. Use <BuildInfo Key=\"Name\"");
                continue;
            }

            Log.LogMessage(MessageImportance.High, $"{metaData}={param.ItemSpec}");
        }

        return true;
    }
}
