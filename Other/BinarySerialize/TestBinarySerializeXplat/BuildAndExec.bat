setlocal

@echo off
set CURRENT_DIR="%~dp0"
call %CURRENT_DIR%z_Common.bat

..\..\..\nuget.exe restore "TestBinarySerializeXplatFx45.sln"
%BUILDFILEPATH% %COMMANDLINE% "TestBinarySerializeXplatFx45.sln"

..\..\..\nuget.exe restore "TestBinarySerializeXplatFx46.sln"
%BUILDFILEPATH% %COMMANDLINE% "TestBinarySerializeXplatFx46.sln"

..\..\..\nuget.exe restore "TestBinarySerializeXplatFx47.sln"
%BUILDFILEPATH% %COMMANDLINE% "TestBinarySerializeXplatFx47.sln"

..\..\..\nuget.exe restore "TestBinarySerializeXplatFx48.sln"
%BUILDFILEPATH% %COMMANDLINE% "TestBinarySerializeXplatFx48.sln"

dotnet restore "TestBinarySerializeXplatCore20.sln"
dotnet msbuild %COMMANDLINE2% "TestBinarySerializeXplatCore20.sln"

dotnet restore "TestBinarySerializeXplatCore30.sln"
dotnet msbuild %COMMANDLINE2% "TestBinarySerializeXplatCore30.sln"
pause

call ".\net45\bin\Debug\TestBinarySerializeXplat.exe"
call ".\net46\bin\Debug\TestBinarySerializeXplat.exe"
call ".\net47\bin\Debug\TestBinarySerializeXplat.exe"
call ".\net48\bin\Debug\TestBinarySerializeXplat.exe"
call ".\net45\bin\Debug\TestBinarySerializeXplat.exe"

dotnet ".\core20\bin\Debug\netcoreapp2.0\TestBinarySerializeXplat.dll"
dotnet ".\core30\bin\Debug\netcoreapp3.0\TestBinarySerializeXplat.dll"
dotnet ".\core20\bin\Debug\netcoreapp3.0\TestBinarySerializeXplat.dll"

pause

endlocal
