namespace LampsPlus.AutomationFramework.Databases.Queries.UserProfile
{
    public class DisableUser
    {
        public const string Action = @"
        UPDATE p
        SET IsActive = 0
        FROM [UserProfile].[dbo].[tblUserProfile] p
        INNER JOIN [UserProfile].[dbo].[aspnet_Membership] m ON m.UserId = p.UserID
        WHERE LoweredEmail = @UserEmail";
    }
}