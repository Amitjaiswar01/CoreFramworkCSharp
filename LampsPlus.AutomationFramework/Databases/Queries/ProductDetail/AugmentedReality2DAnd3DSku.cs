namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify any SKU that has a Brand.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7457
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T242
    /// </summary>
    public class AugmentedReality2DAnd3DSku
    {
        public const string Query = @"
                                    USE carteasy 

                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

                                    SELECT TOP 1 AR.shortsku
                                    FROM   tblprducts AS PA
                                           INNER JOIN carteasy.dbo.tblprductsextra PX
                                                   ON PA.shortsku = PX.shortsku
                                           INNER JOIN tblarproducts AS AR
                                                   ON PA.shortsku = AR.shortsku
                                    WHERE  PA.instock = 1
                                           AND PX.isbuttoneligible = 1
                                    ORDER  BY Newid() 
                                    ";
    }
}
