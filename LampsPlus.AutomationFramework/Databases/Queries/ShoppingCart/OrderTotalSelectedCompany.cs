namespace LampsPlus.AutomationFramework.Databases.Queries.ShoppingCart
{
    /// <summary>
    /// Query to get the current discount rate of a company
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T160
    /// </summary>
    public class OrderTotalSelectedCompany
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 ordertotal
                                    FROM userprofile.dbo.tblcompany 
                                    WHERE companyname = @companyname
                                    ";
    }
}
