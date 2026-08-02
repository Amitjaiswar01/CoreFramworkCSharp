

namespace LampsPlus.AutomationFramework.Databases.Queries.UserProfile
{
    /// <summary>
    /// Query to reset user's payment options
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T338
    /// </summary>
    public class ResetUserPaymentOptions
    {
        public const string Query = "DELETE FROM UserProfile.dbo.tblPaymentInfo WHERE RewardNumber = @RewardNumber";
    }
}
