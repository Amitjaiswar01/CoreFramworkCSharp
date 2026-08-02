namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
	/// <summary>
	/// Query to identify a SKU where the difference between the initial retail price and the current retail price is greater than or equal to $1.
	/// The initial retail price can not be NULL. The initial retail price cannot be equal to the retail price. The clearance flag is set to 'true'.
	/// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T248
	/// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1226
	/// </summary>
	public class SkuSavePriceFiveAndOver
	{
		public const string Query = @"
								 SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

                                 SELECT TOP 1 p.shortsku, initialretailprice, retailprice, retailpriceinternet,
								 Round(floor( initialretailprice - retailpriceinternet ), 0) AS SAVING, *
								 FROM carteasy.dbo.tblprductsextra pe 
								 INNER JOIN carteasy.dbo.tblprducts p ON p.shortsku = pe.shortsku
								 WHERE pe.isbuttoneligible = 1 AND instock = 1
								 AND p.department NOT BETWEEN 80 AND 89
								 AND clearanceflag = 1 AND (SalePrice1Internet = 0 OR SalePrice1Internet >= RetailPriceInternet)
								 AND ( Round(Floor( initialretailprice - retailpriceinternet ), 0) > 5.00 )
								 ORDER BY Newid()
										";
		}
	}
