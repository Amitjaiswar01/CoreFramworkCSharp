namespace LampsPlus.AutomationFramework.Enums
{

	/// <summary>
	///  The back-end code generates a random SurveyId to be used on the front-end to decide if and what survey to display to the user.
	/// </summary>
	public enum UtagDataSurveyId
	{
		/// <summary>
		/// Unknown - nothing will be displayed
		/// </summary>
		Unknown = 0,

		/// <summary>
		/// Google Survey when the utag_data.survey_id is 1
		/// </summary>
		GoogleSurvey = 1,

		/// <summary>
		/// BizRate Survey when the utag_data.survey_id is 2
		/// </summary>
		BizRateSurvey = 2,

		/// <summary>
		/// No survey pops up when the utag_data.survey_id is 3
		/// </summary>
		NoSurvey = 3,

		/// <summary>
		/// TrustPilot Survey - when the utag_data.survey_id is 4
		/// </summary>
		TrustPilotSurvey = 4
	}
}
