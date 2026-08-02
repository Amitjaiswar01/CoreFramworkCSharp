using System;

namespace LampsPlus.AutomationFramework.Databases.Entities
{
    /// <summary>
    /// Contains details about customer information such as name, address, email, city, state, zip, phone...
    /// </summary>
    public class CustomerInformationModel
    {
        public DateTime CreatedDate { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string ShortSku { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}
