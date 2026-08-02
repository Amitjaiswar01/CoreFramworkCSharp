# Compiled Help Module (CHM)
This document explains how to setup and configure Sandcastle to generate design documentation from the source code.

1. Build the source code for the Lamps Plus Web Tests solution.
2. Install the Microsoft build tools (BuildTools_Full.exe) located in the packages folder at the project root.
3. Install Sandcastle (SHFBInstaller_2018.5.29.0.zip) located in the packages folder at the project root.
4. Open TestAutomationDocumnetation.shfbproj located in the source root in the TestAutomationDocumentation folder.
5. Open the file in the Sandcastle Help File Builder Standalone GUI.
6. Select the build configuration to generate documentation for (DebugLocal, ReleaseLocal, ...).
7. Press the build button to the right of AnyCPU.
8. The documentation will be generated in the TestAutomationDocumentation\Help folder.It will have a*.chm extension.
