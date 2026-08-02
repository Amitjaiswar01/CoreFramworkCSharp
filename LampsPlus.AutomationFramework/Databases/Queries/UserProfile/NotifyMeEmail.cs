namespace LampsPlus.AutomationFramework.Databases.Queries.UserProfile
{
    public class NotifyMeEmail
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 emailaddress, shortsku  
                                    FROM   userprofile.dbo.tblemailrecepients 
                                    WHERE emailaddress = @Email
                                    AND shortsku = @ShortSku
                                    ";
    }
}
