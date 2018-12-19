using System.Collections.Generic;
using System.Numerics;

namespace PCloudNet.Models.Folder
{
    public class ListFolder
    {
        public int Result { get; set; }
        public Metadata Metadata { get; set; }
    }

    public class Content
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public string Created { get; set; }
        public bool IsMine { get; set; }
        public bool Thumb { get; set; }
        public string Modified { get; set; }
        public int Comments { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public string Id { get; set; }
        public bool IsShared { get; set; }
        public string Icon { get; set; }
        public bool IsFolder { get; set; }
        public long ParentFolderId { get; set; }
        public long FolderId { get; set; }
        public long? FileId { get; set; }
        public BigInteger? Hash { get; set; }
        public int? Category { get; set; }
        public int? Size { get; set; }
        public string ContentType { get; set; }
        public List<Content> Contents { get; set; }
    }

    public class Metadata
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public string Created { get; set; }
        public bool IsMine { get; set; }
        public bool Thumb { get; set; }
        public string Modified { get; set; }
        public string Id { get; set; }
        public bool IsShared { get; set; }
        public string Icon { get; set; }
        public bool IsFolder { get; set; }
        public long FolderId { get; set; }
        public List<Content> Contents { get; set; }
    }
}