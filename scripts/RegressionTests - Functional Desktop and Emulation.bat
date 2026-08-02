@echo off

rem ************************
rem Configure test settings
rem ************************
set buildConfiguration=ReleaseGrid
set projectPath=LampsPlus.RegressionTests
set traits= -notrait "Category=Chrome Mobile Emulation" -notrait "Category=Android 8 Phone" -notrait "Category=iOS 14 Phone" -notrait "Category=iOS 14 Tablet" -notrait "Category=Chrome Tablet Emulation" -notrait "Category=Mac Mojave" -trait "Category=Common-AddingToCartAndWishList" -trait "Category=Common-AugmentedReality" -trait "Category=Common-Certona" -trait "Category=Common-ChangeEmailPreferences" -trait "Category=Common-ContactUs" -trait "Category=Common-CreateAccount" -trait "Category=Common-HeaderFooter" -trait "Category=Common-Homepage" -trait "Category=Common-ManageAccount" -trait "Category=Common-OrderHistory" -trait "Category=Common-Payment" -trait "Category=Common-Pixels" -trait "Category=Common-ProductDetail" -trait "Category=Common-Search" -trait "Category=Common-Shipping" -trait "Category=Common-CartOverview" -trait "Category=Common-Sort" -trait "Category=Common-Stores" -trait "Category=Common-OrderConfirmation" -trait "Category=Desktop-AddingToCartAndWishList" -trait "Category=Desktop-AugmentedReality" -trait "Category=Desktop-ChangeEmailPreferences" -trait "Category=Desktop-ContactUs" -trait "Category=Desktop-CreateAccount" -trait "Category=Desktop-HeaderFooter" -trait "Category=Desktop-Homepage" -trait "Category=Desktop-ManageAccount" -trait "Category=Desktop-OrderHistory" -trait "Category=Desktop-OrderSummary" -trait "Category=Desktop-Payment" -trait "Category=Desktop-Pixels" -trait "Category=Desktop-ProductDetail" -trait "Category=Desktop-Search" -trait "Category=Desktop-SecureSignin" -trait "Category=Desktop-Shipping" -trait "Category=Desktop-CartOverview" -trait "Category=Desktop-Sort" -trait "Category=Desktop-Stores" -trait "Category=Desktop-OrderConfirmation" -trait "Category=Run-ProductionOnly" -trait "Category=Run-TestDatabaseOnly"


rem **********************************
rem Configure test paths and runner
rem **********************************
set runner=..\packages\xunit.runner.console.2.4.2\tools\net472\xunit.console.exe
set projectDirectory=..\%projectPath%\bin\%buildConfiguration%\
set testProject=%projectPath%.dll
set resultsFileName=%projectDirectory%Results\InitialTestResults

rem **********************************
rem Run XUnit
rem **********************************
%runner% %projectDirectory%%testProject% %traits% -Html "%resultsFileName%.html" -xml "%resultsFileName%.xml"

echo XUnit Console error level %errorlevel%

if %errorlevel% == 0 (
    GOTO :success
    )

if %errorlevel% == 1 (
    GOTO :success
)

exit %errorlevel%

:success
exit 0

