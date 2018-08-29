setlocal
@echo off

@rem ApiKey‚ğ“o˜^
..\..\..\nuget.exe SetApiKey [ApiKey]

@rem nuget.org‚ÉPrimaryPackage‚ğ“o˜^(SymbolPackage‚ğ“o˜^‚µ‚És‚Á‚ÄƒGƒ‰[‚É‚È‚é‚ªA‚»‚ê‚Í–³‹)
..\..\..\nuget.exe push OssCons.DotNetSubcommittee.TestClassLibrary*.nupkg -source https://www.nuget.org/

pause