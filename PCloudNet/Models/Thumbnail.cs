using System;
using System.Linq;

namespace PCloudNet.Models
{
    public class Thumbnail
    {
        public int Result { get; set; }
        public string Size { get; set; }
        public int Width => !string.IsNullOrEmpty(Size) ? int.Parse(Size.ToLower().Split('x').First()) : 0;
        public int Height => !string.IsNullOrEmpty(Size) ? int.Parse(Size.ToLower().Split('x').Last()) : 0;
        public string Path { get; set; }
        public DateTime Expires { get; set; }
        public string[] Hosts { get; set; }
    }
}