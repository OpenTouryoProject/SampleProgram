setlocal

@rem --------------------------------------------------
@rem Turn off the echo function.
@rem --------------------------------------------------
@echo off

@rem --------------------------------------------------
@rem Get the path to the executable file.
@rem --------------------------------------------------
set CURRENT_DIR=%~dp0

@rem --------------------------------------------------
@rem Execution of the common processing.
@rem --------------------------------------------------
call %CURRENT_DIR%z_Common.bat

rem --------------------------------------------------
rem build the batch
rem --------------------------------------------------
%BUILDFILEPATH% %COMMANDLINE% "Asynchronous\Asynchronous.sln"

%BUILDFILEPATH% %COMMANDLINE% "DotNETBridge\BridgeDLL\BridgeDLL.sln"
%BUILDFILEPATH% %COMMANDLINE% "DotNETBridge\CallerEXE\CallerEXE.sln"
%BUILDFILEPATH% %COMMANDLINE% "DotNETBridge\CallerEXE2\CallerEXE2.sln"
%BUILDFILEPATH% %COMMANDLINE% "DotNETBridge\TargetAssembly\TargetAssembly.sln"

%BUILDFILEPATH% %COMMANDLINE% "InterProcComm\InterProcComm.sln"

%BUILDFILEPATH% %COMMANDLINE% "PipesFamilyHouse\AnonymousPipe\Child\Child.sln"
%BUILDFILEPATH% %COMMANDLINE% "PipesFamilyHouse\AnonymousPipe\Parent\Parent.sln"
%BUILDFILEPATH% %COMMANDLINE% "PipesFamilyHouse\StdIOAndPipe\Child\Child.sln"
%BUILDFILEPATH% %COMMANDLINE% "PipesFamilyHouse\StdIOAndPipe\Parent\Parent.sln"

%BUILDFILEPATH% %COMMANDLINE% "ThreadSafe\ThreadSafe.sln"

%BUILDFILEPATH% %COMMANDLINE% "VC_Samples\VC_AutoWrap\VC_AutoWrap.sln"

pause

rem -------------------------------------------------------
endlocal
