$ErrorActionPreference = 'Stop'
Push-Location "$PSScriptRoot/src/Gcds.Blazor"
npm install
npm run verify:coverage
npm run test:js
Pop-Location
if (Get-Command dotnet -ErrorAction SilentlyContinue) {
  dotnet restore "$PSScriptRoot/Gcds.Blazor.sln"
  dotnet build "$PSScriptRoot/Gcds.Blazor.sln" -c Release --no-restore
} else { Write-Warning '.NET SDK not found; npm/coverage/interop tests completed, but .NET compilation was skipped.' }
