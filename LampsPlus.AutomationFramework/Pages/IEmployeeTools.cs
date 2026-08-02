using Automation.Framework;

namespace LampsPlus.AutomationFramework.Pages
{
    /// <summary>
    /// https://www.lampsplus.com/employee-tools/default.aspx
    /// </summary>
    public interface IEmployeeTools
    {
        #region Page Elements
        IElement EmployeeToolBodyElement { get; }
        IElement QuickShortSkuElement { get; }
        IElement QuickAddToCartElement { get; }

        IBrowser Browser { get; }
        #endregion
    }
}
