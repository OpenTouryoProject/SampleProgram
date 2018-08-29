setlocal
@echo off

@rem PrimaryPackage‚Ì¶¬
..\nuget.exe pack PrimaryPackage.nuspec -OutputDirectory pkg\Primary

@rem SymbolPackage‚Ì¶¬
..\nuget.exe pack SymbolPackage.nuspec -OutputDirectory pkg\Symbol -Symbols

pause