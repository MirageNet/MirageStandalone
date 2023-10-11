# Mirage Standalone

Mirage Standalone package is a .NET Core version of [Mirage](https://github.com/MirageNet/Mirage). Mirage Standalone allows the core parts of Mirage to be used outside of unity.

Mirage is a rolling-release high-level API for the Unity Game Engine that provides a powerful, yet easy to use networking API. It is designed to work with Unity 3D and is available on GitHub.

## Install

Either add source code to your project, or add the [nuget package](https://www.nuget.org/packages/MirageNet.Mirage/)

## Adding Weaver to project

Add this to the `.csproj` for a project. when working within this project

```
  <Target Name="PostBuild" AfterTargets="PostBuildEvent">
    <Exec Command="$(SolutionDir)\\Mirage.CodeGen\bin\$(ConfigurationName)\net5.0\Mirage.CodeGen.exe $(TargetPath)" />
    <Error Condition="$(ExitCode) == 1" />
  </Target>
```

or add this when working with the nuget package

```
  <ItemGroup>
    <PackageReference Include="MirageNet.Mirage" Version="130.3.5" GeneratePathProperty="true" />
  </ItemGroup>

  <Target Name="PostBuild" AfterTargets="PostBuildEvent">
    <Exec Command="&quot;$(PkgMirageNet_Mirage)/CodeGen/Mirage.CodeGen.exe&quot; &quot;$(TargetPath)&quot;" />
    <Error Condition="$(ExitCode) == 1" />
  </Target>
```

It will cause weaver to run when that project is compiled and stop if weaver has any errors

## Create package

```sh
sh Pack.sh
```

## How to update this repo

Run copy script
```sh
CopyFromMirage.sh "<path to mirage repo>"
```

This script will delete all old scripts and copy new scripts into the correct folders in this repo. 
The files should then be double checked before committing and pushing.
