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

    public override bool Execute()
    {
        Log.LogMessage(MessageImportance.High, $@"🧠 BuildInfoTask path: {System.IO.Path.Combine(Path, Name)}");

        return true;
    }
}
