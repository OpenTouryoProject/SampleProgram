setlocal
@echo off

@rem SymbolPackage‚Ì¶¬
..\nuget.exe pack OssCons.DotNetSubcommittee.TestClassLibrary1.nuspec -OutputDirectory pkg -Symbols

@rem PrimaryPackage‚Ì¶¬iã‘‚«j
..\nuget.exe pack OssCons.DotNetSubcommittee.TestClassLibrary1.nuspec -OutputDirectory pkg

pause