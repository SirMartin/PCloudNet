﻿using System;

namespace PCloudNet.Models.Streaming
{
    public class FileLink
    {
        public int Result { get; set; }
        public string Path { get; set; }
        public DateTime Expires { get; set; }
        public string[] Hosts { get; set; }
    }
}