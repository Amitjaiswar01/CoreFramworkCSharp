# Lamps Plus test-automation
Provides automation support to test the desktop view of the Lamps Plus website.

## Project Links
### Source Code (Bitbucket)
LampsPlus/TestAutomation https://bitbucket.lampsplus.com:8443/projects/LAMPS/repos/test-automation/

### Project Kanban (JIRA)
https://lampstrack.lampsplus.com:8443/secure/RapidBoard.jspa?rapidView=215

### Test Automation Chat Room (slack)
qa-automation-chat

### Build Plan(s) (Bamboo)
https://bamboo.lampsplus.com:8443/browse/TES

### Test Automation System Documentation (Confluence)
https://confluence.lampsplus.com:8093/pages/viewpage.action?spaceKey=TA&title=Test+Automation

### Weekly Test Automation Sync Meeting
We have a standing weekly meeting to discuss the current state of test automation. The meeting is scheduled **Wednesdays @2PM typically held in the Conference Room 20250 Upstairs conference room**.

## Framework Setup 
To contribute to test automation development the following are required:
- [Visual Studio 2017](https://www.visualstudio.com/downloads/) 15.6.6
- [.NET Framework 4.7](https://www.microsoft.com/net/download/visual-studio-sdks).
- Resharper - Visual Studio addon to aid development. ReSharper also includes a Test Runner that has advantages over the default xUnit test runner.
- Chrome browser (for testing in Chrome)
- Internet Explorer browser (for testing in Internet Explorer)
- Sandcastle (for building a compiled help module) located in packages/SHFBInstaller_v2018.5.29.0.zip in the directory where the code is cloned.
- ImageComments (Viewing images in summaries) located in packages/ImageComments.vsix in the directory where the code is cloned.
- TechTalk Spec Flow located in packages/TechTalk.SpecFlow.VsIntegration.2017-2017.2.7.vsix

This project is self-contained and contains all necessary libraries provided by NuGet.

## Documentation
Dynamic information that changes frequently can be found in source control in the [Documentation folder](./Documentation).
Planning information, meeting minutes, and general information can be found in the [Test Automation Confluence Space](https://confluence.lampsplus.com:8093/pages/viewpage.action?spaceKey=TA&title=Test+Automation).

### General Information
#### [View MD Files in Chrome](./Documentation/General%20Information/View%20MD%20Files%20in%20Chrome.md)
#### [View Image Comments in Visual Studio](./Documentation/General%20Information/View%20Image%20Comments%20in%20Visual%20Studio.md)
#### [Compiled Help Module (CHM)](./Documentation/General%20Information/Compiled%20Help%20Module.md)
#### [Build Configurations](./Documentation/General%20Information/Build%20Configurations.md)
#### [Store In Session Kiosk Access](./Documentation/General%20Information/Store%20In%20Session%20Kiosk%20Access.md)
#### [Automating A Mobile Test](./Documentation/General%20Information/Automating%20A%20Mobile%20Test.md)

### Process
#### [Test Automation SDLC](./Documentation/Process/Test%20Automation%20SDLC.md)
#### [Daily Test Case Review](./Documentation/Process/Daily%20Test%20Case%20Review.md)

### Design Requirements
#### [General Standards](./Documentation/Design%20Requirements/General%20Standards.md)
#### [Test Class Standards](./Documentation/Design%20Requirements/Test%20Class%20Standards.md)
#### [Test Case Standards](./Documentation/Design%20Requirements/Test%20Case%20Standards.md)
#### [Page Object Standards](./Documentation/Design%20Requirements/Page%20Object%20Standards.md)
#### [Database Standards](./Documentation/Design%20Requirements/Database%20Standards.md)
#### [Utilities Standards](./Documentation/Design%20Requirements/Utilities%20Standards.md)
#### [Logging Standards](./Documentation/Design%20Requirements/Logging%20Standards.md)
