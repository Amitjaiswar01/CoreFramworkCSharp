# Daily Test Case Review
For test automation to be useful it is important that automated test cases are automated per requirements and are passing.
When tests fail they should be fixed as soon as possible.
When developers get used to seeing failed tests it becomes normal to not fix what the tests are telling us.

## Build Server
We are using Bamboo as our build agent. Test automation build plans can be found https://bamboo.lampsplus.com:8443/browse/TES.

## Daily Test Run
Currently, the only tests scheduled to run daily are our POM Integration tests. These run daily at 6:30AM PST.
The plan can be found [here](https://bamboo.lampsplus.com:8443/browse/TES-IT).

## Process
Each day the test results from the previous night should be analyzed and dispositioned.

### Latest Build Results

The latest build results can be found by going to the desired build plan and selecting the right most build results.

![](../Images/Daily%20Test%20Case%20Review/Build%20Job%20Icons.jpg)

### Build Results Summary
The Build result summary is helpful to get an overview of the build.
We are interested in New Failures, Existing Failures, and Skipped tests.
All failures and Skipped tests will require investigation.

![](../Images/Daily%20Test%20Case%20Review/Build%20Results%20Summary.jpg)

### Understanding Test Results
The easiest way to get context of tests is by viewing information in the Artifacts tab in Bamboo.

![](../Images/Daily%20Test%20Case%20Review/Build%20Artifacts.jpg)

Start by reviewing the Results HTML file. This will provide a human readable format of the results of the test run.

The results of each test, the execution log, and information about what happened can be seen here.

### Investigation Guidance
This section provides general rules for test case analysis.

#### Requires a Fix
Any test that fails due to an assertion failure is a candidate for fixing.

![](../Images/Daily%20Test%20Case%20Review/Test%20Failure%20Assertion.jpg)

Any test that fails due to an unhandled exception is a candidate for fixing.

This list includes but is not limited to:
* System.NullReferenceException
* System.ArgumentOutOfRangeException
* System.Exception

![](../Images/Daily%20Test%20Case%20Review/Test%20Failure%20Object%20Reference.jpg)

Any test that fails due to a timing issues is a candidate for fixing.

![](../Images/Daily%20Test%20Case%20Review/Test%20Failure%20Timing%20Issue.jpg)

Any test that fails due to parameter mismatch is a candidate for fixing.

This typically occurs when a test decorated with a SkippableTheory flag does not provide the correct number or order of arguments as defined in the method signature.

![](../Images/Daily%20Test%20Case%20Review/Test%20Failure%20Paramater%20Mismatch.jpg)

#### Possible False Positive

Any test that interfaces with the database and is getting information about products can potentially have data mismatches due to product syncing issues.

![](../Images/Daily%20Test%20Case%20Review/Test%20Failure%20Data%20Mismatch.jpg)
