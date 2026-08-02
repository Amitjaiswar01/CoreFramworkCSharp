namespace LampsPlus.AutomationFramework.Workflow
{
    /// <summary>
    /// Common utility methods.
    /// </summary>
    public interface ICommonWorkflow
    {
        /// <summary>
        /// Click Cancel button on mobile drawer.
        /// </summary>
        void CancelDrawer();

        /// <summary>
        /// Close a modal window.
        /// </summary>
        void CloseLpModal();

        /// <summary>
        /// Click confirmation button on mobile drawer. 
        /// </summary>
        void ConfirmDrawer();

        /// <summary>
        /// Waits for currently displayed mobile sliding drawer to stop animating.
        /// Useful for acting on menu items without causing an error.
        /// </summary>
        void WaitForDrawerToStopAnimating();

        /// <summary>
        /// Waits for the Remove Button while removing the item from the cart.
        /// Clicks on the Remove Button on the drawer. 
        /// </summary>
        void ConfirmRemoveItemDrawer();
    }
}
