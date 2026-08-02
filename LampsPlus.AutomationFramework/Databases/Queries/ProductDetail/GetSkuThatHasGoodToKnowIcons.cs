namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify any SKU that has Good to Know Icon
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7825
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7826
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7827
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7828
    /// </summary>

    public class GetSkuThatHasGoodToKnowIconsLabel
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
                                    Use ProductMicroservices

                                    DECLARE @CategoryAttributeId INT

                                    -- Get the attribute ID for 'Category'
                                    SELECT @CategoryAttributeId = AttributeId FROM ProductMicroservices.ProductInfrastructure.tblAttribute WHERE AttributeName='Category'

                                    -- Get all SKUs that should have spec tables
                                    SELECT countbysku.ShortSku,
                                        cbms.SpecTableSetId
                                    INTO #SkusWithSpecTables
                                    FROM
                                        Carteasy.dbo.TblPrducts p INNER JOIN
                                        (
                                            -- Get the count of all attributes for each SKU that match the attributes in a spec table
                                            SELECT
                                                sav.ShortSku,
                                                ms.ManagedSpecTableSetId,
                                                COUNT(1) AS NumberOfAttributes
                                            FROM ProductInfrastructure.tblManagedSpecTableSet ms
                                                INNER JOIN ProductInfrastructure.tblManagedSpecTableSetAttribute msa ON msa.ManagedSpecTableSetId = ms.ManagedSpecTableSetId
                                                INNER JOIN ProductInfrastructure.tblSkuAttributeValue sav ON sav.AttributeValueId = msa.AttributeValueId AND SAV.IsPrimary = CASE WHEN msa.AttributeId = @CategoryAttributeId THEN 1 ELSE SAV.IsPrimary END
                                            GROUP BY sav.ShortSku,
                                                        ms.ManagedSpecTableSetId
                                        ) countbysku ON countbysku.ShortSku = p.ShortSKU
                                        INNER JOIN
                                        (
                                            -- Get the count of all possible attributes for a spec table and the weight for that spec table
                                            SELECT
                                                ms.ManagedSpecTableSetId,
                                                ms.SpecTableSetId,
                                                SUM(aw.AttributeWeight) AS [Weight],
                                                COUNT(1) AS NumberOfAttributes
                                            FROM ProductInfrastructure.tblManagedSpecTableSet ms
                                                INNER JOIN ProductInfrastructure.tblManagedSpecTableSetAttribute msa ON msa.ManagedSpecTableSetId = ms.ManagedSpecTableSetId
                                                LEFT OUTER JOIN ProductInfrastructure.tblAttributeWeight aw ON aw.AttributeId = msa.AttributeId
                                            GROUP BY ms.ManagedSpecTableSetId, ms.SpecTableSetId
                                        ) cbms
                                            ON countbysku.ManagedSpecTableSetId = cbms.ManagedSpecTableSetId
                                    WHERE
                                        cbms.NumberOfAttributes = countbysku.NumberOfAttributes
                                        AND
                                    p.Listable = 1
                                        AND
                                        p.InStock = 1

                                    -- Find SKUs that have good to know specs - motion sensor
                                    SELECT TOP 1
                                    'Motion Sensor'
                                    ,sav.ShortSku
                                    FROM #SkusWithSpecTables ss
                                    INNER JOIN ProductMicroservices.ProductInfrastructure.tblSpecTableSet st ON st.SpecTableSetId = ss.SpecTableSetId
                                    INNER JOIN ProductMicroservices.ProductInfrastructure.tblSpecTableSetAttributeMap sam ON sam.SpecTableSetId = st.SpecTableSetId AND sam.SpecificationTypeId=3
                                    INNER JOIN ProductMicroservices.ProductInfrastructure.tblSpecTableAttribute sa ON sa.SpecTableAttributeId = sam.SpecTableAttributeId
                                    INNER JOIN ProductInfrastructure.tblSpecTableAttributeDisplayValue dv ON dv.SpecTableAttributeId = sa.SpecTableAttributeId
                                    INNER JOIN ProductMicroservices.ProductInfrastructure.tblSkuAttributeValue sav ON sav.ShortSku = ss.ShortSKU AND sav.AttributeValueId = dv.DisplayIfValueIs
                                    WHERE
                                    sa.Label = 'Motion Sensor'
                                    ORDER BY NEWID()

                                    -- Find SKUs that have good to know specs - solar
                                    SELECT TOP 1
                                    'Solar'
                                    ,sav.ShortSku
                                    FROM #SkusWithSpecTables ss
                                    INNER JOIN ProductMicroservices.ProductInfrastructure.tblSpecTableSet st ON st.SpecTableSetId = ss.SpecTableSetId
                                    INNER JOIN ProductMicroservices.ProductInfrastructure.tblSpecTableSetAttributeMap sam ON sam.SpecTableSetId = st.SpecTableSetId AND sam.SpecificationTypeId=3
                                    INNER JOIN ProductMicroservices.ProductInfrastructure.tblSpecTableAttribute sa ON sa.SpecTableAttributeId = sam.SpecTableAttributeId
                                    INNER JOIN ProductInfrastructure.tblSpecTableAttributeDisplayValue dv ON dv.SpecTableAttributeId = sa.SpecTableAttributeId
                                    INNER JOIN ProductMicroservices.ProductInfrastructure.tblSkuAttributeValue sav ON sav.ShortSku = ss.ShortSKU AND sav.AttributeValueId = dv.DisplayIfValueIs
                                    WHERE
                                    sa.Label = 'Solar'
                                    ORDER BY NEWID()

                                    -- Find SKUs that have good to know specs - LED
                                    SELECT TOP 1
                                    'LED'
                                    ,sav.ShortSku
                                    FROM #SkusWithSpecTables ss
                                    INNER JOIN ProductMicroservices.ProductInfrastructure.tblSpecTableSet st ON st.SpecTableSetId = ss.SpecTableSetId
                                    INNER JOIN ProductMicroservices.ProductInfrastructure.tblSpecTableSetAttributeMap sam ON sam.SpecTableSetId = st.SpecTableSetId AND sam.SpecificationTypeId=3
                                    INNER JOIN ProductMicroservices.ProductInfrastructure.tblSpecTableAttribute sa ON sa.SpecTableAttributeId = sam.SpecTableAttributeId
                                    INNER JOIN ProductInfrastructure.tblSpecTableAttributeDisplayValue dv ON dv.SpecTableAttributeId = sa.SpecTableAttributeId
                                    INNER JOIN ProductMicroservices.ProductInfrastructure.tblSkuAttributeValue sav ON sav.ShortSku = ss.ShortSKU AND sav.AttributeValueId = dv.DisplayIfValueIs
                                    WHERE
                                    sa.Label = 'LED'
                                    ORDER BY NEWID()

                                    -- Find SKUs that have good to know specs - dusk to dawn
                                    SELECT TOP 1
                                    'Dusk to Dawn'
                                    ,sav.ShortSku
                                    FROM #SkusWithSpecTables ss
                                    INNER JOIN ProductMicroservices.ProductInfrastructure.tblSpecTableSet st ON st.SpecTableSetId = ss.SpecTableSetId
                                    INNER JOIN ProductMicroservices.ProductInfrastructure.tblSpecTableSetAttributeMap sam ON sam.SpecTableSetId = st.SpecTableSetId AND sam.SpecificationTypeId=3
                                    INNER JOIN ProductMicroservices.ProductInfrastructure.tblSpecTableAttribute sa ON sa.SpecTableAttributeId = sam.SpecTableAttributeId
                                    INNER JOIN ProductInfrastructure.tblSpecTableAttributeDisplayValue dv ON dv.SpecTableAttributeId = sa.SpecTableAttributeId
                                    INNER JOIN ProductMicroservices.ProductInfrastructure.tblSkuAttributeValue sav ON sav.ShortSku = ss.ShortSKU AND sav.AttributeValueId = dv.DisplayIfValueIs
                                    WHERE
                                    sa.Label = 'Dusk to Dawn'
                                    ORDER BY NEWID()

                                    -- Find SKUs that have good to know specs - dark sky
                                    SELECT TOP 1
                                    'Dark Sky'
                                    ,sav.ShortSku
                                    FROM #SkusWithSpecTables ss
                                    INNER JOIN ProductMicroservices.ProductInfrastructure.tblSpecTableSet st ON st.SpecTableSetId = ss.SpecTableSetId
                                    INNER JOIN ProductMicroservices.ProductInfrastructure.tblSpecTableSetAttributeMap sam ON sam.SpecTableSetId = st.SpecTableSetId AND sam.SpecificationTypeId=3
                                    INNER JOIN ProductMicroservices.ProductInfrastructure.tblSpecTableAttribute sa ON sa.SpecTableAttributeId = sam.SpecTableAttributeId
                                    INNER JOIN ProductInfrastructure.tblSpecTableAttributeDisplayValue dv ON dv.SpecTableAttributeId = sa.SpecTableAttributeId
                                    INNER JOIN ProductMicroservices.ProductInfrastructure.tblSkuAttributeValue sav ON sav.ShortSku = ss.ShortSKU AND sav.AttributeValueId = dv.DisplayIfValueIs
                                    WHERE
                                    sa.Label = 'Dark Sky'
                                    ORDER BY NEWID()

                                    DROP TABLE #SkusWithSpecTables
                                    ";
    }
}