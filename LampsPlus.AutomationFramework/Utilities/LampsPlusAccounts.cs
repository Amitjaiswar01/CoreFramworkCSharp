using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Services;

namespace LampsPlus.AutomationFramework.Utilities
{
    /// <summary>
    /// Helper utility to manage behavior of Lamps Plus login types.
    /// </summary>
    public class LampsPlusAccounts
    {
        public static LampsPlusAccount CustomerChangePasswordLoginAccount => new LampsPlusAccount { UserName = "autochangepassword@lampsplus.com", OriginalPassword = "j_we'r/3}8sD]ZU7", TempPassword = "test1234", FirstName = "Auto-ChangePwd", LastName = "Auto-ChangePwd" };
        public static LampsPlusAccount CustomerLoginAccount => UserAccountManagerService.GetUser(UserRolesTypes.Customer);
        public static LampsPlusAccount CustomerServiceRegularLoginAccount => UserAccountManagerService.GetUser(UserRolesTypes.CustomerServiceRegular);
        public static LampsPlusAccount CustomerServiceManagerLoginAccount => UserAccountManagerService.GetUser(UserRolesTypes.CustomerServiceManager);
        public static LampsPlusAccount MinimalAccount => new LampsPlusAccount { UserName = "automationminimalaccount@mailinator.com", Password = "w?dQ'NYu7D" };
        public static LampsPlusAccount ProfessionalLoginAccount => UserAccountManagerService.GetUser(UserRolesTypes.Professional);
        public static LampsPlusAccount HospitalityLoginAccount => UserAccountManagerService.GetUser(UserRolesTypes.Hospitality);
        public static LampsPlusAccount PayPalAccount => new LampsPlusAccount { UserName = "autopaypaluser@gmail.com", Password = "Pa$$word", FirstName = "Auto", LastName = "PayPal" };
    }
}
