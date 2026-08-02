# Lamps Plus Test Automation Test Case Standards
This document details the code standards and preferences for test case development. 

## Test Case Naming
Just like the test class name which requires an underscore in the name, WITHIN the class, the test class name will **_ALSO_** have an underscore in it. Taking an example from the ProductDetail folder, the test case "**LP-T221**" (for Desktop) and "**LP-T455**" (for Mobile) in Adaptavist has a test class name of "**T221_T455_VerifyFreeShippingOnProduct**" in the framework. Within the actual class file, it would look like:
```C#
namespace LampsPlus.RegressionTests.Common.ProductDetail
{
	public class T221_Windows_VerifyFreeShippingOnProduct : T221_DesktopBase
	{
		public T221_Windows_VerifyFreeShippingOnProduct(ITestOutputHelper output) : base(output) { }
		
		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
		[SkippableTheory]
		[InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
		public void FreeShippingOnProduct(string config) => Validate(config);
	}
	
	
	public class T455_iPhone_VerifyFreeShippingOnProduct : T455_MobileBase
	{
		public T455_iPhone_VerifyFreeShippingOnProduct(ITestOutputHelper output) : base(output) { }

		[Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTwelvePhone)]
		[SkippableTheory]
		[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
		public void FreeShippingOnProduct(string config) => Validate(config);
	}
```
Please note the underscores in the public class name and the constructor.

## Test Case Description
As a general rule use the description in the JIRA test case.

### Required
- All test scripts must have a summary block. "///" above the class in Visual Studio.
- Test class summaries end with periods.  
- Test Cases will not contain logic. All logic should be moved to page objects, page object bases, workflows, or utilities.

### Optional
- Additional information can be added to the summary to provide context.

## Traits
Traits allow our test runner (xUnit) to execute behavior based on how a method is tagged.  
Additional traits can be added to a method as needed.

### Required
- The JIRA issue ID  
Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-XXXXX")
- The Test Case ID  
Trait(LpTraits.RequiredTestCaseTags.TId, "LP-TXXXXX")  
- The Category  
Trait(LpTraits.Keys.Category, LpTraits.Suite.XXXXX)
- Traits will be ordered in the following way:  
Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-XXXXX"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-TXXXXX")
```C#
[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-12345"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T123"), Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
```

### Optional
Any additional traits will be come after the **Test Case ID** trait mentioned in the **Required** section above.

Example:
- The Test Category  
Trait(LpTraits.Keys.Category,  LpTraits.Categories.CRUD)
- Traits will be ordered in the following way:  
Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-XXXXX"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-TXXXXX"), Trait(LpTraits.Keys.Category, LpTraits.Categories.CRUD)
```C#
[Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-12345"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T123"), Trait(LpTraits.Keys.Category, LpTraits.Categories.CRUD)]
```
**"CRUD"** stands for Create, Read, Update and Delete. A test that involves any of these four functions is considered a CRUD test, provided it is being executed as a logged-in user.

**Note:** Ignore any items that don't apply to a specific case.

## Executing Tests Multiple Times Before Putting Tasks in Test Case Review status
After automation for a test case is complete, the related task must be put in ***Test Case Review*** status. But before transitioning a task to ***Test Case Review*** status, the test must be successfully run 10 times and the screenshot for the successful runs must be attached to the task.

Please see **BEFORE** and **AFTER** screenshots below on to how to configure a test to run 10 times:

**FUNCTIONAL TEST**

**BEFORE** - *test runs only once*

![](../Images/Design%20Requirements/functional-before.png)

**AFTER** - *test runs 10 times*

![](../Images/Design%20Requirements/functional-after.png)

Functional test repeats are controlled with attribute (pass your test config as parameter):
```C#
[MemberData(nameof(RepeatFunctionalTest), TestConfiguration.Windows_Chrome_SNIS_UNSI)]
```

After making changes as shown in **AFTER** above, when the test is executed, it will run 10 times consecutively. The Unit Test Sessions window in Visual Studio will appear as shown below:

![](../Images/Design%20Requirements/10SuccessfulRuns.jpg)

**Note:** Once a test has successfully run 10 times, the Test Configuration must be reinstated to the **BEFORE** state. The task can then be placed in ***Test Case Review*** status.
Number of functional test repeats is controlled in RepeatFunctionalTest() method.

**VISUAL TEST**

**BEFORE** - *test runs only once for Baseline and Target*

![](../Images/Design%20Requirements/visual-before.png)

**AFTER** - *test runs 10 times for Baseline and Target*

![](../Images/Design%20Requirements/visual-after.png)

Visual test repeats are controlled with attribute (pass your test Baseline and Target configs as parameters):
```C#
[MemberData(nameof(RepeatVisualTest), TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline, TestConfiguration.Windows_Chrome_SNIS_UNSI)]
```
**Note:** Number of visual test repeats is controlled in RepeatVisualTest() method.
