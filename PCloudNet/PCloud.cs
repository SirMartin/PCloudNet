using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PCloudNet.Helpers;

namespace PCloudNet
{
    public partial class PCloud
    {
        private string AuthToken { get; set; }
        
        private readonly string _username;
        private readonly string _password;

        private static string BaseUrl => "https://api.pcloud.com";

        private readonly Lazy<WebClient> _lazyClient;

        public PCloud(string username, string password)
        {
            _username = username;
            _password = password;

            _lazyClient = new Lazy<WebClient>(() => new WebClient { Encoding = Encoding.UTF8 });
        }

        protected WebClient Client()
        {
            if (_lazyClient == null)
            {
                throw new ObjectDisposedException("WebClient has been disposed.");
            }

            return _lazyClient.Value;
        }

        protected T Execute<T>(string urlSegment, List<KeyValuePair<string, string>> parameters = null, bool useAuth = true)
        {
            try
            {
                // Add to the parameters the API KEY.
                if (parameters == null)
                {
                    parameters = new List<KeyValuePair<string, string>>();
                }

                if (useAuth)
                {
                    if (string.IsNullOrEmpty(AuthToken))
                    {
                        // Make login.
                        LogIn();
                    }

                    parameters.Add(new KeyValuePair<string, string>("auth", AuthToken));
                }

                // Convert the parameters to URL.
                var parametersUrl = ParametersHelper.ConvertParametersToUrl(parameters);

                var url = $"{BaseUrl}/{urlSegment}?{parametersUrl}";

                var json = Client().DownloadString(url);
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: Fail while executing this urlSegment: " + urlSegment, ex);
                // Logger.Error("Fail while executing this urlSegment: " + urlSegment, ex);
                return default(T);
            }
        }

        protected async Task<T> ExecuteAsync<T>(string urlSegment, List<KeyValuePair<string, string>> parameters = null, bool useAuth = true)
        {
            try
            {
                // Add to the parameters the API KEY.
                if (parameters == null)
                {
                    parameters = new List<KeyValuePair<string, string>>();
                }

                if (useAuth)
                {
                    if (string.IsNullOrEmpty(AuthToken))
                    {
                        // Make login.
                        LogIn();
                    }

                    parameters.Add(new KeyValuePair<string, string>("auth", AuthToken));
                }

                // Convert the parameters to URL.
                var parametersUrl = ParametersHelper.ConvertParametersToUrl(parameters);

                var url = $"{BaseUrl}/{urlSegment}?{parametersUrl}";


                var httpClient = new HttpClient();
                var json = await httpClient.GetStringAsync(url);

                //var json = await PCloud().DownloadStringTaskAsync(url);
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: Fail while executing this urlSegment: " + urlSegment, ex);
                // Logger.Error("Fail while executing this urlSegment: " + urlSegment, ex);
                return default(T);
            }
        }
    }
}
