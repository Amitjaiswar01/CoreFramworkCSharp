namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify a SKU with the 'Other Options' slider. The product must belong to a finish family that has at least 2 other
    /// products. Intranet can not equal 1.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T235
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1212
    /// </summary>
    public class OtherOptionSectionSkus
    {
        public const string Query = @" 
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									DECLARE @finishFamily NVARCHAR(30),
                                            @IncludeSearchedSku BIT = 1

                                    SELECT @finishFamily = finishfamily
                                    FROM carteasy.dbo.tblprducts 
                                    WHERE shortsku = @ShortSku

                                    SELECT P.shortsku,
	                                    P.productname
                                    FROM carteasy.dbo.tblprducts P 
                                    LEFT JOIN carteasy.dbo.categories C 
	                                    ON P.category LIKE C.cat + '%'
                                    WHERE (
		                                    @IncludeSearchedSku = 1
		                                    OR (
			                                    @IncludeSearchedSku = 0
			                                    AND P.shortsku <> @ShortSku
			                                    )
		                                    )
	                                    AND P.listable = 1
	                                    AND P.intranetonly <> 1
	                                    AND P.instock = 1
	                                    AND P.finishfamily = @finishFamily
                                    ";
    }
}
