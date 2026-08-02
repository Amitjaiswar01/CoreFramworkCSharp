namespace LampsPlus.AutomationFramework.Utilities.External.Nada
{
	/// <summary>
	/// Model representing an email message object.
	/// </summary>
	public class EmailMessageModel
	{
        public string Uid { get; set; }
		public string From { get; set; }
		public string To { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
    }
}
