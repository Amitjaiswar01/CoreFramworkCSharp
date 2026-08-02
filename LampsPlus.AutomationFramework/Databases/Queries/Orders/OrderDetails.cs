namespace LampsPlus.AutomationFramework.Databases.Queries.Orders
{
    /// <summary>
    /// Query to get order details
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T275
    /// Manual Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T827
    /// </summary>
    public class OrderDetails
    {
        public const string Query = @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT DISTINCT goh.orderid, 
                                            d.createddate AS OrderDate,
		                                    Inv.createddate AS ShipDate,
                                            d.emailaddress, 
                                            CASE 
                                                WHEN isnull(d.commissionEmployee, '') = '' THEN d.cashierEmployee 
                                                ELSE d.commissionemployee 
                                            END                        AS SalesAssociate, 
                                            gp.billtofirstname, 
                                            gp.billtolastname, 
                                            gp.billtoaddressline1, 
                                            gp.billtoaddressline2, 
                                            gp.billtocity, 
                                            gp.billtostate, 
                                            gp.billtozipcode, 
                                            gp.billtocountry, 
                                            gp.billtophonenumber, 
                                            ( si.itemtotal * si.quantity ) AS ExtPrice, 
                                            paymentmethod, 
                                            gp.creditcardtype, 
                                            gp.creditcardlastfour, 
                                            d.rewardnumber, 
                                            si.shiptofirstname, 
                                            si.shiptolastname, 
                                            si.shiptoaddressline1, 
                                            si.shiptoaddressline2, 
                                            si.shiptocity, 
                                            si.shiptostate, 
                                            si.shiptozipcode, 
                                            si.shiptocountry, 
                                            si.ShipToPhoneNumber, 
                                            si.productname, 
                                            si.shortsku, 
                                            si.itemstatus, 
                                            si.expectedshipdate, 
                                            si.firstshipdate, 
                                            si.lastshipdate, 
                                            si.firstdeliverydate, 
                                            si.lastdeliverydate, 
                                            si.origitemshiptype         AS TrackingType, 
                                            si.trackingnumber, 
                                            si.quantity, 
                                            si.price                    AS UnitPrice, 
                                            d.itemtotal, 
                                            d.manualdiscount           AS PriceAdjustment, 
                                            d.freighttotal, 
                                            d.taxtotal, 
                                            d.ordertotal, 
                                            gp.paymentmethod,
                                            si.ID
                                    FROM   assets.dbo.tblglobalorderheader goh  
                                        INNER JOIN domexportorder.dbo.tbldomexportorderheader d  ON goh.orderid = d.orderid 
                                        INNER JOIN carteasy.dbo.tblshareditems si  ON si.orderid = goh.orderid 
                                        INNER JOIN assets.dbo.tblglobalpayment gp  ON gp.orderid = goh.orderid AND gp.includedinconvertedorder = 1 
	                                    OUTER APPLY (
				                                    SELECT deii.OrderId,deii.ParentOrderLineNumber,dei.createddate
				                                    FROM DomExportOrder.dbo.tblDomExportInvoiceItem deii 
				                                    INNER JOIN domexportorder.dbo.tblDomExportInvoice dei  ON dei.InvoiceNumber = deii.InvoiceNumber AND dei.OrderId = deii.OrderId AND dei.InvoiceType NOT IN ('Adjustment', 'Return')
				                                    WHERE  deii.orderid = goh.orderid AND deii.ParentOrderLineNumber=si.LineNumber
				                                    ) AS Inv
                                    WHERE  goh.orderid = @orderID
                                    ORDER BY si.ID
                                    ";
    }
}
