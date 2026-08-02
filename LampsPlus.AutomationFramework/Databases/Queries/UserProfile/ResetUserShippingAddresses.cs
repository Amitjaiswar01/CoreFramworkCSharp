
namespace LampsPlus.AutomationFramework.Databases.Queries.UserProfile
{
    /// <summary>
    /// Query to reset user's shipping addresses
    ///  Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T338
    /// </summary>
    public class ResetUserShippingAddresses
    {
        public const string Query = @"DELETE FROM Userprofile.dbo.tblShippingAddress
        WHERE IsDefault=0 AND RewardNUmber = @RewardNumber;";
    }
}
