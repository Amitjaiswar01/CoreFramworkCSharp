# Lamps Plus Test Automation Test Class Standards
This document details the code standards and preferences for test class development.  

## Automation Test Generator Tool

AutomationTestGenerator.exe is a Windows Application that creates automation tests using a specified template. It was created in an attempt to minimize repetitive work and reduce errors.

The application can be found on \\\lpvmdata\WebDevData\Data\Automation\Automation Utilities\AutomationTestGenerator_v1

![](../Images/Design%20Requirements/AutomationTestGenerator.png)

To use it, **copy the files to your desktop** and run AutomationTestGenerator.exe.

Input the following values, and then click "Generate File" to generate a file containing the selected tests:

* Desktop Jira task key
* Desktop Adaptavist test ID
* Mobile Jira task key
* Mobile Adaptavist test ID
* Description
* Method Name
* Namespace
* Test Class Base
* Selected supported device configuration

The file is outputted in the same directory as the application. Address any TODO comments in the file, such as:
* Copy over the correct using statements
* Copy over the base class code implementation
* Add CRUD, DBCLUST Only, or DBTEST Only attribution if neeed
* If test has Settings.IsMobileView flag, then create an abstract method and move logic to the appropriate desktop/mobile base classes


## Adding a New Test Case Class to the Framework
In the event that it becomes necessary to add a brand new test case to the framework, please follow these guidelines:
1. The test case class should be added to the corresponding folder in the framework to the one in Adaptavist. For instance, if adding a test case from the "**Certona**" folder in Adaptavist, the test case class must be added to the Certona folder in the framework.

2. When naming the test case class, please use the following format:
* The beginning of the class file should take the Adaptavist test case ID number minus the "**LP-**" and be followed by an underscore. For instance, if the Adaptavist test case "**LP-T123**" is being added to the framework, the beginning of the class file name should be "**T123_**". 
* Follow the underscore with a meaningful name starting with the word "**Verify**". For instance, "**VerifyCertonaSchemaForHomepage**".
* All together, the test class name should look like the following: "**T123_VerifyCertonaSchemaForHomepage**".

3. In most cases the above will suffice, but there are a few instances where there are multiple user roles being tested for each test. In these cases, it is important to differentiate these tests from others by appending the word "**Tests**" to the test class name. For instance, if there's a test in Adaptavist with test case ID "**LP-T456**" that tests Certona on a Sort page that also needs to be executed for an **Anonymous user**, an **Employee** and a **Professional account**, the test class name would be "**T456_VerifyCertonaForSortPageTests**".

## Adding a Common Desktop/Mobile Test Case Class

When adding a test case that is the same for desktop and mobile, please follow these guidelines:

1. If the mobile and desktop test scripts are identical, then create the test script in the **Common** folder. Otherwise, the tests should be created in the **Desktop** and **Mobile** folders respectively.

    ![](../Images/Design%20Requirements/Regression%20Test%20Folder%20Structure.jpg)

2. Use the following format:
* Put the code for both tests in the same file. 
* The file name will include **BOTH** Adaptavist identifiers in the name separated by an underscore, where the Desktop Adaptavist identifier is listed first. For example, T109_T393_VerifyItemOnSalePriceInShoppingCart, where T109 is the desktop Adaptavist identifier and T393 is the mobile identifier.
* The desktop and mobile tests will inherit from a common desktop or mobile base class, which in turn is derived from an abstract base class containing any common logic and the actual test script. See example below.
* The common base class naming convention is: 
  * [Adaptavist Desktop Identifier]\_[Adaptavist Mobile Identifier]\_Base
* The common desktop and mobile naming conventions are:
   * [Adaptavist Desktop Identifier]\_DesktopBase
   * [Adaptavist Mobile Identifier]\_MobileBase
* _Note: the InitializeFramework() call should be  in the common Base class._
 
	###### _For additional context please see https://lampstrack.lampsplus.com:8443/browse/ACD-6788 which was updated to the new standard for desktop and mobile_.
 
```C#
   namespace LampsPlus.RegressionTests.Common.ShippingInfo
   {
      public class T168_Windows_VerifyShippingInfoFormUsesValidation : T168_DesktopBase
      {
          public T168_Windows_VerifyShippingInfoFormUsesValidation(ITestOutputHelper output) : base(output) { }

          [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
          [SkippableTheory]
          [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
          public void ShippingInfoFormUsesValidation(string config) => Validate(config);
      }


      public class T418_iPhone_VerifyShippingInfoFormUsesValidation : T418_MobileBase
      {
          public T418_iPhone_VerifyShippingInfoFormUsesValidation(ITestOutputHelper output) : base(output) { }

          [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTwelvePhone)]
          [SkippableTheory]
          [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
          public void ShippingInfoFormUsesValidation(string config) => Validate(config);
      }


      ...


      /// <summary>
      /// Verify the validation for all required fields on the Shipping Page.
      /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5190
      /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T168
      /// </summary>
      [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
      [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5190"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T168")]
      public abstract class T168_DesktopBase : T168_T418_Base
      {
          protected T168_DesktopBase(ITestOutputHelper output) : base(output) { }
      }


      /// <summary>
      /// Verify the validation for all required fields on the Shipping Page.
      /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5524
      /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T418
      /// </summary>
      [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
      [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5524"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T418")]
      public abstract class T418_MobileBase : T168_T418_Base
      {
          protected T418_MobileBase(ITestOutputHelper output) : base(output) { }
      }


      public abstract class T168_T418_Base : ShippingInfoTestsBase
      {
          protected T168_T418_Base(ITestOutputHelper output) : base(output) { }

          /// <summary>
          /// Verify the validation for all required fields on the Shipping Page.
          /// </summary>
          protected void Validate(string config)
          {
             InitializeFramework(config);
             ...
          }
      }
   }

```

# New Test Class Standard Update

Testing of real devices will soon be supported on the Selenium Grid. To prepare for this we need to update the current tests so that we can run the correct test configurations in Bamboo.

Updates have been done for “Common\ShippingInfo\T168_T418_VerifyFormUsesValidation.cs”, so it will be used for the example.

## Step  1: Initial Updates

*	Remove unnecessary comments  specifically the following:
    * /// < inheritdoc />
    * /// See < see cref="T168_T418_Base.VerifyShippingInfoFormUsesValidation"/> for details.

* Update the test class names to the following name format [Adaptavist_Id]\_[Device]\_[TestDescription]
    * Note: Tests with Windows_ChromeMobileView_SNIS_UNSI are emulator tests and should have the [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)] trait
    * Naming examples:
      * T168_Windows_VerifyShippingInfoFormUsesValidation
      * T168_Mac_VerifyShippingInfoFormUsesValidation
      * T168_iPad_VerifyShippingInfoFormUsesValidation
      * T418_iPhone_VerifyShippingInfoFormUsesValidation
      * T418_AndroidPhone_VerifyShippingInfoFormUsesValidation
      * T418_Emulator_VerifyShippingInfoFormUsesValidation

    _Information on OS and Browser for testing can be found here https://confluence.lampsplus.com:8093/display/QA/OS+and+Browser+for+Testing_
_
* Rename common base class from CommonBase to Base  
    * e.g. T168_T418_CommonBase to T168_T418_Base

* Create two new inherited classes from the common base class for desktop and mobile which inherit from the common base class.
    * The desktop and mobile common base naming conventions are:
        * [Adaptavist Desktop Identifier]_DesktopBase
        * [Adaptavist Mobile Identifier]_MobileBase
    * Move category attribute for desktop/mobile, and JIRA task attribution to the new  desktop and mobile  common base classes
    * Move JIRA comments to correct desktop and mobile base class to the new  desktop and mobile  common base classes
    * These 2 new classes should be added right above the existing common base class.
    * Currently, the existing common base has a method that every test calls to perform its validation. It is named based on the test description. Rename that method to be called Validate().


![](../Images/Design%20Requirements/T168BaseClassExample.jpg)


* Update any existing tests to use the new classes, where they should now inherit from desktop or mobile base classes respectively.
* Rename Test() method on each existing test to be the meaningful name that was used for the filename,  without the test Adaptavist number and without the ‘Verify’.
  * If method is only one line, expression body syntax should be used 
  * E.g. public void ShippingInfoFormUsesValidation(string config) => Validate(config);
* If there is any if/else IsMobileView in common base class, create an abstract method, and then put the logic in the correct derived desktop or mobile common base class
  * For example, the ShipToDifferentAddress method below

    ```
    public abstract class T169_T419_CommonBase : ShippingInfoTestsBase
    {
        protected T169_T419_CommonBase(ITestOutputHelper output) : base(output) { }

        ... 

        public void ShipToDifferentAddress()
        {
            if (!Settings.IsMobileView)
                CustomerInformation.ShipToDifferentAddressButton.Click();
            else
                CustomerInformation.ShippingAddressInfoContainer.Click();
        }        
    }
    ```

    is refactored so the implementation is moved to desktop and mobile common base classes respectively:

    ```
	public abstract class T169_DesktopBase : T169_T419_Base
	{
		protected T169_DesktopBase(ITestOutputHelper output) : base(output) { }

		public override void ShipToDifferentAddress() => CustomerInformation.ShipToDifferentAddressButton.Click();
	}

	public abstract class T419_MobileBase : T169_T419_Base
	{
		protected T419_MobileBase(ITestOutputHelper output) : base(output) { }

		public override void ShipToDifferentAddress() => CustomerInformation.ShippingAddressInfoContainer.Click();
	}

	public abstract class T169_T419_Base : ShippingInfoTestsBase
	{
		protected T169_T419_Base(ITestOutputHelper output) : base(output) { }
        ...

        public abstract void ShipToDifferentAddress();            
    }
    ```


## Step 2: Adding New Configurations

* Open the desktop and mobile tests cases in Adaptavist 
* Verify Adaptavist attribution and create any tests for missing configurations
* Not all tests have every configuration
* Any new test configurations should use the naming convention mentioned above
* When a test has a CRUD label in Adaptavist, then add the CRUD category to the common base class

![](../Images/Design%20Requirements/AdaptivistCRUD.jpg)

![](../Images/Design%20Requirements/T169CRUD.jpg)

&nbsp;
* If a class has multiple user roles, there should be separate tests with the correct attribution 


![](../Images/Design%20Requirements/AdaptavistUserRoleAttribution.jpg)
 
![](../Images/Design%20Requirements/T169UserRoles.jpg)


## Step 3: Verify

After the above updates are done, validate the following **BEFORE** sending to code review.

* Re-verify Adaptavist attribution (device types, OSes, labels, collections)
* The Test Case ID in the framework matches the ID of the test in Adaptavist
* The test case summary is the same between Adaptavist and the framework
* All test classes and base classes are inheriting from the correct base class
* Double check appropriate spacing between groups of usages at top of file
* There are 2 spaces in between all classes
* The common base class is the last class in the file
* There is 1 space at the end of the file


## Test Class Description
 The general format is Tests to ensure ***UPDATE*** are working as expected.  
 Note: In most cases the class constructor can have the same summary.  
 Additional details can be added as necessary.

## Traits
Traits allow our test runner (xUnit) to execute behavior based on how a method is tagged.  
Additional traits can be added to a method as needed.

## Test Case Order
Test cases will be ordered by the Trait(LpTraits.RequiredTestCaseTags.TaskId, "LP-XXXXX") trait in ascending order.

## Design Patterns
### Arrange Act Assert (AAA)
Test cases should be logically organized using the AAA visual design pattern. Basically there should be a space between logical groups in the test case.

* Arrange - Setup and preconditions of the test.
* Act - Behavior of the test.
* Assert - Expectation(s) of the of the test. In framework language this is a Assert statement.

If an Assert statement fails the test will stop execution at the failure and log the test as a failure.
