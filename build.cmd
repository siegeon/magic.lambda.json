
set version=%1
set key=%2

cd %~dp0

dotnet build magic.lambda.json/magic.lambda.json.csproj --configuration Release --source https://api.nuget.org/v3/index.json
dotnet nuget push magic.lambda.json/bin/Release/magic.lambda.json.%version%.nupkg -k %key% -s https://api.nuget.org/v3/index.json
