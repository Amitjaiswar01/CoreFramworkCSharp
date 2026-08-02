# Lamps Plus Test Automation General Standards
This document details the code standards and preferences for general development.  

## External Imports
- .NET and System imports
- Automation.Framework
- LampsPlus.RegressionTests
- 3rd Party Libraries

### Test Case Example
using Automation.Framework;  
using LampsPlus.RegressionTests;    
using OpenQA.Selenium;  
using OpenQA.Selenium.Support.UI;

### General Class Example
using System;  
using System.IO;  
using LampsPlus.AutomationFramework.Utilities;     
using OpenQA.Selenium;  
using OpenQA.Selenium.Support.Extensions;  
using OpenQA.Selenium.Support.UI;

## Summaries
Only Test Scripts shall have a summary that explains the intent of the test. This includes the DesktopBase and MobileBase.

### Examples
``` C#
    /// <summary>
	/// Verify that all items with the 'Free Shipping and Free Returns' attribute persist to the PDP. 
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5323
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T222
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5323"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T222")]
	public abstract class T222_DesktopBase : T222_T456_Base
	{
		protected T222_DesktopBase(ITestOutputHelper output) : base(output) { }
	}


	/// <summary>
	/// Verify that all items with the 'Free Shipping and Free Returns' attribute persist to the PDP. 
	/// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5488
	/// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T456
	/// </summary>
	[Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
	[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5488"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T456")]
	public abstract class T456_MobileBase : T222_T456_Base
	{
		protected T456_MobileBase(ITestOutputHelper output) : base(output) { }
	}


	public abstract class T222_T456_Base : ProductDetailTestsBase
    {
        protected T222_T456_Base(ITestOutputHelper output) : base(output) { }

 	   protected void Validate(string config)
        {
            InitializeFramework(config, Urls.PdpFreeShippingReturnsUrl);

            var links = Sort.FindLinksForGivenNumberOfProductsOnSortPage(3);

            foreach (var link in links)
            {
                ProductDetail.Navigate(link);

                Verify.Displayed(ProductDetail.FreeShippingAndReturnElement, "The free shipping and return element was expected but not displayed on the screen.");
            }
        }
    }
```

