# Lamps Plus Test Automation Mobile Architecture
This document details the architecture design to support the needs of Lamps Plus mobile automation.

## Business Use Cases
The design of the mobile automation must support the following high level use cases.
* Support for simulation to support local test development.
* Support for simulated device testing.
* Support for real device testing.
* Support for "desktop" and "mobile" view testing.

## Solution / Project Structure

### LampsPlus.AutomationFramework
![](../Images/Mobile%20Architecture/Lamps%20Plus%20Automation%20Framework%20Structure.jpg)

Page Object and Workflows are organized in view specific namespaces with a Base namespace for abstract base classes. Interface files shall be placed in the root namespace (Pages / Workflow)

![](../Images/Mobile%20Architecture/Page%20Object%20Folders.jpg) ![](../Images/Mobile%20Architecture/Workflow%20Folders.jpg)

### LampsPlus.RegressionTests
Test cases that are specific to **ONLY** a desktop implementation should be placed in the appropriate functional area folder (e.g. 'Homepage', 'Search', etc.) in the '**Desktop**' folder. 

Test cases that are specific to **ONLY** a mobile implementation should be placed in the appropriate functional area folder in the '**Mobile**' folder. 

Test cases that are identical (minus slight implementation details which can be handled with abstraction which is discussed below) should be placed into appropriate functional area folder within the '**Common**' folder.

![](../Images/Mobile%20Architecture/Lamps%20Plus%20Regression%20Tests%20Structure.jpg)

## Design Requirements

Generally speaking, the mobile architecture will extend the existing desktop architecture to add support for mobile device / view testing.

![](../Images/Mobile%20Architecture/Page%20Diagram.jpg)![](../Images/Mobile%20Architecture/Workflow%20Diagram.jpg)

Any entity (method, property, field, ...) in the abstract base that requires a different implementation for mobile view testing should override the implementation as appropriate.

### Page Object / Workflow Interfaces
Each Page Object and Workflow **shall** have an interface that will define all public entities for both desktop and mobile views.

Any new public entity added to a Page Object or Workflow **shall** require updating the associated interface with the required signature.

#### Page Object Interface Example

``` C#
namespace LampsPlus.AutomationFramework.Pages
{
	/// <summary>
	/// Common behavior between desktop and mobile views.
	/// </summary>
	public interface IContactUs
	{
        #region Class Setup
		IWebElement CategoryDropdown { get; }
		#endregion

		/// <summary>
		/// Log class to update log messages.
		/// </summary>
		Log Log { get; }

		/// <summary>
		/// Instance of a Browser to enable browser specific UI testing.
		/// </summary>
		IBrowser Browser { get; }

		/// <summary>
		/// Navigate to the given URL.
		/// </summary>
		/// <param name="url">URL to navigate to. This must be begin with http:// or https://.</param>
		void Navigate(string url);
	}
}

```

#### Workflow Interface Example

``` C#
namespace LampsPlus.AutomationFramework.Workflow
{
    /// <summary>
    /// Common utility methods.
    /// </summary>
    public interface ICommonWorkflow
    {
        /// <summary>
        /// Click Cancel button on mobile drawer.
        /// </summary>
        void CancelDrawer();

        /// <summary>
        /// Close a modal window.
        /// </summary
        void CloseLpModal();

        /// <summary>
        /// Click confirmation button on mobile drawer. 
        /// </summary>
        void ConfirmDrawer();
    }
}

```

### Page Object / Workflow Abstract Base

Each Page Object and Workflow **shall** have an abstract base class that implements the given interface.

Each Page Object and Workflow **shall** have both a desktop and mobile object (when applicable) which inherit from the abstract base.

Each Page Object and Workflow **shall** define the implementation for a given entity in the abstract base class when the behavior on desktop and mobile view is the same ``` public IWebElement CategoryDropdown => Browser.Locate.ElementById(EmailCategoryId); ```

Each Page Object and Workflow **shall** define an abstract entity in the abstract base class when the behavior on desktop and mobile views is different, or does not exist in one view. ``` public  abstract IWebElement CommentsInput { get; } ```

Page Object and Workflow public entities **shall** use ``` <inheritdoc /> ``` for summaries with the expectiopn of Class summaries. The summaries **shall** be defined in the interface.

#### Page Object Abstract Base Example

Notice the Page Object base class inherits ``` Page, IContactUs ```.

``` C#
namespace LampsPlus.AutomationFramework.Pages.Base
{
    /// <summary>
    /// Base class for common behavior between desktop and mobile views.
    /// </summary>
    public abstract class ContactUsBase : Page, IContactUs
    {
        #region CSS Selector Strings
        public static string EmailCategoryId => "EmailCategory";
        #endregion

        #region Page Elements
        public IWebElement CategoryDropdown => Browser.Locate.ElementById(EmailCategoryId);
		
        public  abstract IWebElement CommentsInput { get; }
        #endregion

        /// <inheritdoc />
        protected ContactUsBase(IBrowser browser) : base(browser) { }
    }
}

```

#### Workflow Abstract Base Example

Notice that the Workflow base class inherits from ``` WorkflowBase, ICommonWorkflow ```.

``` C#
namespace LampsPlus.AutomationFramework.Workflow.Base
{
    /// <summary>
    /// Common utility methods.
    /// </summary>
    public abstract class CommonWorkflowBase : WorkflowBase, ICommonWorkflow
    {
        /// <inheritdoc />
        protected CommonWorkflowBase(TestsBase testsBase) : base(testsBase) { }

        /// <inheritdoc />
        public abstract void CloseLpModal() {}
	}
}

```

### Page Object / Workflow (View Specific Page Objects)
* Page Object and Workflow entities are **not required** to add implmentation in view specific page objects when the behavior is the **same** in both views. The behavior will be inherited from the base class.

* Page Object and Workflow entities **shall** define view specific implementation when appropriate.

In the example below the implementation is different in both views. In desktop we find an element by ``` DesktopId ``` in mobile we find the element by ``` MobileId ```.

Notice the ``` override ``` qualifier is used in the entity definition.

``` C#
    /// <summary>
    /// Example Desktop Page Object.
    /// </summary>
    public class Sort : SortBase
    {
        public override IWebElement CategoryDropdown => Browser.Locate.ElementById(MobileId);
    }

    /// <summary>
    /// Example Mobile Page Object.
    /// </summary>
    public class MobileSort : SortBase
    {
        public override IWebElement CategoryDropdown => Browser.Locate.ElementById(DesktopId);
    }
```

* Page Object and Workflow entities **shall** ``` throw new NotImplementedException() ``` exceptions if the behavior is not available for a specific view.

The following example shows what to do if behavior exists on the desktop view but not on the mobile view.

``` C#
    /// <summary>
    /// Example Desktop Page Object.
    /// </summary>
    public class Sort : SortBase
    {
        public override IWebElement CategoryDropdown => Browser.Locate.ElementById(MobileId);
    }

    /// <summary>
    /// Example Mobile Page Object.
    /// </summary>
    public class MobileSort : SortBase
    {
        public override IWebElement CategoryDropdown => throw new NotImplementedException();
    }
```

The following example shows of how to build a mobile page object. This example applies to workflow construction as well.

``` C#
namespace LampsPlus.AutomationFramework.Pages.Mobile
{
    /// <summary>
    /// Example Mobile Page Object.
    /// </summary>
    public class MobileSort : SortBase
    {
        #region CSS Selector Strings
        public static string DivAttMenuId => "divAttMenu";
        #endregion

        #region Page Elements
        public override IWebElement ProductDescriptionLinksElement => Browser.Locate.ElementByClassName(SortResultProdNameClass);
        #endregion

        /// <inheritdoc />
        public MobileSort(IBrowser browser, Urls urls) : base(browser, urls) { }

        /// <summary>
        /// Search the page for the given sku.
        /// </summary>
        /// <param name="sku">SKU to find on the page.</param>
        public void SearchPageForSku(string sku)
        {
            // Implementation here.
        }

        /// <summary>
        /// Meaningful summary.
        /// </summary>
        public void ClickHamburgerMenu()
        {
            // Implementation here.
        }
    }
}
```
**NOTE:** For a practical example of all of the above, please look at the "HomeLocatorTests.cs" file located at \LampsPlus.IntegrationTests\Pages\Home\.

#### Page Object / Workflow Organization

* Desktop Page Objects / Workflows inherit from the abstract base object.

``` C#
    public class Sort : SortBase
```

* Mobile Page Objects / Workflows inherit from the abstract base object.

``` C#
    public class MobileSort : SortBase
```

* Page Object / Workflow behavior is the same on the desktop and mobile view.




* Mobile Page Objects / Workflows will override the default behavior in cases where the same beahvior is needed in desktop and mobile but mobile needs a different implementation.
Notice the **use of** the ```override``` keyword to denote this is replacing the desktop implementation with something else.

``` C#
    public static string DivAttMenuId => "mobileDivAttMenu";
	
    public override List<IWebElement> ListOfFilterAttributes => Browser.Locate.ElementById(DivAttMenuId)?.FindElements(By.ClassName(SortAttMenuBtnContainerClass)).ToList();
	
	/// <summary>
    /// Search the page for the given sku.
    /// </summary>
    /// <param name="sku">SKU to find on the page.</param>
    public override void SearchPageForSku(string sku)
    {
        // Implementation here.
    }
```

### Abstract base Page Object / Workflow Construction
For any entity that is overridden in the Mobile Page Object (e.g needs a different implementation), the base page object will need to be modified using the ```abstract``` keyword.
The desktop implementation should be moved to the desktop object and the mobile implementation can be added to the mobile object.

``` C#
    public abstract List<IWebElement> ListOfFilterAttributes => Browser.Locate.ElementById(DivAttMenuId)?.FindElements(By.ClassName(SortAttMenuBtnContainerClass)).ToList();
	
    /// <summary>
    /// Search the page for the given sku.
    /// </summary>
    /// <param name="sku">SKU to find on the page.</param>
    public abstract void SearchPageForSku(string sku)
    {
        // Implementation here.
    }
```

### 3rd Party Dependencies
To support mobile automation, we use NuGet to manage 3rd party dependencies whenever possible, which is the same as desktop.
The only additional library needed to enable mobile automation is Appium. This library is added to all projects in the solution that require this support.
**NOTE:** Changes to versions need to be synced and verified with compatible Selenium versions.
Appium is used as a bridge to real and simulated mobile devices, but Selenium will still be used for mobile automation as we are automating a browser on a mobile device and not doing native app testing at this time.

``` C#
    <Reference Include="appium-dotnet-driver, Version=3.0.0.2, Culture=neutral, processorArchitecture=MSIL">
      <HintPath>$(SolutionDir)\packages\Appium.WebDriver.3.0.0.2\lib\net45\appium-dotnet-driver.dll</HintPath>
    </Reference>
```

## Automating a mobile test

Mobile elements can be located using the '**Automating A Mobile Test**' documentation in the 'General Information' folder.

## Test Life Cycle

We will discuss the mobile test lifecycle in detail to explain the general program flow for mobile automation.
Below we have a simple test.

``` C#
    /// <summary>
    /// Tests to ensure all IWebElements and Lists of IWebElements can be found on the given page object.
    /// </summary>
    [Trait(LpTraits.Category, LpTraits.Integration)]
    public class MobileDriverTests : TestsBase
    {
        /// <summary>
        /// Tests to ensure this page can find all its IWebElements.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public MobileDriverTests(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the requested Finish filter elements could be located on the given sort page.
        /// </summary>
        [SkippableTheory]
        [InlineData(TestConfiguration.iPadiOS11_Safari_SNIS_UNSI)]
        public void MobileInitTest(string config)
        {
            InitializeFramework(config, Urls.AllChandeliersSortPageUrl);

            Browser.Navigate(Urls.HomePageUrl);
        }
    }
}
```

### Test Configuration
Mobile tests, like desktop tests, start by passing a test configuration string as InlineData to the test.
In this example we are using an iPad device which is a **mobile device**, which is not to be confused with a **mobile-view**.
The OS, Browser, and user role for the test are automatically configured by using the configuration strings defined in the TestConfiguration.

``` C#
[InlineData(TestConfiguration.iPadiOS11_Safari_SNIS_UNSI)]
```
### Driver Configuration
As part of the framework init ``` InitializeFramework(config, Urls.AllChandeliersSortPageUrl) ``` the mobile driver, using Appium, will be configured and used
based on the configuration provided.

``` C#
        /// <summary>
        /// Initialize a Safari driver for testing.
        /// </summary>
        /// <returns></returns>
        internal static AppiumDriver<AppiumWebElement> InitializeSafariDriver()
        {
            var realiPadId = "9e068a4e9d50810a7f0425a42f2918f25e8d6efd";
            var simulatediPadId = "9744E78B-CBCB-4761-B346-B9C2F2680C81";

            DesiredCapabilities cap = new DesiredCapabilities();
            cap.SetCapability("automationName", "XCUITest");
            cap.SetCapability(MobileCapabilityType.DeviceName, "iPad");
            cap.SetCapability(MobileCapabilityType.Udid, simulatediPadId);
            cap.SetCapability(MobileCapabilityType.BrowserName, "Safari");
            cap.SetCapability(MobileCapabilityType.PlatformName, "iOS");
            cap.SetCapability(MobileCapabilityType.PlatformVersion, "12.1");

            return new IOSDriver<AppiumWebElement>(new Uri("http://[Appium Server IP Address:Port]/wd/hub"), cap);
        }
```

### Test Case Flow
The test case will execute as described in the same way as a desktop executes. The main difference to note is in the driver configuration.
_NOTE: Selection of the browser will be configured by the Selenium Grid and the documentation will be updated with the relevant information
after the mobile Selenium grid has been configured._

