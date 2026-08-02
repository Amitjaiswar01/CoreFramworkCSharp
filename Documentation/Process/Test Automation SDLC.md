# Lamps Plus Test Automation Software Development Lifecycle (SDLC)
This document describes the process for the development of Lamps Plus Test Automation.

## Project Management
All tasks to support the Lamps Plus test automation effort can be visualized on the project Kanban board located https://lampstrack.lampsplus.com:8443/secure/RapidBoard.jspa?rapidView=215.


![](../Images/Test%20Automation%20SDLC/Test%20Automation%20Kanban.jpg)

### Board Columns

#### On Hold
On Hold status is used when a task is has started development, but is not currently being worked on.
When transitioning a JIRA issue to On Hold a comment should be added with the reason a task is not currently being worked on.

#### Selected for Development
JIRA issues in the Selected for Development column have been groomed and are ready for development.

#### Rework
JIRA issues in the Rework column are in active development and have failed one or more of the QA states (Test Case Review, Technical Review, or Stakeholder Acceptance).

Rework issues should contain the following:

##### Summary "Rework (Original JIRA Test Automation Issue ID)"

![](../Images/Test%20Automation%20SDLC/Rework%20Summary.jpg)

##### Issue Type is Test Automation

![](../Images/Test%20Automation%20SDLC/Test%20Automation%20Issue%20Type.jpg)

##### Component/s One or more components will be added to all rework items

In the case that rework is required do to a failing test (not requirements related), the **Automated Test Case Failure** component will be added to the rework task.

![](../Images/Test%20Automation%20SDLC/Test%20Automation%20Rework%20Failure%20Component.jpg)

In the case that rework is required do to a failing test because site behavior has changed, the **Test Case Update** component will be added to the rework task.

![](../Images/Test%20Automation%20SDLC/Test%20Automation%20Rework%20Test%20Update%20Component.jpg)

##### Description

The description field should have actionable information on what needs to be fixed for the rework item. Typically this is a link to one or more Bamboo builds with logs relevant to one or more failures.

![](../Images/Test%20Automation%20SDLC/Test%20Automation%20Rework%20Description.jpg)

##### Issue Links

Links added to the original test automation task as well as other relevant issues if applicable.

![](../Images/Test%20Automation%20SDLC/Test%20Automation%20Rework%20Issue%20Links.jpg)


##### Original Estimate

All rework items have an original estimate of 1h. Please review the task and estimate accordingly.

![](../Images/Test%20Automation%20SDLC/Original%20Estimate.jpg)

##### Rework Acceptance Criteria
Once the test in rework has been resolved, Evidence the test is passing 10+ times will be added to the task to prove the test is stable on the Grid.

Note any observations as comments in the task. This could be information specific to a browser, or environment observations, or general information to understand the context of the fix.

This is best done by attaching the test logs found in the location where the test automation repo was cloned at the following location relative to the build configuration.

test-automation\LampsPlus.Automation.Tests\bin\BUILD_CONFIGURATION\Logs
* BUILD_CONFIGURATION is the name of the configuration being ran, ReleaseGrid, ReleaseLocal, DebugGrid, DebugLocal.

![](../Images/Test%20Automation%20SDLC/Logs%20Example.jpg)

#### Requirements Support Needed
JIRA issues in the Requirements Support Needed have open questions about the requirements, or suggestions to improve the requirements for automation.

Any tasks put in this column will include a comment beginning with @Adam @Jaya and information about the support that is needed for the task.

Tasks may be placed in this column for reasons that include but not limited to the following:
* Questions about the intent of the test case
* Suggestions on optimizing the test case
* Updating Database queries
* Combining similar test cases
* General Questions

#### Technical Support Needed
JIRA issues in the Technical Support Needed column indicate that technical help is needed to automate the task.

This includes but is not limited to framework support that is not currently present in the framework, advice on design approach, or general automation help.

Any tasks put in this column will include a comment beginning with @Mike Black and information about the support that is needed for the task.

Tasks may be placed in this column for reasons that include but not limited to the following:
* Answer questions about TA Infrastructure tasks
* Framework and Architecture considerations
* Help resolving test flakiness
* Help resolving concurrency issues
* Help resolving browser specific issues
* General Questions

#### In Progress
JIRA issues in the In Progress state shows the tasks that are currently in active development.

#### Test Case Review
JIRA issues in the Test Case Review status have been developed per the test cases and are ready to be reviewed against the requirements.

NOTE: This status applies to Test Automation tasks **ONLY**. TA Infrastructure tasks should be transitioned to the Technical Review status.

#### Technical Review
JIRA issues in the Technical Review status have been developed to established standards and is ready to be reviewed.

#### Stakeholder Test
JIRA issues in the Stakeholder Test status are ready to be verified against the desktop-conversion branch.

NOTE: This is a last sanity check to ensure there were no issues with the merge after the code has been integrated.

#### Failed Statuses
Tasks will be put into a failed status for the following reasons:

- Tasks transitions that do not follow the established Test Automation Software Development process.
- Tasks that do not adhere to established standards and styles.
- Tasks that do not meet ALL established acceptance criteria defined in the task description.
- Test Automation tasks that are not reliable (intermittent failures).

#### Hidden Statuses
The Test Automation Kanban board does not visualize tasks that have not been groomed, or are not ready to be developed.

Tasks that have been completed and passed the Stakeholder Test will not be visualized on the board.

The intent is to keep the board clean to show only the work that is currently planned for development.

### Test Automation JIRA Task Types
Lamps Plus Test Automation uses the following JIRA issue types by default, other issue types may be used for Documentation or Research:

Test Automation tasks ![](../Images/Test%20Automation%20SDLC/Test%20Automation%20Task%20Icon.jpg) represent test cases to be automated per established requirements.

TA Infrastructure tasks ![](../Images/Test%20Automation%20SDLC/TA%20Infrastructure%20Task%20Icon.jpg) are any task that is not automating a test case per requirements.

### QA Automation Chat Room
We use the QA Automation Chat room in Stride to share information for test automation.

NOTE: All team members will be added to the room to ensure all shared information is accessible to all team members.

## Software Development Process
Test Automation work is not actively managed within the test automation project. Test automation work will be planned and assigned and managed within the teams development process (Sprint or Kanban).

The test automation Kanban board is ranked, meaning that the most important work can be found at the top of a given column. Generally speaking, the Selected for Development column is the only column which is actively managed and ranked.

When looking for work for test automation, work should first be pulled from the Rework column. If there are no tasks in the Rework column work will be pulled from the top of the Selected for Development column.

In the event a test automation resource is not comfortable working on one or more tasks at the top of a column, the resource can select the highest rank task that can be done by the given resource.

This may be the case for some TA Infrastructure tasks. As a general rule anyone should be able to work on any Test Automation task.

Test automation resources, will assign and work on one task at a time. In cases where support is needed additional work can be started.

Test automation resources will only assign work that is not currently assigned to another resource. In some cases work can be reassigned, but this will require an agreement from the original task owner.

### General Requirements for Test Automation and TA Infrastructure Tasks
This section details the requirements and process common for all test automation work.

#### Description
All tasks to support test automation require a description with an explanation of the task under the Description and information about how the task will be verified in the Acceptance Criteria.

NOTE: For Test Automation tasks the task is described in the description as a link to the details of the test case.

![](../Images/Test%20Automation%20SDLC/Jira%20Task%20Description.jpg)

#### Original Estimate
An original estimate should be established after reviewing the task and before beginning work on the task. Depending on the team process, this may be done during team grooming sessions.

![](../Images/Test%20Automation%20SDLC/Jira%20Task%20Original%20Estimate.jpg)

NOTE: This estimate includes the time to investigate and understand the task, development time, and time to review the work.

#### Assignee
The task must be assigned to the resource that will work on the task. This resource is now the task owner until the task has entered a resolved state (Stakeholder Acceptance).

![](../Images/Test%20Automation%20SDLC/Jira%20Task%20Assignee.jpg)

#### Resource Group
The team of the development resource must be updated in the Resource Group field.

![](../Images/Test%20Automation%20SDLC/Jira%20Task%20Resource%20Group.jpg)

#### Resource Queue
All tasks to support test automation will use **Automation** for the Resource Queue.

![](../Images/Test%20Automation%20SDLC/Jira%20Task%20Resource%20Queue.jpg)

### Test Automation Tasks
Before starting automation, read the requirements for the task and make sure you understand what the test is trying to accomplish.
If there are any questions or observations about the task, the task should be put in the Requirements Support Needed status. Depending on the support needed development may begin while awaiting answers on the requirements.
If the questions will dramatically change the test case it may be better to wait for information on requirements before beginning or continuing work on the task.

Once the intent of the test is understood, test case development can begin.
In the event that after development has started, technical help is needed, or the task is larger than originally estimated, the task can be placed in Technical Support Needed.
In some cases we may decide to create specific tasks for architecture work to support test automation tasks.

When all work for the task is production ready, committed and pushed to the test automation repo, a Pull Request will be created for the task. The task should be pull requested against the desktop-conversion branch unless otherwise specified.

At this point the task is out of the responsibility of the task owner unless there are observations from review. In this case, the original task owner will resolve the outstanding observations.

### TA Infrastructure Tasks
Before starting work on a TA Infrastructure task, ensure the intent of the task and the acceptance criteria is understood. If the intent of the task is not clear, ask Mike Black for clarification before beginning work.
In the event the task is not well understood or there are technical hurdles that were not accounted for before development has begun, description, scope, and acceptance criteria can be modified with approved and documented discussion with Mike Black.

Once the task is production ready, committed, and pushed to the test automation repo, a Pull Request will be created for the task. The task should be pull requested against the desktop-conversion branch unless otherwise specified.

## Pull Requests
Test Automation will use the same process as LP development for pull requests. The process is defined https://confluence.lampsplus.com:8093/display/TA/Perform+a+code+review+with+Bitbucket+Pull+Requests.
