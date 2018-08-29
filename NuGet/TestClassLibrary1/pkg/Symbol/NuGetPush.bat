setlocal
@echo off

@rem ApiKey‚ð“o˜^
..\..\..\nuget.exe SetApiKey [ApiKey]

@rem symbolsource.org‚ÉSymbolPackage‚ð“o˜^
..\..\..\nuget.exe push OssCons.DotNetSubcommittee.TestClassLibrary*.symbols.nupkg -source https://nuget.smbsrc.net/

pause