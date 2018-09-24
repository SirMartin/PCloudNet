using System.Collections.Generic;
using System.Net;
using PCloudNet.Models;

namespace PCloudNet
{
    public partial class PCloud
    {
        /// <summary>
        /// Returns information about the current user. 
        /// As there is no specific login method as credentials can be passed to any method, 
        /// this is an especially good place for logging in with no particular action in mind.
        /// </summary>
        /// <returns>The user information.</returns>
        public UserInfo GetUserInfo()
        {
            var parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username", WebUtility.UrlEncode(_username)),
                new KeyValuePair<string, string>("password", WebUtility.UrlEncode(_password)),
                new KeyValuePair<string, string>("getauth", WebUtility.UrlEncode("1"))
            };

            var result = Execute<UserInfo>("userinfo", parameters, false);

            AuthToken = result.Auth;

            return result;
        }
    }
}
