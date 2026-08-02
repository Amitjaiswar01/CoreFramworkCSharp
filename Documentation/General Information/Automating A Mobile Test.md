# Automating A Mobile Test

Because it is currently not possible to automate a test while using an actual mobile device to test on, it is necessary to use the Chrome browser in mobile view mode during development (and testing for now). The purpose of this document is to explain how to go about this.

### Identifying Locators

There's a good chance that the locators needed to interact with the page will not change between the desktop and mobile views. However, to verify this is true, the browser must be in mobile view mode.
1. With Chrome open, press F12. This will open the Developer Tools.
2. In the Developer Tools, click the mobile view icon:

![](../Images/Mobile%20Testing/Developer_Tools.png)

3. It may be necessary to reload the page (F5) in order to see the mobile view:

![](../Images/Mobile%20Testing/Before_Refresh.png)

![](../Images/Mobile%20Testing/After_Refresh.png)

4. From this point, the user can identify locators the same way as the desktop view: right click on the required section of the page and select "**Inspect**":

![](../Images/Mobile%20Testing/Inspect.png)

### Executing A Mobile Test

Due to current device and hardware limitations, it is necessary to execute tests in mobile view as well. It is not as easy as simply setting the browser to mobile view in the developer tools - it requires a small change in the framework.

1. In the framework, look for the word "**driver**".
2. Double click on the "**Driver.cs**" file:

![](../Images/Mobile%20Testing/Driver.png)

3. Inside the Driver.cs file, look for the method "**InitializeChromeDriver**" and switch the value of "**isMobileView**" to "**true**".

![](../Images/Mobile%20Testing/Mobile_View_Method.png)

_**NOTE: Make sure to remove this change before checking in code for a pull request!**_

4. Technically, this is all that is required to get the test to run in mobile view. However, if desired, the user can also change the _type_ of device being tested. Simply pass in the desired device as a parameter for "**options.EnableMobileEmulation()**".

![](../Images/Mobile%20Testing/Device.png)

5. The user can input any device that is also in the device dropdown in Chrome:

![](../Images/Mobile%20Testing/DeviceList.png)

_Please note: The device name in the framework must match **EXACTLY** to the device name in the list. Otherwise, it will not work. Currently, iPhone 6/7/8 should be the primary testing device(s)._