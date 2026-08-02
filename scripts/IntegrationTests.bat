@echo off

rem ************************
rem Configure test settings
rem ************************
set buildConfiguration=ReleaseGrid
set projectPath=LampsPlus.IntegrationTests
set traits=-trait "Page Object Model=BillingInfo" -trait "Page Object Model=ContactUs" -trait "Page Object Model=CreateAccount" -trait "Page Object Model=Email" -trait "Page Object Model=EmployeeOrderLookup" -trait "Page Object Model=GlobalLocators" -trait "Page Object Model=HeaderFooter" -trait "Page Object Model=Home" -trait "Page Object Model=ManageAccount" -trait "Page Object Model=OrderConfirmation" -trait "Page Object Model=OrderDetails" -trait "Page Object Model=OrderHistory" -trait "Page Object Model=OrderSummaryBlock" -trait "Page Object Model=ProductDetail" -trait "Page Object Model=AugmentedReality" -trait "Page Object Model=Search" -trait "Page Object Model=HeaderSignIn" -trait "Page Object Model=ShippingInfo" -trait "Page Object Model=CsrBlock" -trait "Page Object Model=ShoppingCart" -trait "Page Object Model=SignIn" -trait "Page Object Model=SortBucket" -trait "Page Object Model=SortFullPageCertona" -trait "Page Object Model=Sort" -trait "Page Object Model=SortPla" -trait "Page Object Model=Stores" -trait "Page Object Model=PayPal" -trait "Page Object Model=Wishlist"

rem **********************************
rem Configure test paths and runner
rem **********************************
set runner=..\packages\xunit.runner.console.2.4.1\tools\net472\xunit.console.exe
set projectDirectory=..\%projectPath%\bin\%buildConfiguration%\
set testProject=%projectPath%.dll
set resultsFileName=%projectDirectory%Results\InitialTestResults

rem **********************************
rem Run XUnit
rem **********************************
%runner% %projectDirectory%%testProject% %traits% -Html "%resultsFileName%.html" -xml "%resultsFileName%.xml"

exit 0
