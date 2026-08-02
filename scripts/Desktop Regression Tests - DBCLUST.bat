@echo off

rem Format current time YYYYDDMM
set currentDate=%date:~-4,4%%date:~-7,2%%date:~-10,2%
set currentTime=%time:~0,2%%time:~3,2%%time:~6,2%

rem Configure test settings
set buildConfiguration=ReleaseGrid
set projectPath=LampsPlus.RegressionTests
set traits=-trait "Category=Desktop" -trait "Database=DbClust" -notrait "Database=DbTest"

rem Configure test paths and runner
set runner=..\packages\xunit.runner.console.2.4.0\tools\net472\xunit.console.exe
set projectDirectory=..\%projectPath%\bin\%buildConfiguration%\
set testProject=%projectPath%.dll
set resultsFileName=%projectDirectory%Results\%currentDate% %currentTime% %releaseName% Test Results

rem Run script
%runner% %projectDirectory%%testProject% %traits% -Html "%resultsFileName%.html" -xml "%resultsFileName%.xml"

exit 0