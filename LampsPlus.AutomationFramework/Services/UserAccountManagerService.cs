using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using Newtonsoft.Json;
using Automation.Framework.Exceptions;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;


namespace LampsPlus.AutomationFramework.Services
{
    public static class UserAccountManagerService 
    {
		private const string Key = "rXL+xSfK5qOpWHruvKo8AKGD/Avk6c5y";
		private const string InitVector = "UG6ggmZXX9g=";

		private static readonly string AccountManagerServiceUri;
        private static readonly Dictionary<string, AccountManagerServiceAccountType> UserAccountTypes = new Dictionary<string, AccountManagerServiceAccountType>();
		private static readonly object UserAccountTypesLock = new object();

        static UserAccountManagerService()
        {
            AccountManagerServiceUri = TestsBase.IsDbClust || TestsBaseRefactored.TestsBase.IsDbClustRefactored ? ConfigurationManager.AppSettings["AccountManagerServiceUri"] : ConfigurationManager.AppSettings["AccountManagerServiceUri.ppe"];
        }

        /// <summary>
        /// Method to allocate user account 
        /// </summary>
        /// <param name="userRoleType">Used to identify user account role.</param>
        public static LampsPlusAccount GetUser(UserRolesTypes userRoleType)
		{
			var accountType = GetAccountType(userRoleType);

			var request = new RequestApi();
            ResponseModel response;

            try
            {
                response = request.GetResponse($"{AccountManagerServiceUri}{accountType}", HttpMethod.Post);
            }
            catch (Exception ex)
            {
              throw new FrameworkApiException($"Uri is: {AccountManagerServiceUri}{accountType}", ex);
            }

            var account = JsonConvert.DeserializeObject<LampsPlusAccount>(response.Content);

			if(!UserAccountTypes.ContainsKey(account.UserName))
			{
				lock(UserAccountTypesLock)
				{
					if(!UserAccountTypes.ContainsKey(account.UserName))
						UserAccountTypes.Add(account.UserName, accountType);
				}
			}

			account.Password = new TripleDesCryptor().Decrypt(account.Password, Key, InitVector);

			return account;
		}

        /// <summary>
        /// Method to get user account 
        /// </summary>
        /// <param name="userRoleType">Used to identify user account role.</param>
		private static AccountManagerServiceAccountType GetAccountType(UserRolesTypes userRoleType)
		{
			switch(userRoleType)
			{
				case UserRolesTypes.Customer:
					return AccountManagerServiceAccountType.Consumer;
				case UserRolesTypes.CustomerServiceRegular:
					return AccountManagerServiceAccountType.Employee;
				case UserRolesTypes.CustomerServiceManager:
					return AccountManagerServiceAccountType.Manager;
				case UserRolesTypes.Professional:
					return AccountManagerServiceAccountType.Professional;
				case UserRolesTypes.Hospitality:
					return AccountManagerServiceAccountType.Hospitality;
				default:
					throw new NotSupportedException($"UserRoleType: {userRoleType.ToString()} is not supported");
			}
		}

        /// <summary>
        /// Method to release user account 
        /// </summary>
        /// <param name="userEmail">Used to identify user account.</param>
		public static void ReleaseUser(string userEmail)
        {
            if (string.IsNullOrWhiteSpace(userEmail)) return;

			if(!UserAccountTypes.ContainsKey(userEmail))
				return;

			var accountType = UserAccountTypes[userEmail];

			new RequestApi().GetResponse($"{AccountManagerServiceUri}{accountType}?userName={userEmail}", HttpMethod.Delete);
		}

        /// <summary>
        /// Method to clear user account assets
        /// </summary>
        /// <param name="userEmail">Used to identify user account.</param>
        public static void ClearUserAssets(string userEmail)
        {
            if (string.IsNullOrWhiteSpace(userEmail)) return;

            if (!UserAccountTypes.ContainsKey(userEmail))
                return;

            var accountType = UserAccountTypes[userEmail];

            new RequestApi().PutRestRequest($"{AccountManagerServiceUri}{accountType}?userName={userEmail}");
        }
    }
}