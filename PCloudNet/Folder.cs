using System;
using System.Collections.Generic;
using System.Net;
using PCloudNet.Models;

namespace PCloudNet
{
    public partial class PCloud
    {
        /// <summary>
        /// Receive data for a folder.
        /// Expects folderid or path parameter, returns folder's metadata. The metadata will have contents field that is array of metadatas of folder's contents.
        /// Recursively listing the root folder is not an expensive operation.
        /// </summary>
        /// <param name="path">path to the folder(discouraged)</param>
        /// <param name="folderId">id of the folder</param>
        /// <param name="recursive">If is set full directory tree will be returned, which means that all directories will have contents filed.</param>
        /// <param name="showDeleted">If is set, deleted files and folders that can be undeleted will be displayed.</param>
        /// <param name="noFiles">If is set, only the folder (sub)structure will be returned.</param>
        /// <param name="noShares">If is set, only user's own folders and files will be displayed.</param>
        /// <returns>The content of the list folders and files.</returns>
        public ListFolder ListFolder(string path, long? folderId = null, bool recursive = false,
            bool showDeleted = false, bool noFiles = false, bool noShares = false)
        {
            if (string.IsNullOrEmpty(path) && folderId == 0)
                throw new Exception("path or folderId are obligatory");

            var parameters = new List<KeyValuePair<string, string>>();

            if (!string.IsNullOrEmpty(path))
            {
                parameters.Add(new KeyValuePair<string, string>("path", WebUtility.UrlEncode(path)));
            }
            else if (folderId.HasValue)
            {
                parameters.Add(new KeyValuePair<string, string>("folderid", WebUtility.UrlEncode(folderId.Value.ToString())));
            }

            parameters.Add(new KeyValuePair<string, string>("recursive", WebUtility.UrlEncode(recursive ? "1" : "0")));
            parameters.Add(new KeyValuePair<string, string>("showdeleted", WebUtility.UrlEncode(showDeleted ? "1" : "0")));
            parameters.Add(new KeyValuePair<string, string>("nofiles", WebUtility.UrlEncode(noFiles ? "1" : "0")));
            parameters.Add(new KeyValuePair<string, string>("noshares", WebUtility.UrlEncode(noShares ? "1" : "0")));

            return Execute<ListFolder>("listfolder", parameters);
        }

    }
}
