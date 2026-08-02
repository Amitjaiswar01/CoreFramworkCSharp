# Verify
The Lamps Plus Web Tests solution provides 3 different types of verification.

**Current Supported verification methods**
* Verify True
* Verify False
* Verify Equals
* Verify InRange
* Verify DatabaseObject
* Verify TextLink
* Verify PageUrl
* Verify StringContains
* Verify Displayed
* Verify NotDisplayed
* Verify Condition

## Verify (default verify)
This type has the default verify behavior.
In case the verify condition fails. The system will stop running the test case and mark it as failed

**Sequence**
	If the condition is met successfully 
		There is no interruption to the test case.
	Else
		Throw an exception to stop the test case with failure output. 

**Sample Code**
	Verify.True(true, "Show this message if the condition was false");

## Conditional Verify (skippable verify)
Conditional Verify allows more flexibility in the check. It will work only on the test cases that has skippable tag.
The main idea behind conditional verification that is the condition failed it will mark the test case as skipped.  

**Sequence**
	If the condition is met successfully 
		There is no interruption to the test case.
	Else
		Throw an exception and mark the test case as skipped. 

**Sample Code**
	ConditionalVerify.True(true, "Show this message if the condition was false");

## Soft Verify (verify all)
Soft verify gives the ability to run all the verify statements in the test case before it fail. 
Example: if your code has 10 verify statements, and statement number 5 fails. The system will run the 10 verify stamtements then notify the user that statement number 5 is failing.

**Sequence**
	If the condition is met successfully 
		There is no interruption to the test case.
	Else
		Add the failing to result and continue to the next statement.
	
	At the end of the test case if one or more statement failed: user will be notified.

**Sample Code**
	SoftVerify.True(true, "Show this message if the condition was false");
