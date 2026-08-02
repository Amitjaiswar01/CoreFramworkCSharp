namespace LampsPlus.AutomationFramework.Databases.Queries.Sort
{
    /// <summary>
    /// Query to get sort url to navigate to the respective sort page
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7751
    /// </summary>
    public class GetSortUrlForFds
    {
        public const string Query = @"
                                    IF ( Object_id('TempDB..#TempData') IS NOT NULL )
                                      BEGIN
                                          DROP TABLE tempdb..#tempdata
                                      END

                                    SELECT TOP 2 MS.sorturl,
                                                 fds.filterdisplaysetid,
                                                 fds.defaultvisiblemenucount
                                    INTO   #tempdata
                                    FROM   productmicroservices.productinfrastructure.tblmanagedsort MS WITH(nolock)
                                           INNER JOIN
                                           productmicroservices.productinfrastructure.tblmanagedsortfilterdisplayset
                                           MFDS
                                           WITH
                                           (nolock)
                                                   ON MFDS.managedsortid = MS.managedsortid
                                           INNER JOIN productmicroservices.productinfrastructure.tblfilterdisplayset
                                                      FDS
                                                      WITH (nolock)
                                                   ON FDS.filterdisplaysetid = MFDS.filterdisplaysetid
                                           INNER JOIN
                                           productmicroservices.productinfrastructure.tblfilterdisplaytype FDT
                                           WITH (nolock
                                                                                                       )
                                                   ON FDT.filterdisplaytypeid = fds.filterdisplaytypeid
                                    WHERE  ms.managedsortid != 5
                                           AND FDT.displaydevicetypeid = 1 --1 for Desktop or 2 for Mobile
                                    ORDER  BY Newid();

                                    WITH cte
                                         AS (SELECT DISTINCT Temp.sorturl                 AS SortUrl,
                                                             Temp.defaultvisiblemenucount AS DefaultVisibleMenuCount
                                                             ,
                                                             AFDT.displaytype
                                                             AS AttributeDisplayType,
                                                             FDSAG.attributegroupdisplayname,
                                                             AFDT.attributefilterdisplaytypeid
                                             FROM   #tempdata Temp
                                                    INNER JOIN productinfrastructure.tblfilterdisplayset FDS WITH(
                                                               nolock)
                                                            ON Temp.filterdisplaysetid = FDS.filterdisplaysetid
                                                    INNER JOIN
                                                    productinfrastructure.tblfilterdisplaysetattributegroup
                                                    FDSAG
                                                    WITH(
                                                    nolock)
                                                            ON FDSAG.filterdisplaysetid = FDS.filterdisplaysetid
                                                    INNER JOIN productinfrastructure.tblfilterdisplaysetattribute
                                                               FDSA
                                                               WITH(
                                                               nolock)
                                                            ON FDSA.filterdisplaysetattributegroupid =
                                                               FDSAG.filterdisplaysetattributegroupid
                                                    INNER JOIN
                                                    productinfrastructure.tblattributegroupfilterdisplaytype
                                                    AGFDT WITH(
                                                    nolock)
                                                            ON AGFDT.attributegroupfilterdisplaytypeid =
                                                               FDSAG.attributegroupdisplaytypeid
                                                    INNER JOIN productinfrastructure.tblattributefilterdisplaytype
                                                               AFDT
                                                               WITH
                                                               (nolock)
                                                            ON AFDT.attributefilterdisplaytypeid =
                                                               FDSA.attributedisplaytypeid)
                                    SELECT C1.*
                                    FROM   cte C1
                                           INNER JOIN (SELECT attributedisplaytype,
                                                              Count(1) AS Cnt
                                                       FROM   cte
                                                       GROUP  BY attributedisplaytype) AS C2
                                                   ON C2.attributedisplaytype = C1.attributedisplaytype
                                    ORDER  BY C1.sorturl,
                                              C2.cnt,
                                     C1.attributedisplaytype
                                    ";
    }
}
