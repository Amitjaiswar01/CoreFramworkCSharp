namespace LampsPlus.AutomationFramework.Databases.Queries.ShoppingCart
{
    /// <summary>
    /// Query to get the current discount rate based on order total
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T160
    /// </summary>
    public class DiscountRateForOrderTotal
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT Currentdiscountrate
                                    FROM userprofile.dbo.tblcompany 
                                    WHERE companyname = @companyname
                                    ";
    }
}
