namespace LampsPlus.AutomationFramework.Databases.Queries.SortCallout
{
    /// <summary>
    /// Query to verify that a SKU is eligible to display the Sale callout.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T211
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1188
    /// </summary>
    public class SalePriceByShortSku
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 SalePrice1Internet,
	                                    RetailPriceInternet,
                                        Listable,
                                        InStock,
	                                    shortsku
                                    FROM carteasy.dbo.tblprducts 
                                    WHERE shortsku = @shortsku
                                    ";
    }
}
