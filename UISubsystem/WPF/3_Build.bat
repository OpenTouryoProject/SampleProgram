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
%BUILDFILEPATH% %COMMANDLINE% "Animation\Menu\Menu.sln"
%BUILDFILEPATH% %COMMANDLINE% "Animation\TimelineAndFrame\TimelineAndFrame.sln"

%BUILDFILEPATH% %COMMANDLINE% "Cbx in DataGrid\DataGrid\DataGrid.sln"
%BUILDFILEPATH% %COMMANDLINE% "Cbx in DataGrid\GridView\GridView.sln"

%BUILDFILEPATH% %COMMANDLINE% "DataBinding\DataBinding.sln"
%BUILDFILEPATH% %COMMANDLINE% "InputSupport\InputSupport.sln"
%BUILDFILEPATH% %COMMANDLINE% "Template\Template.sln"
%BUILDFILEPATH% %COMMANDLINE% "Trigger\Trigger.sln"

%BUILDFILEPATH% %COMMANDLINE% "Validation\DataGrid\DataGrid.sln"
%BUILDFILEPATH% %COMMANDLINE% "Validation\InputField\InputField.sln"

pause

rem -------------------------------------------------------
endlocal
