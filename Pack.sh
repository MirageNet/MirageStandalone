#!/bin/bash
# Extract version from AssemblyInfo.cs
version=$(grep -oP '\[assembly: AssemblyVersion\("\K(.*)(?="\)\])' Mirage/Runtime/AssemblyInfo.cs)

# Update AssemblyVersion and Version in .csproj file
sed -i "s/<AssemblyVersion>.*<\/AssemblyVersion>/<AssemblyVersion>$version<\/AssemblyVersion>/g" Mirage/Mirage.csproj
sed -i "s/<Version>.*<\/Version>/<Version>$version<\/Version>/g" Mirage/Mirage.csproj

dotnet publish ./Mirage.CodeGen/Mirage.CodeGen.csproj --configuration Release
dotnet pack ./Mirage/Mirage.csproj --configuration Release --output out
