namespace LampsPlus.AutomationFramework.Databases.Queries.ProductDetail
{
    /// <summary>
    /// Query to identify a SKU with specifications tables.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7819 and https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7820
    /// </summary>
    public class SkuThatHasSpecificationTable
    {
        public const string Query = @"
                                    SET TRANSACTION isolation level READ uncommitted;

DECLARE @CategoryAttributeId INT -- Get the attribute ID for 'Category'

SELECT @CategoryAttributeId = attributeid
FROM   productmicroservices.productinfrastructure.tblattribute
WHERE  attributename = 'Category' -- Get a SKU that should have spec tables

SELECT TOP 1 shortsku,
             finish,
             height
FROM   carteasy.dbo.tblprducts p
WHERE  p.listable = 1
       AND p.instock = 1
       AND ( finish IS NOT NULL
             AND finish != '' )
       AND ( height IS NOT NULL
             AND height != '' )
       AND EXISTS (SELECT TOP 1 MS.spectablesetid
                   FROM
[ProductMicroservices].[ProductInfrastructure].[tblmanagedspectableset]
MS
INNER JOIN
[ProductMicroservices].[ProductInfrastructure].[tblmanagedspectablesetattribute]
MA
ON MS.managedspectablesetid = MA.managedspectablesetid
LEFT JOIN [ProductMicroservices].productinfrastructure.tblattributeweight AW
ON MA.attributeid = AW.attributeid
-- Match to SKU attribute value for category only when IsPrimary=1. For all other attributes ignore IsPrimary
-- I accomplished this with a case statement. If the attribute is category then the value has to be 1, otherwise I compare the
-- IsPrimary to itself which will always return true
LEFT JOIN [ProductMicroservices].[ProductInfrastructure].[tblskuattributevalue]
  SAV (
  nolock)
ON SAV.attributevalueid = MA.attributevalueid
  AND SAV.isprimary = CASE
                        WHEN MA.attributeid = @CategoryAttributeId THEN
                        1
                        ELSE SAV.isprimary
                      END
  AND SAV.shortsku = p.shortsku
GROUP  BY MS.spectablesetid
--Only return records where the number of attributes defined in MA matches the number matching values in SAV
HAVING Count(1) = Sum(CASE
                 WHEN sav.shortsku IS NOT NULL THEN 1
                 ELSE 0
               END)
ORDER  BY Sum(Isnull(AW.attributeweight, 0.00)) DESC)
ORDER  BY Newid() 
                                    ";
    }
}