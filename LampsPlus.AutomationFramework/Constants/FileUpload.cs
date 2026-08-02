namespace LampsPlus.AutomationFramework.Constants
{
    /// <summary>
    /// File paths to be used for tests that require file upload.
    /// </summary>
    public static class FileUpload
    {
        // All test files for upload should be stored here (Local or Network path that is accessible from where the browser is being run)
        private static readonly string BaseUploadPath = @"\\LPSELDOCKER01\TestUploads";

		/// <summary>
		/// File to test Slyce Camera search by Lamp image.
		/// </summary>
	    public static readonly string LampImagePath = $@"{BaseUploadPath}\Lamp_Slyce_Camera_Table_Lamp_Search.png";


		/// <summary>
		/// File path of the photo to be uploaded when writing a TurnTo Review on PDP.
		/// </summary>
		public static readonly string TurnToReviewPhotoUploadPath = $@"{BaseUploadPath}\PDP_TurnTo_Review_Photo_For_Upload.png";

        /// <summary>
        /// File path of the photo to be uploaded when navigating through the AR.
        /// </summary>
        public static readonly string ArUploadPath = $@"{BaseUploadPath}\Ar_SampleImage_Upload.png";
    }
}
