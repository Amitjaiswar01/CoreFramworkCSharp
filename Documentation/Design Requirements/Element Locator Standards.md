# Lamps Plus Element Locator Standards
Element locators are a critical part of test automation. There are several ways in which these can be defined so the purpose of this document is to provide some guidance on how to create them as well as showing some examples.

# General Guidelines
1.	Do not rely on generating a CSS selector by using Chrome Dev Tool’s “Copy selector” feature. In most cases, it generates long and undesired CSS selectors. For example: #lpContainer > table > tbody > tr > td.shippingCol > a
2.	Use ElementByXXXX or ElementsByXXXX methods. Specify the **parentElement** parameter as needed. If nothing is suitable, then use **ElementBySelector** or **ElementsBySelector** methods as a last resort. Use these methods over the old Element and Elements methods.
3.	When locating elements by CSS selector, do not hardcode the ID, classes, or attributes. Use the available string extension methods such as **ToCssIdSelector**, **ToCssClassSelector**, etc.
4.	Do not hardcode HTML attributes. Use the **HtmlTextWriterAttribute** enum. Use **.ToString()** if this is used in a string parameter. This doesn’t apply to custom attributes such as “data-xxxx”.
5.	Do not hardcode HTML tag names. Use the **HtmlTextWriterTag** enum. Use **.ToString()** if this is used in a string parameter. This doesn’t apply to newer tags that are not in the enum yet (e.g. “section”).

# New Locator Methods
As the automation framework matures and additional tests are added to it, it will become necessary to incorporate additional locator methods. Fortunately, **Glenn Vergara** has added a number of new methods designed to find elements that might be difficult or more complex to define.

## _**Locate Elements by Attribute Name and Value from A List of Parent Elements**_
Use the following method to locate elements by attribute name and value from a list of parent elements:
```c#
public ReadOnlyCollection<IElement> ElementsByAttribute(ReadOnlyCollection<IElement> parentElements, AttributeSelectorType attSelectorType, HtmlTextWriterAttribute attributeName, string attributeValue = "")
```
For example, if you want to locate all anchor links that contain /products/ in the href attribute from all category menu dropdowns, then the code may look like this:
```c#
var links = Browser.Locate.ElementsByAttribute(categoryDropdowns, AttributeSelectorType.Contains, HtmlTextWriterAttribute.Href, "/products/");
```

## _**Locate Element That Is An Ancestor or Parent Element of the Specified Descendant Element by HTML ID Attribute**_
Use the following method to locate an element that is an ancestor or parent element of the specified descendant element by HTML ID attribute:
```c#
public IElement AncestorElementById(IElement descendantElement, string ancestorId)
```
For example, in the following code snippet:
```html
<div id="container">
    <div>
        <div class="noReview"></div>
    </div>
</div>
```
To locate the container div that has a child div with the noReview class do the following:
```c#
var containerId = "container";
var noReviewClass = "noReview";
var child = Browser.Locate.ElementByClassName(noReviewClass);
var container = Browser.Locate.AncestorElementByClassName(child, containerId);
```

## _**Locate Element That Is An Ancestor or Parent Element of the Specified Descendant Element by CSS Class Name**_
Use the following method to locate an element that is an ancestor or parent element of the specified descendant element by CSS class name:
```c#
public IElement AncestorElementByClassName(IElement descendantElement, string ancestorClassName)
```
For example, given the following code snippet:
```html
<div class="container">
    <div>
        <div class="noReview"></div>
    </div>
</div>
```
To locate the container div that has a child div with the noReview class do the following:
```c#
var containerClass = "container";
var child = Browser.Locate.ElementByClassName("noReview");
var container = Browser.Locate.AncestorElementByClassName(child, containerClass);
```

## _**Locate Element That Is An Ancestor or Parent Element of the Specified Descendant Element by HTML Tag Name**_
Use the following method to locate an element that is an ancestor or parent element of the specified descendant element by HTML tag name:
```c#
public IElement AncestorElementByTagName(IElement descendantElement, string ancestorTagName)
```
For example, given the following code snippet:
```html
<section class="container">
    <div>
        <div class="noReview"></div>
    </div>
</section>
```
To locate the container section that has a child div with the noReview class do the following:
```c#
var noReviewClass = "noReview";
var child = Browser.Locate.ElementByClassName(noReviewClass);
var container = Browser.Locate.AncestorElementByTagName(child, "section");
```

## _**Locate Element That Is An Ancestor or Parent Element of the Specified Descendant Element by HTML Tag Name**_
Use the following method to locate an element that is an ancestor or parent element of the specified descendant element by HTML tag name:
```c#
public IElement AncestorElementByTagName(IElement descendantElement,  HtmlTextWriterTag ancestorTagName)
```
For example, given the following code snippet:
```html
<div class="container">
    <section>
        <div class="noReview"></div>
    </section>
</div>
```
To locate the container div that has a child div with the noReview class do the following:
```c#
var noReviewClass = "noReview";
var child = Browser.Locate.ElementByClassName(noReviewClass);
var container = Browser.Locate.AncestorElementByTagName(child, HtmlTextWriterTag.Div);
```

## _**Locate Element That Is An Ancestor or Parent Element of the Specified Descendant Element by CSS Selector**_
Use the following method to locate an element that is an ancestor or parent element of the specified descendant element by CSS selector:
```c#
public IElement AncestorElementBySelector(IElement descendantElement, string ancestorSelector)
```
For example, given the following code snippet:
```html
<div class="container active">
    <section>
        <div class="noReview"></div>
    </section>
</div>
```
To locate the active container div that has a child div with the noReview class:
```c#
var containerClass = "container";
var activeClass = "active";
var noReviewClass = "noReview";
var child = Browser.Locate.ElementByClassName(noReviewClass);
var container = Browser.Locate.AncestorElementByClassName(child, $"{containerClass.ToCssClassSelector()}{activeClass.ToCssClassSelector()}");
```

## _**String Extension Methods**_
### Example 1:
Use the following string extension method to convert an HTML attribute type into an HTML input tag name and type attribute CSS selector:
```c#
public static string ToInputTypeCssSelector(this string attributeValue)
```
For example, to get the following:
```html
input[type="submit"]
```
Use the following code:
```c#
"submit".ToInputTypeCssSelector()
```
### Example 2:
Use the following string extension method to convert an HTML attribute into a :not([attr]) CSS attribute selector:
```c#
public static string ToNotAttributeSelector(this string attributeToBeNegated)
```
For example, to the get the following:
```html
:not([data-sku])
```
Use the following code:
```c#
"data-sku".ToNotAttributeSelector()
```
### Example 3:
Use the following string extension method to convert an HTML attribute into a :not([attr]) CSS attribute selector:
```c#
public static string ToNotAttributeSelector(this HtmlTextWriterAttribute attributeToBeNegated)
```
For example, to get the following:
```html
:not([checked])
```
Use the following code:
```c#
HtmlTextWriterAttribute.Checked.ToNotAttributeSelector()
```
# Practical Examples
The following are real-world examples from the automation framework. They show the original code, the updated code, and an explanation of why they were changed.
### Example 1: Using ElementByClassNameAndAttributeEquals
For locators using .className[attr=”value”], use the **ElementByClassNameAndAttributeEquals** method or other related **ElementByClassNameAndAttributeXXXX** methods.
##### *File Location:* 
LampsPlus.AutomationFramework/Pages/Base/EmployeeOrderLookupBase.cs
##### *Code Before:*
```c#
public static string TrTemplateClassWithAttribute(string attribute, string value) => $".trTemplate[{attribute}={value}]";
public IElement SelectedOrder(string orderId) => Browser.Locate.Element(TrTemplateClassWithAttribute("data", orderId));
```
##### *Code After:*
```c#
public static string TrTemplateClass => "trTemplate";
public IElement SelectedOrder(string orderId) => Browser.Locate.ElementByClassNameAndAttributeEquals(TrTemplateClass, "data", orderId);
```
### Example 2: Using ToCssIdSelector and ToCssClassSelector
No “#” and “.” should ever be hardcoded in the selectors. Use **ToCssIdSelector** and **ToCssClassSelector** string extension methods to add those characters programmatically.
##### *File Location:*
LampsPlus.AutomationFramework/Pages/Base/GlobalLocatorsBase.cs
##### *Code Before:*
```c#
public IElement AddToCartButton => Browser.Locate.ElementBySelector($"#{PdAddToCartId}, #{AddToCartMultiproductId}");
```
##### *Code After:*
```c#
public IElement AddToCartButton => Browser.Locate.ElementBySelector($"{PdAddToCartId.ToCssIdSelector()}, {AddToCartMultiproductId.ToCssIdSelector()}");
```
### Example 3: Don't Use Hardcoded CSS Attribute Selectors
No CSS attribute selectors should be hardcoded.
##### *File Location:*
LampsPlus.AutomationFramework/Pages/Base/HeaderFooterBase.cs
##### *Code Before:*
```c#
public static string FooterFacebookNameAttribute => "[name=footer_facebook]";
public static string FooterInstagramNameAttribute => "[name=footer_instagram]";
public static string FooterPinterestNameAttribute => "[name=footer_pinterest]";
```
##### *Code After:*
```c#
public static string FooterFacebookNameAttribute => "footer_facebook";
public static string FooterInstagramNameAttribute => "footer_instagram";
public static string FooterPinterestNameAttribute => "footer_pinterest";
```
### Example 4: Compound Selectors Containing 2 Selectors
For compound selectors containing 2 selectors, use the optional **parentElement** parameter to specify the parent or ancestor element that contains the child or descendant element being located. All **Browser.Locate.ElementByXXXX** methods (except Browser.Locate.ElementById) have the optional **parentElement** parameter for this purpose.
##### *File Location:*
LampsPlus.AutomationFramework/Pages/Base/HeaderFooterBase.cs
##### *Code Before:*
```c#
public bool IsProAccount() { return Browser.Locate.Element($" #{LogoId} {ImgTagString}").GetAttribute(SrcString).Contains("PROS"); }
```
##### *Code After:*
```c#
public bool IsProAccount() {
    return Browser.Locate.ElementByTagName(HtmlTextWriterTag.Img,   
       Browser.Locate.ElementById(LogoId)).GetAttribute(SrcString).Contains("PROS");
}
```
### Example 5: Locating Links Based On Link Text
For locating links based on the link text, use the **Browser.Locate.ElementByLinkText** method. Specify the optional **parentElement** parameter if you need to locate the links inside a parent or ancestor element.
##### *File Location:*
LampsPlus.AutomationFramework/Pages/Base/MagicMerchandizerBase.cs
##### *Code Before:*
```c#
public IElement ChandelierNavLinkElement => ChandeliersNavCategoryElement.FindElement(By.LinkText(ChandeliersCategory));
```
##### *Code After:*
```c#
public IElement ChandelierNavLinkElement => Browser.Locate.ElementByLinkText(ChandeliersCategory, ChandeliersNavCategoryElement);
```
### Example 6: Locating Un-Checked Radio Buttons or Checkboxes (Case 1)
For locating unchecked radio button or checkbox, use the **ToInputTypeCssSelector** method along with the **ToNotAttributeSelector** method.
##### *File Location:*
LampsPlus.AutomationFramework/Pages/Base/ManageAccountBase.cs
##### *Code Before:*
```c#
public IElement UnselectedRadioButton => Browser.Locate.Element("input[type='radio']:not([checked])");
```
##### *Code After:*
```c#
public IElement UnselectedRadioButton => Browser.Locate.ElementBySelector($"{GlobalLocators.InputTypeRadioAttribute.ToInputTypeCssSelector()}{HtmlTextWriterAttribute.Checked.ToNotAttributeSelector()}");
```
### Example 7: Locating Un-Checked Radio Buttons or Checkboxes (Case 2)
For locating unchecked radio button or checkbox, use the **ToInputTypeCssSelector** method along with **ToNotAttributeSelector** method. In some cases, adding each element in the DOM tree is not necessary. In the example below, “table tbody tr” is not necessary and can be excluded.
##### *File Location:*
LampsPlus.AutomationFramework/Pages/Base/ManageAccountBase.cs
##### *Code Before:*
```c#
public string FirstBillingInfoAddress => Browser.Locate.Element($"{PaymentOptionClass.ToCssClassSelector()} table tbody tr td:nth-child(3) ul li:nth-child(3)").Text;
```
##### *Code After:*
```c#
public string FirstBillingInfoAddress => Browser.Locate.ElementBySelector($"{HtmlTextWriterTag.Li.ToNthChildSelector(3)}", BillingInfoTd).Text;
```
### Example 8: Locating Elements By Tag Name
For locating elements by tag name, use the **ElementByTagName** method and then use the **HtmlTextWriterTag** enum.
##### *File Location:*
LampsPlus.AutomationFramework/Pages/Base/OrderConfirmationBase.cs
##### *Code Before:*
```c#
public IElement OrderItemShipmentLabel(int index) => OrderDetailsItemShipmentElements[index].FindElement(By.TagName("strong"));
```
##### *Code After:*
```c#
public IElement OrderItemShipmentLabel(int index) => Browser.Locate.ElementByTagName(HtmlTextWriterTag.Strong, OrderDetailsItemShipmentElements[index]);
```
### Example 9: Locating A Direct Child Element
For locating a direct child element, specify the **parentElement** parameter and then specify **true** for the **isDirectChild** parameter. In some cases, creating the **parentElement** as a separate variable makes the code cleaner and makes that variable re-usable in other elements.
##### *File Location:*
LampsPlus.AutomationFramework/Pages/Base/OrderDetailsBase.cs
##### *Code Before:*
```c#
public IElement PaymentMethodElement => Browser.Locate.Element($"{PaymentMethodClass.ToCssClassSelector()} > div");
```
##### *Code After:*
```c#
public IElement PaymentMethod => Browser.Locate.ElementByClassName(PaymentMethodClass);
public IElement PaymentMethodElement => Browser.Locate.ElementByTagName(HtmlTextWriterTag.Div, PaymentMethod, true);
```
### Example 10: Locating An Element That Has Multiple Class Names
For locating an element having multiple class names, use the **ElementByClassNames** method and then specify each class name as individual parameters. It has overloaded methods for locating elements with or without the **parentElement** and **isDirectChild** parameters.
##### *File Location:*
LampsPlus.AutomationFramework/Pages/Base/OrderSummaryBlockBase.cs
##### *Code Before:*
```c#
public IElement PosProductTotal => Browser.Locate.Element($"div{FlClass.ToCssClassSelector()}{OsValueClass.ToCssClassSelector()}");
```
##### *Code After:*
```c#
public IElement PosProductTotal => Browser.Locate.ElementByClassNames(OrderSummaryElement, false, OsValueClass, FlClass);
```

