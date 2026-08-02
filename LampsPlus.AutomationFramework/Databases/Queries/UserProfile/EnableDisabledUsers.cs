namespace LampsPlus.AutomationFramework.Databases.Queries.UserProfile
{
    public class EnableDisabledUsers
    {
        public const string Action = @"
        UPDATE p
        SET IsActive = 1
        FROM [UserProfile].[dbo].[tblUserProfile] p
        INNER JOIN [UserProfile].[dbo].[aspnet_Membership] m ON m.UserId = p.UserID
        WHERE FirstName = @UserFirstName
        and p.IsActive = 0";
    }
}
