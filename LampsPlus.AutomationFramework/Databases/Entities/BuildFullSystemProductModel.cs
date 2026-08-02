namespace LampsPlus.AutomationFramework.Databases.Entities
{
    /// <summary>
    /// Product model for the Build Full System page.
    /// </summary>
    public class BuildFullSystemProductModel
    {
        public int DisplayOrder { get; set; }
        public int Quantity { get; set; }

        public string BuildFullSystemSku { get; set; }
        public string ProductName { get; set; }
    }
}
