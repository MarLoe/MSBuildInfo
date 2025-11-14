# MSBuildInfo
[![NuGet version (MSBuildInfo)](https://img.shields.io/nuget/v/MSBuildInfo.svg?style=flat-square)](https://www.nuget.org/packages/MSBuildInfo)

Generate json with build information during build.

When building your project in your prefered CI pipeline, I found that getting information about the build could sometimes be valueable - but hard to extract.

This is where `MSBuildInfo` comes in handy. Just add the NuGet to your project and it will start generating `BuildInfo.json` on every build.

## Customise
You can add you own build properties to the BUildInfo.json. This can be done in one of two ways.

Either you can add this file next to you project file (.csproj):
[BuildInfo.build.targets](https://github.com/MarLoe/MSBuildInfo/raw/refs/heads/master/MSBuildInfo.Sample/MSBuildInfo.build.targets).

You can also add the content directly to your project (.csproj) file
```xml
<Target Name="GenerateBuildInfo">
  <PropertyGroup>
    <CustomValue>This is my value</CustomValue>
  </PropertyGroup>
  <ItemGroup>
    <BuildInfo Include="CustomInfo" Value="$(CustomValue)" />
    <BuildInfo Include="TestVariable" Value="$(TestVariable)" />
  </ItemGroup>
</Target>
```

In both cases you just add more entries of
```xml
    <BuildInfo Include="Key" Value="Value" />
```

You cannot do both. MSBuild will only pick up the one defined last in the build process.

# TLDR;
Reference this NuGet and add this file next to you project file (.csproj):
[BuildInfo.build.targets](https://github.com/MarLoe/MSBuildInfo/raw/refs/heads/master/MSBuildInfo.Sample/MSBuildInfo.build.targets).

Look for the generated BuildInfo.json in your output folder.