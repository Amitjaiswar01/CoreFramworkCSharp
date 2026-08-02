namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to verify parts of  SKU with Replacement Part
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7786
    /// </summary>
    public class ReplacementPartDetails
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
		                            Use Carteasy

                                    SELECT 
                                         gm.ShortSKU as PrimaryShortSKU, i.ShortSKU as PartShortSku
                                    FROM
                                        [ProductMicroservices].[Relationship].[tblRelationshipGroupMap] gm
                                        INNER JOIN [ProductMicroservices].[Relationship].[tblRelationshipItem] i ON gm.RelationshipGroupID = i.RelationshipGroupId
                                        INNER JOIN [Carteasy].[dbo].[tblPrductsExtra] rpe ON i.ShortSku = rpe.ShortSku
                                        INNER JOIN [ProductMicroservices].[Relationship].[tblRelationshipGroup] g  ON gm.RelationshipGroupID = g.RelationshipGroupId
                                        INNER JOIN [ProductMicroservices].[Relationship].[tblRelationshipType] t  ON g.RelationshipID = t.RelationshipID
                                    WHERE
                                        t.[RelationshipID] = 1
                                        AND t.[RelationshipName] = 'Parts'
                                        AND rpe.PartsListable = 1
	                                    AND gm.ShortSKU =@parentsku
                                    order by i.SortOrder
                                    ";
    }
}