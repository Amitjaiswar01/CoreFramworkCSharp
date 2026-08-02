 Page Object Standards
This document details the code standards and preferences for the development of page objects.  
Page objects are software representations of a specific web page or web page template. Information about page objects can be found at https://www.swtestacademy.com/page-object-model-c/.
Lamps Plus specific page objects can be found in code under the LampsPlus.AutomationFramework.Pages namespace.

![](../Images/Page%20Object%20Standards/Page%20Objects%20in%20VS.jpg)

There is a Base implementation of the POM and a corresponding Desktop and Mobile version. The Base implementation contains functionality that is exactly the same between Desktop and Mobile. It also includes the abstract and/or virtual version of functionality that is different between Desktop and Mobile. The Desktop and Mobile POMs include the overridden functionality of the abstracted and virtual elements or methods from the corresponding Base class.

## Page Object Organization
Page object classes will organize entities in the following order within the class file in the following order:

Base class:
protected Constructor
public virtual auto-implemented get properties 
public virtual IElements locators 
public virtual ```ReadOnlyCollection<IElement>``` lists
public virtual methods 
public abstract methods 


Inherited class (Desktop and Mobile):
public Constructor
public override auto-implemented get properties 
public override  IElements locators 
public override ```ReadOnlyCollection<IElement>```lists
public override methods 


NOTE: There should be only one class per file.
Base class example (CsrBlockBase):

```C#
using Automation.Framework;
using Automation.Framework.Core;
using OpenQA.Selenium.Support.UI;

namespace LampsPlus.AutomationFramework.Pages.Base
{
    /// <summary>
    /// Base class for common behavior between desktop and mobile views.
    /// </summary>
    public abstract class CsrBlockBase : Page, ICsrBlock
    {
       
        protected CsrBlockBase(IBrowser browser) : base(browser) { }

		public virtual string SaleSourceId { get; } = "saleSource";
        public virtual IElement SaleSourceField => Browser.Locate.ElementById(SaleSourceId);
		  
	    public virtual void SelectSaleSource(string saleSource) { new SelectElement(Browser.Locate.ElementById(SaleSourceId).InternalElement).SelectByText(saleSource); }
		public abstract void TestMethod();//as an example for abstract method
    }
}
```

## Design Requirements
Page objects will follow all relevant design requirements, standards, and styles for test automation software development in addition to the information provided here.

### Namespace / Folder
As mentioned above, all page objects will be organized in a namespace beginning with LampsPlus.AutomationFramework.Pages. There will be three folders contained inside the 'Pages' folder: Base, Desktop, and Mobile. Hence, there will be three POMs per functional area: a Base, a Desktop version, and a Mobile version. 
* The Base POM will be named '**\<FUNCTIONAL AREA>Base**' (e.g. ContactUsBase.cs).
* The Desktop POM will just be named after the functional area (e.g. ContactUs.cs).
* The Mobile POM will have the word 'Mobile' prepended to the functional area (e.g. MobileContactUs.cs).

### Inheritance
All page objects will inherit from Page or a class that inherits from Automation.Framework.Core.Page.
Framework specific behavior is provided by Page and it is important all pages inherit from this to have consistency across all framework page objects.

### Element Locators
Any element that will be interacted with will be defined as an element locator in the page object. This will avoid multiple implementations being created for the same element and help make the code more maintainable.

#### Requirements
* Element locators will return an IElement or ReadOnlyCollection<IElement>.
* Element locators will be defined as a property with only a getter.

It is preferred that expression syntax "=>" is used whenever possible for element locators.

* Element locators will use methods in Browser.Locate to ensure all element locations are routed through common code for logging and to reduce the chance of duplicate implementations.
* Elements should use CSS Selector or XPath syntax whenever possible.

W3Schools provides a good reference for CSS Selector syntax [here](https://www.w3schools.com/cssref/css_selectors.asp).

There is also explanations on how to identify locators specifically for the Lamps Plus site in the document, '**Element Locator Standards.md**' located in the '**Design Requirements**' folder.

* Compound selectors will be constructed using string interpolation syntax **NOT** string concatination.

``` C#
        public IElement DailySkuSortResultContainer(string dailySaleSku) => Browser.Locate.Element($"{Locators.Sort.SortResultContainerId}{dailySaleSku}");
```
### Composite Page Objects
There are certain circumstances where it makes sense for one page object to access another. This can be accomplished by using **composite page objects**. 

For example, the elements for the form fields on the Payment page (First Name, Last Name, etc.) are identical regardless of whether a signed-in customer is updating a saved address on a shipping modal, or filling out a billing address on the Payment page. Therefore, those fields should be defined in one page object where other page objects can reference them.

Continuing to take the Payment page as a practical example, since the form fields are shared between pages, they should be defined in a separate class, in this case '**CustomerAddressInformation**'. 

Once the class file has been created and fleshed out with the correct functionality, it must be called from the '**PaymentBase**' class file. This is done in the constructor of the '**PaymentBase**' class.
``` C#
        public PaymentBase(IBrowser browser, Log log, CustomerAddressInformation customerAddressInformation) : base(browser, log)
        {
            CustomerAddressInformation = customerAddressInformation;
        }
```
The '**CustomerAddressInformation**' class must also be declared at the beginning of the '**PaymentBase**' class:
``` C#
        public CustomerAddressInformation CustomerAddressInformation;
```
Now the Payment page object has access to everything contained within the CustomerAddressInformation page object.

