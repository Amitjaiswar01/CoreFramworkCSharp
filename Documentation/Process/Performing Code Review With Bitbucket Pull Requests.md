# Perform a code review with Bitbucket Pull Requests
Bitbucket Pull Requests are used to do our code reviews. All code should be reviewed before it is merged into a parent branch in source control. This document will outline the code review process for Lamps Plus software development.

It is important to NOTE the following:

1. All task owners will add their Team lead and 1+ other members working on automation for each Pull Request.
2. Tasks that require architecture help should be transitioned to the "Tech Support Needed" status.

Test automation tasks are not considered "Complete" until your code has been merged into the release branch.

## JIRA Task Status
Before participating in a code review, ensure that the JIRA task meets the following criteria:
- Valid status for review: CODE REVIEW, CODE REVIEW IN PROGRESS, or TECHNICAL REVIEW.
- Open Pull Request for the task

![](../Images/Code%20Reviews/OpenPullRequest.jpg)


If any of the following is no, then please comment on the task that the code is not ready for review.

## Is Task Mergable?
The following criteria should be used to determine if a task is mergable:
- Code has been synced with the parent branch
- Code compiles
- Task under review works as intended (if it is a test case run it)
- Functionality being verified works as expected

To determine if the code is up to date and synced, click on the task pull request.

![](../Images/Code%20Reviews/IsTaskMergable.jpg)


If the following message is displayed, the code will not be mergable until the code under review has been synced with the parent branch and any merge conflicts resolved.

![](../Images/Code%20Reviews/MergeConflict.jpg)


If any of the following is no, then please comment on the task that the code is not ready for review.

## Review in Bitbucket Pull Requests
The code review can be opened by clicking the pull request link under the Development area in the JIRA task.

### Verify Context
Before beginning the code review, we should first make sure we are reviewing the right thing. The following questions will help answer this question.
- Is the Task associated with the PR correct?
- Does the PR have the correct source and destination branches? This can be confirmed by checking the branches at the top of the page when the PR is opened.

![](../Images/Code%20Reviews/VerifyContext.jpg)

- All branches should be merged back into their parent branch. Sub-Task Feature -> Parent Feature, Feature -> Release, HotFix -> HotFix Release, Release -> master


If any of the following is no, then please comment on the task that the code is not ready for review.

### Review Changes
Review each of the files in the review for the following:

- Are the Coding Standards & Styles being followed?
- Is the logic correct?
- Are S.O.L.I.D. design patterns being followed?
- Review the test cases in the test case management system and verify that the test case(s) are automated per the requirements.
- For any open questions or anything that is not immediately apparent add a comment in the review.

For anything which need further work, add a comment by clicking on any line of code in the PR.

![](../Images/Code%20Reviews/NeedsWork.jpg)

It is the developer's responsibility to resolve any feedback indicating further work and to manage the JIRA status to get the task ready to be reviewed.

NOTE: A PR should not be approved with any outstanding "Needs Work" issues.

### Merging
To merge, check all of the following:

- No outstanding "Needs Work" comments (Bitbucket will not permit merges until resolved)
- No Merge Conflicts
- At least 2 approvals (ideally)

Once the PR has been approved, the last reviewer will click the MERGE button to merge the code into the parent branch.
**Select the "Delete sour branch after merging" checkbox to remove the remote branch after the merge**

## Task needs Resolution
As a reviewer, if you don't feel the code or task is production-ready for any reason, comment in the JIRA task your rational and an explanation of what needs to be fixed before the PR can be approved.

You can also click the Needs Work button in the PR itself to provide context-specific feedback.

![](../Images/Code%20Reviews/NeedsWork.jpg)

The developer can optionally incorporate the comment ID into commit messages to link a commit to a particular comment:

![](../Images/Code%20Reviews/CommentIdIntoCommit.jpg)

The reviewer can either Undo or Resolve the comment once the feedback has been addressed:

![](../Images/Code%20Reviews/UndoResolve.jpg)