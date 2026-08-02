namespace LampsPlus.AutomationFramework.Databases.Entities
{
    /// <summary>
    /// Verification whether a new account is approved or not.
    /// </summary>
    public class NewUserAccount
    {
        public string Email { get; set; }

        public int IsApproved { get; set;}
    }
}
