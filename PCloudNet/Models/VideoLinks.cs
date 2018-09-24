using System;
using System.Collections.Generic;

namespace PCloudNet.Models
{
    public class VideoLinks
    {
        public int Result { get; set; }
        public List<VideoLinkVariant> Variants { get; set; }
    }

    public class VideoLinkVariant
    {
        public int Width { get; set; }
        public string Path { get; set; }
        public string Fps { get; set; }
        public bool IsOriginal { get; set; }
        public int Height { get; set; }
        public string VideoCodec { get; set; }
        public DateTime Expires { get; set; }
        public int VideoBitrate { get; set; }
        public int AudioBitrate { get; set; }
        public string AudioCodec { get; set; }
        public string Duration { get; set; }
        public string[] Hosts { get; set; }
        public int? Rotate { get; set; }
        public int? AudioSampleRate { get; set; }
    }
}