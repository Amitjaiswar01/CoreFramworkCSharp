using System.Net;

namespace Automation.Framework.Utilities
{
	public class ResponseModel
	{
		public HttpStatusCode StatusCode { get; set; }
		public string Content { get; set; }
	}
}