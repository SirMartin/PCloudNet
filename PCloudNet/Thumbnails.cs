using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using PCloudNet.Helpers;
using PCloudNet.Models.Thumbnails;

namespace PCloudNet
{
    public partial class PCloud
    {
        #region GetThumbLink

        private const string GetThumbLinkUrl = "getthumblink";

        private List<KeyValuePair<string, string>> CreateParametersGetThumbLink(string path, long? fileId, int width, int height, bool crop = false, string type = null)
        {
            var parameters = ParametersHelper.CreateParameterListForFile(path, fileId);
            
            parameters.Add(new KeyValuePair<string, string>("size", WebUtility.UrlEncode($"{width}x{height}")));
            parameters.Add(new KeyValuePair<string, string>("crop", WebUtility.UrlEncode(crop ? "1" : "0")));

            if (!string.IsNullOrEmpty(type))
            {
                parameters.Add(new KeyValuePair<string, string>("type", WebUtility.UrlEncode(type)));
            }

            return parameters;
        }

        /// <summary>
        /// Asynchronous Method
        /// Get a link to a thumbnail of a file 
        /// </summary>
        /// <param name="path">path to the folder</param>
        /// <param name="width">The width of the thumbnail</param>
        /// <param name="height">The height of the thumbnail</param>
        /// <param name="crop">To make the thumbnail exactly the specified size, so it is croped for the smallets side.</param>
        /// <param name="type">By default is returned on jpeg, but you can specify another type.</param>
        /// <returns></returns>
        public Task<Thumbnail> GetThumbLinkAsync(string path, int width, int height, bool crop = false, string type = null)
        {
            if (string.IsNullOrEmpty(path))
                throw new Exception("path cannot be empty.");

            var parameters = CreateParametersGetThumbLink(path, null, width, height, crop, type);

            return ExecuteAsync<Thumbnail>(GetThumbLinkUrl, parameters);
        }

        /// <summary>
        /// Asynchronous Method
        /// Get a link to a thumbnail of a file 
        /// </summary>
        /// <param name="fileId">id of the folder</param>
        /// <param name="width">The width of the thumbnail</param>
        /// <param name="height">The height of the thumbnail</param>
        /// <param name="crop">To make the thumbnail exactly the specified size, so it is croped for the smallets side.</param>
        /// <param name="type">By default is returned on jpeg, but you can specify another type.</param>
        /// <returns></returns>
        public Task<Thumbnail> GetThumbLinkAsync(long? fileId, int width, int height, bool crop = false, string type = null)
        {
            if (fileId == 0)
                throw new Exception("fileId has a wrong value.");

            var parameters = CreateParametersGetThumbLink(null, fileId, width, height, crop, type);

            return ExecuteAsync<Thumbnail>(GetThumbLinkUrl, parameters);
        }

        /// <summary>
        /// Synchronous Method
        /// Get a link to a thumbnail of a file 
        /// </summary>
        /// <param name="path">path to the folder</param>
        /// <param name="width">The width of the thumbnail</param>
        /// <param name="height">The height of the thumbnail</param>
        /// <param name="crop">To make the thumbnail exactly the specified size, so it is croped for the smallets side.</param>
        /// <param name="type">By default is returned on jpeg, but you can specify another type.</param>
        /// <returns></returns>
        public Thumbnail GetThumbLink(string path, int width, int height, bool crop = false, string type = null)
        {
            if (string.IsNullOrEmpty(path))
                throw new Exception("path cannot be empty.");

            var parameters = CreateParametersGetThumbLink(path, null, width, height, crop, type);

            return Execute<Thumbnail>(GetThumbLinkUrl, parameters);
        }

        /// <summary>
        /// Synchronous Method
        /// Get a link to a thumbnail of a file 
        /// </summary>
        /// <param name="fileId">id of the folder</param>
        /// <param name="width">The width of the thumbnail</param>
        /// <param name="height">The height of the thumbnail</param>
        /// <param name="crop">To make the thumbnail exactly the specified size, so it is croped for the smallets side.</param>
        /// <param name="type">By default is returned on jpeg, but you can specify another type.</param>
        /// <returns></returns>
        public Thumbnail GetThumbLink(long? fileId, int width, int height, bool crop = false, string type = null)
        {
            if (fileId == 0)
                throw new Exception("fileId has a wrong value.");

            var parameters = CreateParametersGetThumbLink(null, fileId, width, height, crop, type);

            return Execute<Thumbnail>(GetThumbLinkUrl, parameters);
        }

        #endregion
    }
}
