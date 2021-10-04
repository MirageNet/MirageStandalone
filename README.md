MirageStandalone


## Adding Weaver to project

Add this to the `.csproj` for a project.

```
  <Target Name="PostBuild" AfterTargets="PostBuildEvent">
    <Exec Command="$(SolutionDir)\Weaver\bin\Debug\net5.0\Weaver.exe $(TargetPath)" />
    <Error Condition="$(ExitCode) == 1" />
  </Target>
```

It will cause weaver to run when that project is compiled and stop if weaver has any errors