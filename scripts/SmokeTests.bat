@echo off

rem ************************
rem Configure test settings
rem ************************
set buildConfiguration=ReleaseGrid
set projectPath=LampsPlus.RegressionTests
set traits= -trait "Category=Debugging"

rem **********************************
rem Configure test paths and runner
rem **********************************
set runner=..\packages\xunit.runner.console.2.4.1\tools\net472\xunit.console.exe 
set projectDirectory=..\%projectPath%\bin\%buildConfiguration%\
set testProject=%projectPath%.dll
set resultsFileName=%projectDirectory%Results\TestResults


rem **********************************
rem Run XUnit
rem **********************************
%runner% %projectDirectory%%testProject% %traits% -Html "%resultsFileName%.html" -xml "%resultsFileName%.xml"

exit 0
