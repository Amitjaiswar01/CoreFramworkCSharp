namespace LampsPlus.AutomationFramework.Utilities.Environment
{
    public class AccountConfiguration
    {
        public bool KeepMeLoggedIn { get; set; }
        public string StoreInSessionStoreNumber { get; set; }
        public bool ClearStoreInSessionOnSetup { get; set; }
        public bool ClearStoreInSessionOnTearDown { get; set; }
        public bool ClearSavedPaymentOptionsOnSetup { get; set; }
        public bool ClearSavedShippingAddressOnSetup { get; set; }
    }
}
