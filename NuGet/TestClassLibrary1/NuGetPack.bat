setlocal
@echo off

@rem SymbolPackageの生成
..\nuget.exe pack SymbolPackage.nuspec -OutputDirectory pkg -Symbols

@rem PrimaryPackageの生成（上書き）
..\nuget.exe pack PrimaryPackage.nuspec -OutputDirectory pkg

pause