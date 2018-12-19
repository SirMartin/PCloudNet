using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using PCloudNet.Helpers;
using PCloudNet.Models.PublicLinks;
using PCloudNet.Models.Streaming;
using PCloudNet.Models.Thumbnails;

namespace PCloudNet
{
    public partial class PCloud
    {
        #region GetPublicThumbLink

        private const string GetPublicThumbLinkUrl = "getpubthumblink";

        private List<KeyValuePair<string, string>> CreateParametersGetPublicThumbLink(string path, long? fileId, string code, int width, int height, bool crop = false, string type = null)
        {
            var parameters = ParametersHelper.CreateParameterListForFile(path, fileId);

            parameters.Add(new KeyValuePair<string, string>("code", WebUtility.UrlEncode(code)));
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
        public Task<Thumbnail> GetPublicThumbLinkAsync(string path, string code, int width, int height, bool crop = false, string type = null)
        {
            if (string.IsNullOrEmpty(path))
                throw new Exception("path cannot be empty.");

            if (string.IsNullOrEmpty(code))
                throw new Exception("code cannot be empty.");

            var parameters = CreateParametersGetPublicThumbLink(path, null, code, width, height, crop, type);

            return ExecuteAsync<Thumbnail>(GetPublicThumbLinkUrl, parameters);
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
        public Task<Thumbnail> GetPublicThumbLinkAsync(long fileId, string code, int width, int height, bool crop = false, string type = null)
        {
            if (fileId == 0)
                throw new Exception("fileId has a wrong value.");

            if (string.IsNullOrEmpty(code))
                throw new Exception("code cannot be empty.");

            var parameters = CreateParametersGetPublicThumbLink(null, fileId, code, width, height, crop, type);

            return ExecuteAsync<Thumbnail>(GetPublicThumbLinkUrl, parameters);
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
        public Thumbnail GetPublicThumbLink(string path, string code, int width, int height, bool crop = false, string type = null)
        {
            if (string.IsNullOrEmpty(path))
                throw new Exception("path cannot be empty.");

            if (string.IsNullOrEmpty(code))
                throw new Exception("code cannot be empty.");

            var parameters = CreateParametersGetPublicThumbLink(path, null, code, width, height, crop, type);

            return Execute<Thumbnail>(GetPublicThumbLinkUrl, parameters);
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
        public Thumbnail GetPublicThumbLink(long fileId, string code, int width, int height, bool crop = false, string type = null)
        {
            if (fileId == 0)
                throw new Exception("fileId has a wrong value.");

            if (string.IsNullOrEmpty(code))
                throw new Exception("code cannot be empty.");

            var parameters = CreateParametersGetPublicThumbLink(null, fileId, code, width, height, crop, type);

            return Execute<Thumbnail>(GetPublicThumbLinkUrl, parameters);
        }

        #endregion

        #region Get Public File Link

        private const string GetPublicFileLinkUrl = "getfilepublink";

        private List<KeyValuePair<string, string>> CreateParametersGetPublicFileLink(string path = null, long? fileId = null, DateTime? expire = null,
            int? maxDownloads = null, int? maxTraffic = null, bool shortLink = false)
        {
            var parameters = ParametersHelper.CreateParameterListForFile(path, fileId);

            if (expire.HasValue)
            {
                parameters.Add(new KeyValuePair<string, string>("expire", WebUtility.UrlEncode(expire.Value.ToString("o"))));
            }
            if (maxDownloads.HasValue)
            {
                parameters.Add(new KeyValuePair<string, string>("maxdownloads", WebUtility.UrlEncode(maxDownloads.Value.ToString())));
            }
            if (maxTraffic.HasValue)
            {
                parameters.Add(new KeyValuePair<string, string>("maxtraffic", WebUtility.UrlEncode(maxTraffic.Value.ToString())));
            }

            parameters.Add(new KeyValuePair<string, string>("shortlink", WebUtility.UrlEncode(shortLink ? "1" : "0")));

            return parameters;
        }

        /// <summary>
        /// Asynchronous Method
        /// Creates and return a public link to a file. 
        /// </summary>
        /// <param name="fileId">ID of the file</param>
        /// <param name="expire">Datetime when the link will stop working</param>
        /// <param name="maxDownloads">Maximum number of downloads for this file</param>
        /// <param name="maxTraffic">Maximum traffic that this link will consume (in bytes, started downloads will not be cut to fit in this limit)</param>
        /// <param name="shortLink">If set, a short link will also be generated</param>
        /// <returns>On success it will return array hosts with servers that have the file. The first server is the one we consider best for current download.</returns>
        public Task<PublicFileLink> GetPublicFileLinkAsync(long fileId, DateTime? expire = null, int? maxDownloads = null, int? maxTraffic = null, bool shortLink = false)
        {
            if (fileId == 0)
                throw new Exception("fileId has a wrong value.");

            var parameters = CreateParametersGetPublicFileLink(null, fileId, expire, maxDownloads, maxTraffic, shortLink);

            return ExecuteAsync<PublicFileLink>(GetPublicFileLinkUrl, parameters);
        }

        /// <summary>
        /// Asynchronous Method
        /// Creates and return a public link to a file. 
        /// </summary>
        /// <param name="path">Path to the file</param>
        /// <param name="expire">Datetime when the link will stop working</param>
        /// <param name="maxDownloads">Maximum number of downloads for this file</param>
        /// <param name="maxTraffic">Maximum traffic that this link will consume (in bytes, started downloads will not be cut to fit in this limit)</param>
        /// <param name="shortLink">If set, a short link will also be generated</param>
        /// <returns>On success it will return array hosts with servers that have the file. The first server is the one we consider best for current download.</returns>
        public Task<PublicFileLink> GetPublicFileLinkAsync(string path, DateTime? expire = null, int? maxDownloads = null, int? maxTraffic = null, bool shortLink = false)
        {
            if (string.IsNullOrEmpty(path))
                throw new Exception("path cannot be empty.");

            var parameters = CreateParametersGetPublicFileLink(path, null, expire, maxDownloads, maxTraffic, shortLink);

            return ExecuteAsync<PublicFileLink>(GetPublicFileLinkUrl, parameters);
        }

        /// <summary>
        /// Synchronous Method
        /// Creates and return a public link to a file. 
        /// </summary>
        /// <param name="fileId">ID of the file</param>
        /// <param name="expire">Datetime when the link will stop working</param>
        /// <param name="maxDownloads">Maximum number of downloads for this file</param>
        /// <param name="maxTraffic">Maximum traffic that this link will consume (in bytes, started downloads will not be cut to fit in this limit)</param>
        /// <param name="shortLink">If set, a short link will also be generated</param>
        /// <returns>On success it will return array hosts with servers that have the file. The first server is the one we consider best for current download.</returns>
        public PublicFileLink GetPublicFileLink(long fileId, DateTime? expire = null, int? maxDownloads = null, int? maxTraffic = null, bool shortLink = false)
        {
            if (fileId == 0)
                throw new Exception("fileId has a wrong value.");

            var parameters = CreateParametersGetPublicFileLink(null, fileId, expire, maxDownloads, maxTraffic, shortLink);

            return Execute<PublicFileLink>(GetPublicFileLinkUrl, parameters);
        }

        /// <summary>
        /// Synchronous Method
        /// Creates and return a public link to a file. 
        /// </summary>
        /// <param name="path">Path to the file</param>
        /// <param name="expire">Datetime when the link will stop working</param>
        /// <param name="maxDownloads">Maximum number of downloads for this file</param>
        /// <param name="maxTraffic">Maximum traffic that this link will consume (in bytes, started downloads will not be cut to fit in this limit)</param>
        /// <param name="shortLink">If set, a short link will also be generated</param>
        /// <returns>On success it will return array hosts with servers that have the file. The first server is the one we consider best for current download.</returns>
        public PublicFileLink GetPublicFileLink(string path, DateTime? expire = null, int? maxDownloads = null, int? maxTraffic = null, bool shortLink = false)
        {
            if (string.IsNullOrEmpty(path))
                throw new Exception("path cannot be empty.");

            var parameters = CreateParametersGetPublicFileLink(path, null, expire, maxDownloads, maxTraffic, shortLink);

            return Execute<PublicFileLink>(GetPublicFileLinkUrl, parameters);
        }

        #endregion

        #region Get Public Folder Link

        private const string GetPublicFolderLinkUrl = "getfolderpublink";

        private List<KeyValuePair<string, string>> CreateParametersGetPublicFolderLink(string path = null, long? folderId = null, DateTime? expire = null,
            int? maxDownloads = null, int? maxTraffic = null, bool shortLink = false)
        {
            var parameters = ParametersHelper.CreateParameterListForFolder(path, folderId);

            if (expire.HasValue)
            {
                parameters.Add(new KeyValuePair<string, string>("expire", WebUtility.UrlEncode(expire.Value.ToString("o"))));
            }
            if (maxDownloads.HasValue)
            {
                parameters.Add(new KeyValuePair<string, string>("maxdownloads", WebUtility.UrlEncode(maxDownloads.Value.ToString())));
            }
            if (maxTraffic.HasValue)
            {
                parameters.Add(new KeyValuePair<string, string>("maxtraffic", WebUtility.UrlEncode(maxTraffic.Value.ToString())));
            }

            parameters.Add(new KeyValuePair<string, string>("shortlink", WebUtility.UrlEncode(shortLink ? "1" : "0")));

            return parameters;
        }

        /// <summary>
        /// Asynchronous Method
        /// Creates and return a public link to a file. 
        /// </summary>
        /// <param name="folderId">ID of the folder</param>
        /// <param name="expire">Datetime when the link will stop working</param>
        /// <param name="maxDownloads">Maximum number of downloads for this file</param>
        /// <param name="maxTraffic">Maximum traffic that this link will consume (in bytes, started downloads will not be cut to fit in this limit)</param>
        /// <param name="shortLink">If set, a short link will also be generated</param>
        /// <returns>On success it will return array hosts with servers that have the file. The first server is the one we consider best for current download.</returns>
        public Task<PublicFileLink> GetPublicFolderLinkAsync(long folderId, DateTime? expire = null, int? maxDownloads = null, int? maxTraffic = null, bool shortLink = false)
        {
            if (folderId == 0)
                throw new Exception("folderId has a wrong value.");

            var parameters = CreateParametersGetPublicFolderLink(null, folderId, expire, maxDownloads, maxTraffic, shortLink);

            return ExecuteAsync<PublicFileLink>(GetPublicFolderLinkUrl, parameters);
        }

        /// <summary>
        /// Asynchronous Method
        /// Creates and return a public link to a file. 
        /// </summary>
        /// <param name="path">Path to the file</param>
        /// <param name="expire">Datetime when the link will stop working</param>
        /// <param name="maxDownloads">Maximum number of downloads for this file</param>
        /// <param name="maxTraffic">Maximum traffic that this link will consume (in bytes, started downloads will not be cut to fit in this limit)</param>
        /// <param name="shortLink">If set, a short link will also be generated</param>
        /// <returns>On success it will return array hosts with servers that have the file. The first server is the one we consider best for current download.</returns>
        public Task<PublicFileLink> GetPublicFolderLinkAsync(string path, DateTime? expire = null, int? maxDownloads = null, int? maxTraffic = null, bool shortLink = false)
        {
            if (string.IsNullOrEmpty(path))
                throw new Exception("path cannot be empty.");

            var parameters = CreateParametersGetPublicFolderLink(path, null, expire, maxDownloads, maxTraffic, shortLink);

            return ExecuteAsync<PublicFileLink>(GetPublicFolderLinkUrl, parameters);
        }

        /// <summary>
        /// Synchronous Method
        /// Creates and return a public link to a file. 
        /// </summary>
        /// <param name="folderId">ID of the folder</param>
        /// <param name="expire">Datetime when the link will stop working</param>
        /// <param name="maxDownloads">Maximum number of downloads for this file</param>
        /// <param name="maxTraffic">Maximum traffic that this link will consume (in bytes, started downloads will not be cut to fit in this limit)</param>
        /// <param name="shortLink">If set, a short link will also be generated</param>
        /// <returns>On success it will return array hosts with servers that have the file. The first server is the one we consider best for current download.</returns>
        public PublicFileLink GetPublicFolderLink(long folderId, DateTime? expire = null, int? maxDownloads = null, int? maxTraffic = null, bool shortLink = false)
        {
            if (folderId == 0)
                throw new Exception("folderId has a wrong value.");

            var parameters = CreateParametersGetPublicFolderLink(null, folderId, expire, maxDownloads, maxTraffic, shortLink);

            return Execute<PublicFileLink>(GetPublicFolderLinkUrl, parameters);
        }

        /// <summary>
        /// Synchronous Method
        /// Creates and return a public link to a file. 
        /// </summary>
        /// <param name="path">Path to the file</param>
        /// <param name="expire">Datetime when the link will stop working</param>
        /// <param name="maxDownloads">Maximum number of downloads for this file</param>
        /// <param name="maxTraffic">Maximum traffic that this link will consume (in bytes, started downloads will not be cut to fit in this limit)</param>
        /// <param name="shortLink">If set, a short link will also be generated</param>
        /// <returns>On success it will return array hosts with servers that have the file. The first server is the one we consider best for current download.</returns>
        public PublicFileLink GetPublicFolderLink(string path, DateTime? expire = null, int? maxDownloads = null, int? maxTraffic = null, bool shortLink = false)
        {
            if (string.IsNullOrEmpty(path))
                throw new Exception("path cannot be empty.");

            var parameters = CreateParametersGetPublicFolderLink(path, null, expire, maxDownloads, maxTraffic, shortLink);

            return Execute<PublicFileLink>(GetPublicFolderLinkUrl, parameters);
        }

        #endregion
    }
}
