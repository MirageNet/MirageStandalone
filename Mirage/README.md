# Mirage Standalone

Mirage Standalone package is a .NET Core version of [Mirage](https://github.com/MirageNet/Mirage). Mirage Standalone allows the core parts of Mirage to be used outside of unity.

Mirage is a rolling-release high-level API for the Unity Game Engine that provides a powerful, yet easy to use networking API. It is designed to work with Unity 3D and is available on GitHub.

## .NET Core Setup 

### Adding Weaver to project

Mirage uses mono.cecil to modify dlls after they are built to make some of the networking features work.

To make this work on your projects add this to the `.csproj` for a project.

```
  <Target Name="PostBuild" AfterTargets="PostBuildEvent">
    <Exec Command="$(SolutionDir)\\Mirage.CodeGen\bin\$(ConfigurationName)\net5.0\Mirage.CodeGen.exe $(TargetPath)" />
    <Error Condition="$(ExitCode) == 1" />
  </Target>
```

It will cause weaver to run when that project is compiled and stop if weaver has any errors

