namespace Automation.Framework.Enums
{
    /// <summary>
    /// CSS Attribute selector types for locating an element by HTML attribute
    /// </summary>
    public enum AttributeSelectorType
    {
        /// <summary>
        /// [attr="value"] Select elements whose attribute has the exact specified value.
        /// </summary>
        Equals = 1,
        /// <summary>
        /// [attr^="value"] Select elements whose attribute starts with a specified value.
        /// </summary>
        StartsWith,
        /// <summary>
        /// [attr$="value"] Select elements whose attribute ends with a specified value.
        /// </summary>
        EndsWith,
        /// <summary>
        /// [attr] Select elements with a specified attribute.
        /// </summary>
        HasAttribute,
        /// <summary>
        /// [attr*="value"] Select elements whose attribute value contains a specified substring.
        /// </summary>
        Contains,
        /// <summary>
        /// [attr|="value"] Select elements whose attribute contains an exact specified value or starts with a specified value immediately followed by a hyphen.
        /// </summary>
        ContainsPrefix,
        /// <summary>
        /// [attr~="value"] Select elements whose attribute contains a specified word delimited by spaces.
        /// </summary>
        ContainsWord
    }
}
