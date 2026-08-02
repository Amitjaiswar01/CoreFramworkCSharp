@echo off

rem ************************
rem Configure test settings
rem ************************
set buildConfiguration=ReleaseGrid
set projectPath=LampsPlus.VisualRegressionTests
set traits= -trait "Category=Windows 10" -trait "Category=ChromeMobileSimulation" -notrait "Category=Run-ProductionOnly" -notrait "Category=Run-TestDatabaseOnly"
rem set traits= -trait "Category=Windows 10"   -trait "Category=ChromeMobileSimulation" 

rem **********************************
rem Configure test paths and runner
rem **********************************
set runner=..\packages\xunit.runner.console.2.4.0\tools\net472\xunit.console.exe
set projectDirectory=..\%projectPath%\bin\%buildConfiguration%\
set testProject=%projectPath%.dll
set resultsFileName=%projectDirectory%Results\InitialTestResults

rem **********************************
rem Run XUnit
rem **********************************
%runner% %projectDirectory%%testProject% %traits% -Html "%resultsFileName%.html" -xml "%resultsFileName%.xml"

exit 0
