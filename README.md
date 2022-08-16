MirageStandalone


## Adding Weaver to project

Add this to the `.csproj` for a project.

```
  <Target Name="PostBuild" AfterTargets="PostBuildEvent">
    <Exec Command="$(SolutionDir)\\Mirage.CodeGen\bin\$(ConfigurationName)\net5.0\Mirage.CodeGen.exe $(TargetPath)" />
    <Error Condition="$(ExitCode) == 1" />
  </Target>
```

It will cause weaver to run when that project is compiled and stop if weaver has any errors


## How to update this repo

Run copy script
```sh
CopyFromMirage.sh "<path to mirage repo>"
```

This script will delete all old scripts and copy new scripts into the correct folders in this repo. 
The files should then be double checked before committing and pushing.


