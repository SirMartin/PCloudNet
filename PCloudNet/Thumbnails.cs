using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using PCloudNet.Models;

namespace PCloudNet
{
    public partial class PCloud
    {
        /// <summary>
        /// Get a link to a thumbnail of a file 
        /// </summary>
        /// <param name="path">path to the folder</param>
        /// <param name="fileId">id of the folder</param>
        /// <param name="width">The width of the thumbnail</param>
        /// <param name="height">The height of the thumbnail</param>
        /// <param name="crop">To make the thumbnail exactly the specified size, so it is croped for the smallets side.</param>
        /// <param name="type">By default is returned on jpeg, but you can specify another type.</param>
        /// <returns></returns>
        public Task<Thumbnail> GetThumbLink(int width, int height, long? fileId = null, string path = null, bool crop = false, string type = null)
        {
            if (string.IsNullOrEmpty(path) && fileId == 0)
                throw new Exception("path or folderId are obligatory");

            var parameters = new List<KeyValuePair<string, string>>();

            if (!string.IsNullOrEmpty(path))
            {
                parameters.Add(new KeyValuePair<string, string>("path", WebUtility.UrlEncode(path)));
            }
            else if (fileId.HasValue)
            {
                parameters.Add(new KeyValuePair<string, string>("fileid", WebUtility.UrlEncode(fileId.Value.ToString())));
            }

            parameters.Add(new KeyValuePair<string, string>("size", WebUtility.UrlEncode($"{width}x{height}")));
            parameters.Add(new KeyValuePair<string, string>("crop", WebUtility.UrlEncode(crop ? "1" : "0")));

            if (!string.IsNullOrEmpty(type))
            {
                parameters.Add(new KeyValuePair<string, string>("type", WebUtility.UrlEncode(type)));
            }

            var result = ExecuteAsync<Thumbnail>("getthumblink", parameters);

            return result;
        }
    }
}
