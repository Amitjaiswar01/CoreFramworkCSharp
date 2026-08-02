namespace Automation.Framework.Enums
{
    /// <summary>
    /// Supported approaches to Locate elements.
    /// </summary>
    public enum LocatorStrategy
    {
        Id = 1,
        Class,
        Css,
        TagName,
        Text,
        PartialText,
        Name,
        By,
        Js,
        Xpath
    }
}
