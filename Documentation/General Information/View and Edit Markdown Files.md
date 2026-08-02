# View and Edit Markdown (*.md) Files

## View / Edit Markdown files in Visual Studio
To enable support to view and edit markdown files in Visual Studio you can install the Markdown Editor https://marketplace.visualstudio.com/items?itemName=MadsKristensen.MarkdownEditor.

This file is sourced in the [packages/Markdown_Editor_v1.12.236.vsix](../../packages/Markdown_Editor_v1.12.236.vsix) at the project root.

This will add an integration into the Visual Studio Solution Explorer
![](../Images/Markdown/Visual%20Studio%20Solution%20Explorer%20Markdown%20Editor.jpg)

Markdown files can edited and viewed in real time in Visual Studio.
![](../Images/Markdown/Visual%20Studio%20Markdown%20Editor%20View.jpg)

## View Markdown files in Chrome
1. Download the Markdown Viewer chrome extension https://chrome.google.com/webstore/detail/markdown-viewer/ckkdlimhmcjmikdlpkmbgfkaikojcbjk?hl=en.
2. Open the Markdown Viewer extension by clicking the ![](../Images/Markdown/Markdown%20Viewer%20Icon.jpg) icon at the top right of Chrome.
3. Configure the Markdown Viewer extension by clicking the Advanced Options button on the extension.

![](../Images/Markdown/Markdown%20Viewer%20Menu.jpg)

4. Click the ALLOW ALL and ALLOW ACCESS TO FILE:// URLS buttons.

![](../Images/Markdown/Markdown%20Viewer%20Advanced%20Options%20Menu.jpg)

5. Open the Chrome Extensions menu.

![](../Images/Markdown/Extensions%20Chrome%20Menu.jpg)

6. Modify the Markdown Viewer settings by clicking the Details button on the Markdown Viewer Chrome extension.

![](../Images/Markdown/Markdown%20Viewer%20Extensions.jpg)

7. Make sure the extension toggle is "On" and set the toggles for "Allow in incognito" and "Allow access to file URLs" to enabled.

![](../Images/Markdown/Markdown%20Viewer%20Extension%20Settings.jpg)

8. Any *.md file can now be dragged into Chrome to view in markdown format.

---

# Other Useful Tools

There is a Markdown 'cheat sheet' located at https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet which outlines how to achieve different formatting options.

It is also possible to see the results of markdown text in realtime by using a plugin called '**MarkdownView++**' for Notepad++.  
1. In Notepad++ go to the '**Plugins**' menu and select '**Plugin Manager**' and then select the option '**Show Plugin Manager**'.   
2. On the '**Available**' tab, scroll down and check the box for '**MarkdownView++**' and then click the '**Install**' button.   
  * Notepad might need to be re-started at this point
  
3. In order to user the viewer, open a new tab, add some markdown text then select '**MarkdownViewer++**' from the plugins menu. Another window should open on the right which shows the correctly formatted text based on what is typed into the left panel.
4. It is recommended that after finishing the markdown to save it in Notepad++ as an .md file and to view it in Chrome (provided the extension mentioned above is installed). Once this is done, the text can be copy/pasted back into Visual Studio.

There are also options to change the language to markdown syntax.

1. Navigate to https://github.com/Edditoria/markdown-plus-plus and follow the instructions in the '**Usage**' section.
2. Once the syntax XML is downloaded, click on the '**Language**' menu option in Notepad++ and select '**Define your language**'.
3. On the popup window that appears, click the '**Import**' button.
4. Navigate to the location where the XML was saved and select it.
5. Re-start Notepad++.
6. Open a Markdown file and from the '**Language**' menu, select the language. (It will be located at the bottom of the menu.)
