using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace LampsPlus.AutomationFramework.Utilities.External.Nada
{
	/// <summary>
	/// Nada email client model.
	/// Overview of the concept of email API via JSON:
	/// http://www.testautomationguru.com/selenium-webdriver-email-validation-with-disposable-email-addresses/
	/// </summary>
	public static class NadaClient
	{
        /// <summary>
        /// Method to get the details of a Nada email address.
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
		public static EmailMessageModel GetAccountVerificationEmail(string emailAddress)
		{
			var rtnEmailAddress = CreateNadaEmailAddress(emailAddress);
			var objEmailAddress = JObject.Parse(rtnEmailAddress);
			var uid = objEmailAddress.SelectToken("msgs[0].uid").ToString();

			var rtnEmailMessage = GetNadaMessage(uid);
			var objEmailMessage = JObject.Parse(rtnEmailMessage);
			var emailMessageModel = new EmailMessageModel
			{
				Uid = uid,
				Subject = objEmailMessage.SelectToken("s").ToString(),
				From = objEmailMessage.SelectToken("f").ToString(),
				To = objEmailMessage.SelectToken("ib").ToString()
			};

			return emailMessageModel;
		}

		/// <summary>
		/// Method to create a new email inbox and get message list.
		/// Example: https://getnada.com/api/v1/inboxes/lampsplusautomation20190201113135@getnada.com
		/// </summary>
		/// <param name="emailAddress"></param>
		/// <returns></returns>
		private static string CreateNadaEmailAddress(string emailAddress)
		{
			return GetResponse($"https://getnada.com/api/v1/inboxes/{emailAddress}");
		}

		/// <summary>
		/// Method to get an email message by its uid.
		/// Example: https://getnada.com/api/v1/messages/XE3hwoFMSeJrNlBOrCn7uAvJxBmVTj
		/// </summary>
		/// <param name="messageId"></param>
		/// <returns></returns>
		private static string GetNadaMessage(string messageId)
		{
			return GetResponse($"https://getnada.com/api/v1/messages/{messageId}");
		}

		/// <summary>
		/// Method to make a HTTP request to a given API URL.
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		private static string GetResponse(string url)
		{
			var request = (HttpWebRequest) WebRequest.Create(url);
			var response = request.GetResponse();
			using (var responseStream = response.GetResponseStream())
			{
				var reader = new StreamReader(responseStream, Encoding.UTF8);
				return reader.ReadToEnd();
			}
		}
	}
}
