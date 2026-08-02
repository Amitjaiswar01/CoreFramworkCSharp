namespace LampsPlus.AutomationFramework.Databases.Queries.ShoppingCartEmployee
{
    /// <summary>
    /// Query to verify the list of SKUs, Quantities and EmployeeNumber
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-7762
    /// </summary>
    class ReturnCartDetail
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
								
									USE Assets
                                    SELECT cli.ShortSKU, cli.Quantity,cl.EmployeeNumber, cl.EmployeeCartId FROM tblCartLink cl
                                    inner join tblCartLinkItem cli on cli.CartLinkId = cl.Id
                                    WHERE cl.EmployeeCartId = @employeeCartId
                                    order by CreatedDate desc
                                    ";
    }
}
