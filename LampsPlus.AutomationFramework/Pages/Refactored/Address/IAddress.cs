namespace LampsPlus.AutomationFramework.Pages.Refactored.Address
{
    public interface IAddress : IPageObjectModel
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string AddressLine1 { get; set; }
        string AddressLine2 { get; set; }
        string City { get; set; }
        string State { get; set; }
        string Country { get; set; }
        string ZipCode { get; set; }
        string Phone { get; set; }
        string Email { get; set; }
        bool SaveToProfile { get; set; }
    }
}
