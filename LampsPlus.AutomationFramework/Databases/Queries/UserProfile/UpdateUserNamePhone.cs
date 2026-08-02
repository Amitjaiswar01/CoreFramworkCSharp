namespace LampsPlus.AutomationFramework.Databases.Queries.UserProfile
{
    /// <summary>
    /// Query to reset user's shipping addresses
    ///  Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T338
    /// </summary>
    public class UpdateUserNamePhone
    {
        public const string Query = @"
        UPDATE 
            Userprofile.dbo.tblUserProfile
        SET
            FirstName = @FirstName,
            LastName = @LastName,
            PhoneNumber = @PhoneNumber
        WHERE 
            RewardNUmber = @RewardNumber";
    }
}