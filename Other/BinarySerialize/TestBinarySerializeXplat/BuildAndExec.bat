setlocal

@echo off
set CURRENT_DIR="%~dp0"
call %CURRENT_DIR%z_Common2.bat

..\..\..\nuget.exe restore "TestBinarySerializeXplatFx45.sln"
%BUILDFILEPATH% %COMMANDLINE% "TestBinarySerializeXplatFx45.sln"
pause

..\..\..\nuget.exe restore "TestBinarySerializeXplatFx46.sln"
%BUILDFILEPATH% %COMMANDLINE% "TestBinarySerializeXplatFx46.sln"
pause

..\..\..\nuget.exe restore "TestBinarySerializeXplatFx47.sln"
%BUILDFILEPATH% %COMMANDLINE% "TestBinarySerializeXplatFx47.sln"
pause

..\..\..\nuget.exe restore "TestBinarySerializeXplatFx48.sln"
%BUILDFILEPATH% %COMMANDLINE% "TestBinarySerializeXplatFx48.sln"
pause

call ".\net45\bin\Debug\TestBinarySerializeXplat.exe"
call ".\net46\bin\Debug\TestBinarySerializeXplat.exe"
call ".\net47\bin\Debug\TestBinarySerializeXplat.exe"
call ".\net48\bin\Debug\TestBinarySerializeXplat.exe"
call ".\net45\bin\Debug\TestBinarySerializeXplat.exe"
pause

endlocal
