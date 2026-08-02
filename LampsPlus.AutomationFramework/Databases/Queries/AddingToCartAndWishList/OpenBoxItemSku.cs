namespace LampsPlus.AutomationFramework.Databases.Queries.AddingToCartAndWishList
{
    /// <summary>
    /// Query To Find Open-Box Item
    /// </summary>
    public class OpenBoxItemSku
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 p.shortsku, 
                                                 p.productname, 
                                                 px.inventory58 
                                    FROM   carteasy.dbo.tblprducts p 
                                           INNER JOIN carteasy.dbo.tblprductsextra px
                                                   ON p.shortsku = px.shortsku 
                                    WHERE  p.listable = 1 
                                           AND p.instock = 1 
                                           AND px.listable58 = 1 
                                           AND px.inventory58 > 1 -- (11/02/23) Added in order to have a dropdown box on PDP as the inventory will have more than one products. 
                                           AND px.inventory58 < 20 
                                           AND p.endsale1 IS NULL
                                           AND HasCroppedImage = 1 -- (11/14/23) Added to ensure the selected SKU has thumbnail images.
                                    ORDER  BY Newid() ";
    }
}
