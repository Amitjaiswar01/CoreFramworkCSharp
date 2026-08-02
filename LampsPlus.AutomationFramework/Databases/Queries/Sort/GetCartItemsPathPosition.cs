namespace LampsPlus.AutomationFramework.Databases.Queries.Sort
{
    /// <summary>
    /// Query to get cart items with path and position
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7056
    /// </summary>
    public class GetCartItemsPathPosition
    {
        public static string Query(string cartId) => $@"
                                                        SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
														
														SELECT ShortSku, sp.SortPath, cs.SortPosition
                                                        FROM [Assets].[dbo].[tblCartSharedItems] cs 
                                                        INNER JOIN carteasy..tblsortpath sp 
                                                        ON cs.SortPathID = sp.id
                                                        WHERE cartid = '{cartId}'
                                                        ";
    }
}
