namespace LampsPlus.AutomationFramework.Databases.Queries.Sort
{
    /// <summary>
    /// Query to find a parameter that can added to the end of a URL to bring the user to a hybrid page. The search path must contain the word 
    /// 'products' and it can NOT contain 'clearance', 'onsale', or 'daily-savings'. The splashcopy can NOT be NULL.
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T217
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T1194
    /// </summary>
    public class SearchPath
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 1 searchpath
                                    FROM carteasy.dbo.tblsearchsplashcontent 
                                    WHERE searchpath LIKE '%/products/%'
	                                    AND searchpath NOT LIKE '%clearance%'
	                                    AND searchpath NOT LIKE '%onsale%'
	                                    AND searchpath NOT LIKE '%daily-savings%'
                                        AND searchpath NOT LIKE '%designer-lighting%' -- (1/25/23) This page does not go to a Sort page.
                                        AND searchpath NOT LIKE '%manufacturer%' -- (3/30/23) Certain manufacturer pages re-direct to 'designer-lighting'.
	                                    AND splashcopy IS NOT NULL
	                                    AND sublocationcode = 9003
                                    ORDER BY NEWID()
                                    ";
    }
}
