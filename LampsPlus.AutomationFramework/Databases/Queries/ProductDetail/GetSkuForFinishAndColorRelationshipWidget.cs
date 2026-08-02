namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify a SKU that qualifies for color and finish relationship widget.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T8003
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T8004
    /// </summary>
    public class SkuForFinishAndColorRelationshipWidget
    {
        public const string Query = @"
                                    USE productmicroservices
                                    SET nocount ON
                                    SET TRANSACTION isolation level READ uncommitted
                                    SELECT TOP 1 RI.shortsku,
                                    RI.[label],
                                    RI.[sortorder],
                                    RT.[relationshipid],
                                    RT.[relationshipname],
                                    RT.[relationshipdirectionid],
                                    RT.[relationshipactionid],
                                    RT.[relationshipsourceid],
                                    RG.[relationshipgroupid],
                                    RG.[relationshipid],
                                    RI.[relationshipitemid],
                                    RI.[relationshipgroupid],
                                    RI.[shortsku],
                                    RI.[parentitemid]
                                    FROM [ProductMicroservices].[Relationship].[tblrelationshiptype] RT
                                    INNER JOIN [ProductMicroservices].[Relationship].[tblrelationshipgroup]
                                    RG
                                    ON RT.relationshipid = RG.relationshipid
                                    INNER JOIN [ProductMicroservices].[Relationship].[tblrelationshipitem] RI
                                    ON RG.relationshipgroupid = RI.relationshipgroupid
                                    INNER JOIN carteasy.dbo.tblprducts P
                                    ON P.shortsku = RI.shortsku
                                    INNER JOIN carteasy.dbo.tblprductsextra px
                                    ON PX.shortsku = RI.shortsku
                                    INNER JOIN carteasy.dbo.tblproductsavailability PA
                                    ON P.shortsku = PA.shortsku
                                    LEFT JOIN carteasy.dbo.categories C
                                    ON C.cat = ( CASE
                                    WHEN Charindex(',', P.category) = 0 THEN P.category
                                    ELSE LEFT(P.category, Charindex(',', P.category)
                                    - 1)
                                    END )
                                    INNER JOIN [Relationship].[tblrelationshipcategoryfilters] RCF
                                    ON ( RCF.category = C.catname
                                    AND RT.relationshipname = RCF.relationshipname )
                                    WHERE RI.[label] IS NOT NULL
                                    AND P.instock = 1
                                    AND P.listable = 1
                                    AND PA.firstshipdays < 57
                                    AND isbuttoneligible = 1
                                    AND RT.relationshipname IN ( 'Finish', 'Motor finish', 'Color',
                                    'Glass Color' )
                                    ORDER BY Newid()";
    }
}
