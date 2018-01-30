setlocal
@echo off

@rem SymbolPackage‚Ì¶¬
..\nuget.exe pack SymbolPackage.nuspec -OutputDirectory pkg -Symbols

@rem PrimaryPackage‚Ì¶¬iã‘‚«j
..\nuget.exe pack PrimaryPackage.nuspec -OutputDirectory pkg

pause