setlocal
@echo off

@rem PrimaryPackageの生成
..\nuget.exe pack PrimaryPackage.nuspec -OutputDirectory pkg\Primary

@rem SymbolPackageの生成
..\nuget.exe pack SymbolPackage.nuspec -OutputDirectory pkg\Symbol -Symbols -SymbolPackageFormat snupkg

pause