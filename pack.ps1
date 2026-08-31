$ErrorActionPreference = 'Stop'
& "$PSScriptRoot/build.ps1"
dotnet pack "$PSScriptRoot/src/Gcds.Blazor/Gcds.Blazor.csproj" -c Release --no-build -o "$PSScriptRoot/artifacts"
