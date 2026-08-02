namespace LampsPlus.AutomationFramework.Utilities
{
    /// <summary>
    /// Account to use to log into the website.
    /// </summary>
    public class LampsPlusAccount
    {
        /// <summary>
        /// User name to use for login.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Password used for login.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Original Password used for reset.
        /// </summary>
        public string OriginalPassword { get; set; }

        /// <summary>
        /// Temp Password used for reset.
        /// </summary>
        public string TempPassword { get; set; }

        /// <summary>
        /// First name used for login.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name used for login.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Phone number used for account
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Discount used for account.
        /// </summary>
        public string Discount { get; set; }

        /// <summary>
        /// Does the account have a UserName and Password?
        /// NOTE: This does not mean the user name and password match only they have been set.
        /// </summary>
        public bool IsAccountValid => !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password);

        /// <summary>
        /// A account requires all input variables to build a valid account.
        /// </summary>
        /// <param name="userName">Account user name.</param>
        /// <param name="password">Account password.</param>
        /// <param name="firstName">Account first name.</param>
        /// <param name="lastName">Account last name.</param>
        /// <param name="discount">Maximum discount able to be applied to the account.</param>
        public LampsPlusAccount(string userName, string password, string firstName = "", string lastName = "", string discount = "")
        {
            UserName = userName;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Discount = discount;
        }

        /// <summary>
        /// Default constructor. The other constructor should typically be used for construction or all necessary properties need to be set.
        /// A valid account must have a UserName and Password property that is not an empty string.
        /// </summary>
        public LampsPlusAccount() { }
    }
}