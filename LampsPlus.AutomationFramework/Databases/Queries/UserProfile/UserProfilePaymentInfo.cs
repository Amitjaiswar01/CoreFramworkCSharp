namespace LampsPlus.AutomationFramework.Databases.Queries.UserProfile
{
    /// <summary>
    /// Query to get Payment Information
    /// Automated Test Case: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T556
    /// </summary>
    public class UserProfilePaymentInfo
    {
        public static string Query => @"
                                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
									
									SELECT TOP 10 lastfourdigit, 
                                                  cardholdername, 
                                                  expirationdate, 
                                                  cardtype, 
                                                  paymenttoken,
                                                  billingfirstname, 
                                                  billinglastname, 
                                                  address1, 
                                                  address2, 
                                                  city, 
                                                  state, 
                                                  zip, 
                                                  country, 
                                                  phonenumber,
                                                  rewardnumber
                                    FROM   userprofile.dbo.tblpaymentinfo 
                                    WHERE    rewardnumber = @RewardNumber
                                    ";
    }
}
