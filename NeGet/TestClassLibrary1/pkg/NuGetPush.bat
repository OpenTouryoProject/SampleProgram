..\..\nuget.exe SetApiKey [ApiKey]
..\..\nuget.exe push OssCons.DotNetSubcommittee.TestClassLibrary1.0.0.1.nupkg -Source https://api.nuget.org/v3/index.json
..\..\nuget.exe push OssCons.DotNetSubcommittee.TestClassLibrary1.0.0.1.symbols.nupkg -source https://nuget.smbsrc.net/

pause