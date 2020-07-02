setlocal

@rem --------------------------------------------------
@rem Turn off the echo function.
@rem --------------------------------------------------
@echo off

@rem --------------------------------------------------
@rem Get the path to the executable file.
@rem --------------------------------------------------
set CURRENT_DIR="%~dp0"

@rem --------------------------------------------------
@rem Execution of the common processing.
@rem --------------------------------------------------
call %CURRENT_DIR%z_Common.bat

rem --------------------------------------------------
rem Batch build of TestClassLibrary1_net nn.
rem --------------------------------------------------
copy /Y "TestClassLibrary1\packages_net46.config" "TestClassLibrary1\packages.config"
..\nuget.exe restore "TestClassLibrary1_net46.sln"
%BUILDFILEPATH% %COMMANDLINE% "TestClassLibrary1_net46.sln"

rem if exist "packages" rd /s /q "packages"
copy /Y "TestClassLibrary1\packages_net47.config" "TestClassLibrary1\packages.config"
..\nuget.exe restore "TestClassLibrary1_net47.sln"
%BUILDFILEPATH% %COMMANDLINE% "TestClassLibrary1_net47.sln"

pause

rem --------------------------------------------------
rem Batch build of TestClassLibrary1_netstd nn.
rem --------------------------------------------------
dotnet restore "TestClassLibrary1_netstd2.sln"
dotnet msbuild %COMMANDLINE% "TestClassLibrary1_netstd2.sln"

pause

rem -------------------------------------------------------
endlocal
