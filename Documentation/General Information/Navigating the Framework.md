# Navigating the Test Automation Framework

All test automation will begin with a test method. Test methods are found class files in the [LampsPlus.Automation.Tests namespace](../../LampsPlus.Automation.Tests/Tests) namespace.

Test classes are organized per functional areas as defined in test cases in the [requirements repository](https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/design?projectId=10172).

![](../Images/Navigating%20the%20Framework/Adaptavist.jpg)

## Test Method
One or more test methods will be created for each test case defined in the regression.

Test methods require the following

### Summary
All test methods require a summary with the following:
* Verbiage from the test case. "Verify that if a product is an LPPRODUCT and it is on sale, there's a 'Compare' callout."
* JIRA Task Link to the test automation issue in JIRA.
* Test Case Link to the test case in the requirements repository.
* Additional details about the test as necessary.

### Traits
Traits allow catagorizing test cases. Traits are required and should be added to all test cases as appropriate.
See the [Test Case Standards document](../Design%20Requirements/Test%20Case%20Standards.md) for guidance on using Traits.

### Test Case Attribute
All tests need to be decorated with the [SkippableFact] or [SkippableTheory] attribute to be picked up by the test runner.

### Test Structure
See the [Test Case Standards document](../Design%20Requirements/Test%20Case%20Standards.md) for guidance on using test class structure.

### Browser
The Browser class in the LampsPlus.Automation.Framework namespace which provides access to interact with a browser and provides common utilities such as Page, Locate, and Navigate.

### Database
Database interactions can be accessed through classes in the LampsPlus.Automation.Tests.Databases.Actions namespace.

Methods in the Actions namespace call queries in the LampsPlus.Automation.Tests.Databases.Queries namespace.

Information from the database is typically used in test case preconditions for example to find specific type of products and to verify expectations from the database as part of the test case assertion from the AAA pattern.

### Page Objects
Page objects are used to encapsulate "page" specific behavior. Page Objects are designed using the Page Object Model testing design pattern. Note a page does not necessairley represent an entire page.

Page Objects can be found in the LampsPlus.Automation.Tests.Pages namespace.

Page Objects can be created for sub sections of pages to benefit from code reuse.

In most cases page objects should not know about other pages.

Exceptions to this rule are called composite page objects. Composite page objects will use dependency injection to require necessary page objects to be passed into the composite page object construction.

### Workflows
Workflows are used when behavior spans multiple page objects. For example a utility method can be created for adding an item to the cart.

This is a flow that will span multiple pages, but is useful to define once since it will be used in many tests.

Workflows can be found in the LampsPlus.Automation.Tests.Workflow namespace.

### Utilities
Utilities are useful for providing general behavior and functionality that is not a Page Object or a Workflow.

User accounts used for automation (LampsPlus.Automation.Tests.Utilities.LoginType) is an example of a utility class.

``` C#
        /// <summary>
        /// Verify that if a product is an LPPRODUCT and it is on sale, there's a 'Compare' callout.
        /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/LP-16835
        /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T252
        /// </summary>
        [Trait(LpTraits.RequiredTestCaseTags.TaskId, "LP-16835"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T252")]
        [SkippableFact]
        public void VerifyCompareCalloutTest()
        {
            var productDataFromDb = ProductActions.GetLPProductOnSaleWithComparePrice();

            Assert.DatabaseObject(productDataFromDb, "GetLPProductOnSaleWithComparePrice()");

            Home.Navigate();
            Search.ExecuteSearch(productDataFromDb.ShortSku);
            // If window is small, brings the element into view for the screenshot
            Browser.MouseOverOnElement(ProductDetail.ComparePriceCallout);

            // If there is a 'Compare' text with a price displayed
            var comparePriceFromPage = GetComparePriceNumberFromPage();
            Verify.True(comparePriceFromPage != string.Empty, "Compare price appears on page");

            VerifyComparePricingLogic(productDataFromDb.ComparePrice, comparePriceFromPage);
        }
```
