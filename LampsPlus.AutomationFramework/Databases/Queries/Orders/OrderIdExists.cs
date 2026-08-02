

namespace LampsPlus.AutomationFramework.Databases.Queries.Orders
{
	/// <summary>
	/// https://lampstrack.lampsplus.com:8443/browse/ACD-6582
	/// </summary>
	public class OrderIdExists
	{
		public static string QueryForOrderHeader(string orderId) => $@"
                                                        SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
														SELECT OrderID, CommissionEmployee, CashierEmployee
			                                            FROM   assets.dbo.tblglobalorderheader 
			                                            WHERE orderid = '{orderId}'";
	}
}
