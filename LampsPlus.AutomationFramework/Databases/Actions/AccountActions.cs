using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using LampsPlus.AutomationFramework.Databases.Entities;
using LampsPlus.AutomationFramework.Databases.Queries.UserProfile;
using LampsPlus.AutomationFramework.Utilities;

namespace LampsPlus.AutomationFramework.Databases.Actions
{
    /// <summary>
    /// Helper to provide access to account related database queries.
    /// </summary>
    public class AccountActions
    {
        /// <summary>
        /// Helper to provide access to account related database queries.
        /// </summary>
        public AccountActions(string cartEasyConnectionString)
        {
            CartEasyConnectionString = cartEasyConnectionString;
        }

        /// <summary>
        /// CartEasy database connection string.
        /// </summary>
        public string CartEasyConnectionString { get; }

        /// <summary>
        /// Get a user account based on the given email address.
        /// </summary>
        /// <param name="email">Email address of a given user in the database.</param>
        /// <returns>NewUserAccount object with the account email and IsApproved status.</returns>
        public NewUserAccount GetUserByEmail(string email)
        {
            NewUserAccount newUserAccount = null;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(UserProfile.Query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@Email", email));
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        newUserAccount = new NewUserAccount
                        {
                            Email = (string)reader["email"],
                            IsApproved = Convert.ToInt32(reader["IsApproved"])
                        };
                    }
                }
            }

            return newUserAccount;
        }

        /// <summary>
        /// Get users profile with a given email address
        /// </summary>
        /// <param name="email">Email address of a given user in the database.</param>
        /// <returns>OptOutModel object with the status for OptOutEmail, SubLocation codes, reward number, and optout value.</returns>
        public List<OptOutModel> GetUserProfileOptOutValuesList(string email)
        {
            var model = new List<OptOutModel>();
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(UserProfileOptOutValues.Query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@Email", email));
                    conn.Open();
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        model.Add(new OptOutModel()
                        {
                            RewardNumber = reader["rewardnumber"].ToString(),
                            SubLocation = reader["sublocation"].ToString(),
                            OptOutEmail = reader["optoutemail"].ToString()
                        });
                    }
                }
            }

            return model;
        }

        public ProductNotifyMeDetails GetEmailAndSku(string email, string shortsku)
        {
            Thread.Sleep(1000);
            ProductNotifyMeDetails productNotifyMeDetails = null;
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(NotifyMeEmail.Query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@Email", email));
                    cmd.Parameters.Add(new SqlParameter("@ShortSku", shortsku));
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        productNotifyMeDetails = new ProductNotifyMeDetails
                        {
                            EmailAddress = Convert.ToString(reader["emailaddress"]),
                            ShortSku = Convert.ToString(reader["shortsku"])
                        };
                    }
                }
            }

            return productNotifyMeDetails;
        }
        
        /// <summary>
        /// Get payment info with a given email last four of card
        /// </summary>
        /// <param name="rewardNumber">Reward number of user account.</param>
        /// <returns>PaymentInfoModel obj.</returns>
        public List<PaymentInfoModel> GetPaymentInfoFromUser(long rewardNumber)
        {
            var model = new List<PaymentInfoModel>();

            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(UserProfilePaymentInfo.Query, conn))
				{
					cmd.Parameters.Add(new SqlParameter("@RewardNumber", SqlDbType.BigInt) { Value = rewardNumber });
                    conn.Open();
                    var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    while (reader.Read())
                    {
                        model.Add(new PaymentInfoModel
                        {
                            LastFourDigit = reader["lastfourdigit"].ToString(),
                            CardholderName = reader["cardholdername"].ToString(),
                            ExpirationDate = reader["expirationdate"].ToString(),
                            CardType = reader["cardtype"].ToString(),
                            PaymentToken = reader["paymenttoken"].ToString(),
                            BillingFirstName = reader["billingfirstname"].ToString(),
                            BillingLastName = reader["billinglastname"].ToString(),
                            Address1 = reader["address1"].ToString(),
                            Address2 = reader["address2"].ToString(),
                            City = reader["city"].ToString(),
                            State = reader["state"].ToString(),
                            Zip = reader["zip"].ToString(),
                            Country = reader["country"].ToString(),
                            PhoneNumber = reader["phonenumber"].ToString(),
                        });
                    }
                }
            }

            return model;
        }
        
        /// <summary>
        /// Delete user's payment options
        /// </summary>
        /// <param name="rewardNumber">rewardNumber of a given user in the database.</param>
        public void ResetPaymentOptions(string rewardNumber)
        {
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(ResetUserPaymentOptions.Query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@RewardNumber", rewardNumber));
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Delete user's shipping addresses
        /// </summary>
        /// <param name="rewardNumber">rewardNumber of a given user in the database.</param>
        public void ResetShippingAddresses(string rewardNumber)
        {
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(ResetUserShippingAddresses.Query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@RewardNumber", rewardNumber));
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Reset user's name and phone number to default values
        /// </summary>
        /// <param name="rewardNumber">rewardNumber of a given user in the database.</param>
        /// TODO: UMS - How do we resolve this?
        public void ResetNamePhone(string rewardNumber)
        {
            var customerLoginAccount = LampsPlusAccounts.CustomerLoginAccount; // TODO: UMS - This won't work

            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(UpdateUserNamePhone.Query, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@RewardNumber", rewardNumber));
                    cmd.Parameters.Add(new SqlParameter("@FirstName", customerLoginAccount.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@LastName", customerLoginAccount.LastName));
                    cmd.Parameters.Add(new SqlParameter("@phoneNumber", customerLoginAccount.PhoneNumber));
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        /// <summary>
        /// Enable deactivated user accounts.
        /// </summary>
        /// <param name="usersFirstName">User first name.</param>
        public void ActivateUsersByFirstName(string usersFirstName)
        {
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(EnableDisabledUsers.Action, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@UserFirstName", usersFirstName));
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Deactivate user account.
        /// </summary>
        /// <param name="userEmail">User first name.</param>
        public void DeactivateUser(string userEmail)
        {
            using (var conn = new SqlConnection(CartEasyConnectionString))
            {
                using (var cmd = new SqlCommand(DisableUser.Action, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@UserEmail", userEmail));
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
