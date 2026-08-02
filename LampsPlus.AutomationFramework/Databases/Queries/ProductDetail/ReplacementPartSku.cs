namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify SKU with Replacement Part
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7786
    /// </summary>
    public class ReplacementPartSku
    {
        public const string Query = @"
        SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
        Use Carteasy
        SELECT 
        TOP 1 gm.[ShortSKU]
        FROM
        [ProductMicroservices].[Relationship].[tblRelationshipGroupMap] gm
        INNER JOIN [ProductMicroservices].[Relationship].[tblRelationshipItem] i WITH (nolock) ON gm.RelationshipGroupID = i.RelationshipGroupId
        INNER JOIN [Carteasy].[dbo].[tblPrducts] rp ON i.ShortSku = rp.ShortSku
        INNER JOIN [Carteasy].[dbo].[tblPrductsExtra] rpe ON rp.ShortSku = rpe.ShortSku
        INNER JOIN [ProductMicroservices].[Relationship].[tblRelationshipGroup] g  ON gm.RelationshipGroupID = g.RelationshipGroupId
        INNER JOIN [ProductMicroservices].[Relationship].[tblRelationshipType] t  ON g.RelationshipID = t.RelationshipID
        INNER JOIN [ProductMicroservices].[Relationship].[tblRelationshipDirection] d  ON t.RelationshipDirectionID = d.RelationshipDirectionID
        INNER JOIN [ProductMicroservices].[Relationship].[tblRelationshipAction] a ON t.RelationshipActionID = a.RelationshipActionID
        INNER JOIN [ProductMicroservices].[Relationship].[tblRelationshipSource] s  ON t.RelationshipSourceID = s.RelationshipSourceID
        INNER JOIN [Carteasy].[dbo].[tblPrducts] p ON p.ShortSku = gm.ShortSku
        INNER JOIN [Carteasy].[dbo].[tblPrductsExtra] pe ON p.ShortSku = pe.ShortSku
        INNER JOIN (
        SELECT
        pe.ShortSKU,
        b.BulbSKU
        FROM
        carteasy..tblprducts p 
        INNER JOIN carteasy..tblprductsextra pe  ON pe.shortsku = p.shortsku
        INNER JOIN carteasy..tblproductsavailability pa (nolock) ON pe.shortsku = pa.shortsku
        INNER JOIN Carteasy.dbo.tblBulbs b  ON pe.shortsku = b.ShortSku
        WHERE
        pe.isbuttoneligible = 1
        AND instock = 1
        AND clearanceflag = 0
        AND (
        saleprice1internet = 0
        OR saleprice1internet >= retailpriceinternet
        )
        AND Len(p.shortsku) = 5
        AND p.Bulbs = '1'
        ) AS TEST ON TEST.ShortSKU = P.ShortSKU
        WHERE
        t.[RelationshipID] = 1
        AND t.[RelationshipName] = 'Parts'
        AND d.[RelationshipDirectionID] = 2
        AND d.[RelationshipDirection] = 'unidirectional'
        AND s.[RelationshipSourceID] = 2
        AND s.[RelationshipSource] = 'rules based'
        AND a.[RelationshipActionID] = 1
        AND a.[RelationshipAction] = 'bundle'
        AND pe.IsButtonEligible = 1
        AND rpe.PartsListable = 1
        order by newid()";
    }
}
