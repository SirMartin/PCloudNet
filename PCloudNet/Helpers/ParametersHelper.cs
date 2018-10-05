using System;
using System.Collections.Generic;
using System.Net;

namespace PCloudNet.Helpers
{
    public class ParametersHelper
    {
        public static string ConvertParametersToUrl(List<KeyValuePair<string, string>> parameters)
        {
            var url = string.Empty;
            foreach (var parameter in parameters)
            {
                if (!string.IsNullOrEmpty(url))
                {
                    url = url + "&";
                }

                url = url + $"{parameter.Key}={parameter.Value}";
            }

            return url;
        }

        public static List<KeyValuePair<string, string>> CreateParameterListForFolder(string path = null, long? folderId = null)
        {
            if (string.IsNullOrEmpty(path) && (!folderId.HasValue || folderId.Value == 0))
                throw new Exception("Path or folderId are obligatory");

            var parameters = new List<KeyValuePair<string, string>>();

            if (!string.IsNullOrEmpty(path))
            {
                parameters.Add(new KeyValuePair<string, string>("path", WebUtility.UrlEncode(path)));
            }
            else
            {
                parameters.Add(new KeyValuePair<string, string>("folderid", WebUtility.UrlEncode(folderId.ToString())));
            }

            return parameters;
        }

        public static List<KeyValuePair<string, string>> CreateParameterListForFile(string path = null, long? fileId = null)
        {
            if (string.IsNullOrEmpty(path) && (!fileId.HasValue || fileId.Value == 0))
                throw new Exception("Path or fileId are obligatory");

            var parameters = new List<KeyValuePair<string, string>>();

            if (!string.IsNullOrEmpty(path))
            {
                parameters.Add(new KeyValuePair<string, string>("path", WebUtility.UrlEncode(path)));
            }
            else
            {
                parameters.Add(new KeyValuePair<string, string>("fileid", WebUtility.UrlEncode(fileId.ToString())));
            }

            return parameters;
        }
    }
}
