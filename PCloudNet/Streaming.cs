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
        /// Get a download link for file 
        /// </summary>
        /// <param name="fileId">ID of the file</param>
        /// <param name="path">Path to the file</param>
        /// <param name="forceDownload">Download with Content-Type = application/octet-stream</param>
        /// <param name="contentType">Set Content-Type</param>
        /// <param name="maxSpeed">Limit the download speed</param>
        /// <param name="skipFileName">Include the name of the file in the generated link</param>
        /// <returns>On success it will return array hosts with servers that have the file. The first server is the one we consider best for current download.</returns>
        public Task<FileLink> GetStreamFileLink(long? fileId = null, string path = null, bool forceDownload = false,
            string contentType = null, int? maxSpeed = null, int? skipFileName = null)
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

            parameters.Add(new KeyValuePair<string, string>("forcedownload", WebUtility.UrlEncode(forceDownload ? "1" : "0")));
            if (!string.IsNullOrEmpty(contentType))
            {
                parameters.Add(new KeyValuePair<string, string>("type", WebUtility.UrlEncode(contentType)));
            }
            if (maxSpeed.HasValue)
            {
                parameters.Add(new KeyValuePair<string, string>("maxspeed", WebUtility.UrlEncode(maxSpeed.Value.ToString())));
            }
            if (skipFileName.HasValue)
            {
                parameters.Add(new KeyValuePair<string, string>("skipfilename", WebUtility.UrlEncode(skipFileName.Value.ToString())));
            }

            var result = ExecuteAsync<FileLink>("getfilelink", parameters);

            return result;
        }

        /// <summary>
        /// Get a streaming link for video file Takes fileid (or path) of a video file and provides links (same way getfilelink does with hosts and path) 
        /// from which the video can be streamed with lower bitrate (and/or resolution).
        /// The transcoded video will be in a FLV container with x264 video and mp3 audio, by default the video bitrate will be adapted 
        /// to the connection speed in real time.
        /// </summary>
        /// <param name="fileId">ID of the file</param>
        /// <param name="path">Path to the file</param>
        /// <param name="forceDownload">Download with Content-Type = application/octet-stream</param>
        /// <param name="contentType">Set Content-Type</param>
        /// <param name="maxSpeed">Limit the download speed</param>
        /// <param name="skipFileName">Include the name of the file in the generated link</param>
        /// <param name="abitrate">audio bit rate in kilobits, from 16 to 320</param>
        /// <param name="vbitrate">video bitrate in kilobits, from 16 to 4000</param>
        /// <param name="resolution">in pixels, from 64x64 to 1280x960, WIDTHxHEIGHT</param>
        /// <param name="fixedbitrate"> if set, turns off adaptive streaming and the stream will be with a constant bitrate.</param>
        /// <returns>By default the content servers will send appropriate content-type for FLV files, 
        /// this can be overridden with either forcedownload or contenttype optional parameters. </returns>
        public Task<FileLink> GetVideoStreamFileLink(long? fileId = null, string path = null, bool forceDownload = false,
            string contentType = null, int? maxSpeed = null, int? skipFileName = null, int abitrate = 128,
            int vbitrate = 1000, string resolution = "1280x960", bool fixedbitrate = false)
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

            parameters.Add(new KeyValuePair<string, string>("forcedownload", WebUtility.UrlEncode(forceDownload ? "1" : "0")));
            if (!string.IsNullOrEmpty(contentType))
            {
                parameters.Add(new KeyValuePair<string, string>("type", WebUtility.UrlEncode(contentType)));
            }
            if (maxSpeed.HasValue)
            {
                parameters.Add(new KeyValuePair<string, string>("maxspeed", WebUtility.UrlEncode(maxSpeed.Value.ToString())));
            }
            if (skipFileName.HasValue)
            {
                parameters.Add(new KeyValuePair<string, string>("skipfilename", WebUtility.UrlEncode(skipFileName.Value.ToString())));
            }

            parameters.Add(new KeyValuePair<string, string>("abitrate", WebUtility.UrlEncode(abitrate.ToString())));
            parameters.Add(new KeyValuePair<string, string>("vbitrate", WebUtility.UrlEncode(vbitrate.ToString())));
            parameters.Add(new KeyValuePair<string, string>("resolution", WebUtility.UrlEncode(resolution)));
            parameters.Add(new KeyValuePair<string, string>("fixedbitrate", WebUtility.UrlEncode(fixedbitrate ? "1" : "0")));

            var result = ExecuteAsync<FileLink>("getvideolink", parameters);

            return result;
        }

        /// <summary>
        /// Get a download link for file 
        /// </summary>
        /// <param name="fileId">ID of the file</param>
        /// <param name="path">Path to the file</param>
        /// <param name="forceDownload">Download with Content-Type = application/octet-stream</param>
        /// <param name="contentType">Set Content-Type</param>
        /// <param name="maxSpeed">Limit the download speed</param>
        /// <param name="skipFileName">Include the name of the file in the generated link</param>
        /// <returns>On success it will return array hosts with servers that have the file. The first server is the one we consider best for current download.</returns>
        public Task<VideoLinks> GetVideoLinks(long? fileId = null, string path = null, bool forceDownload = false,
            string contentType = null, int? maxSpeed = null, int? skipFileName = null)
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

            parameters.Add(new KeyValuePair<string, string>("forcedownload", WebUtility.UrlEncode(forceDownload ? "1" : "0")));
            if (!string.IsNullOrEmpty(contentType))
            {
                parameters.Add(new KeyValuePair<string, string>("type", WebUtility.UrlEncode(contentType)));
            }
            if (maxSpeed.HasValue)
            {
                parameters.Add(new KeyValuePair<string, string>("maxspeed", WebUtility.UrlEncode(maxSpeed.Value.ToString())));
            }
            if (skipFileName.HasValue)
            {
                parameters.Add(new KeyValuePair<string, string>("skipfilename", WebUtility.UrlEncode(skipFileName.Value.ToString())));
            }

            var result = ExecuteAsync<VideoLinks>("getvideolinks", parameters);

            return result;
        }

    }
}
