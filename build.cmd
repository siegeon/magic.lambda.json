cd %~dp0
dotnet build magic.lambda.json/magic.lambda.json.csproj --configuration Release --source https://api.nuget.org/v3/index.json
