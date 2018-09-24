using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using PCloudNet.Models;

namespace PCloudNet
{
    public partial class PCloud
    {
        #region LogIn

        private List<KeyValuePair<string, string>> CreateParametersUserInfo(string username, string password, bool getAuth)
        {
            var parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username", WebUtility.UrlEncode(username)),
                new KeyValuePair<string, string>("password", WebUtility.UrlEncode(password)),
                new KeyValuePair<string, string>("getauth", WebUtility.UrlEncode(getAuth ? "1" : "0"))
            };

            return parameters;
        }

        /// <summary>
        /// Returns information about the current user. 
        /// As there is no specific login method as credentials can be passed to any method, 
        /// this is an especially good place for logging in with no particular action in mind.
        /// </summary>
        /// <returns>The user information.</returns>
        private UserInfo LogIn()
        {
            var parameters = CreateParametersUserInfo(_username, _password, true);

            var result = Execute<UserInfo>("userinfo", parameters, false);

            AuthToken = result.Auth;

            return result;
        }

        #endregion

        #region UserInfo

        /// <summary>
        /// Asynchronous Method
        /// Returns information about the current user. 
        /// As there is no specific login method as credentials can be passed to any method, 
        /// this is an especially good place for logging in with no particular action in mind.
        /// </summary>
        /// <returns>The user information.</returns>
        public Task<UserInfo> UserInfoAsync()
        {
            var parameters = CreateParametersUserInfo(_username, _password, true);

            return ExecuteAsync<UserInfo>("userinfo", parameters);
        }

        /// <summary>
        /// Synchronous Method
        /// Returns information about the current user. 
        /// As there is no specific login method as credentials can be passed to any method, 
        /// this is an especially good place for logging in with no particular action in mind.
        /// </summary>
        /// <returns>The user information.</returns>
        public UserInfo UserInfo()
        {
            var parameters = CreateParametersUserInfo(_username, _password, true);

            return Execute<UserInfo>("userinfo", parameters);
        }

        #endregion
    }
}
