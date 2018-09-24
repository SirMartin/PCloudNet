namespace PCloudNet.Models
{
    public class File
    {
        public string Name { get; set; }
        public string Created { get; set; }
        public bool Thumb { get; set; }
        public string Modified { get; set; }
        public bool IsFolder { get; set; }
        public int Height { get; set; }
        public long FileId { get; set; }
        public int Width { get; set; }
        public int Comments { get; set; }
        public int Category { get; set; }
        public string Id { get; set; }
        public bool IsShared { get; set; }
        public bool IsMine { get; set; }
        public int Size { get; set; }
        public int ParentFolderId { get; set; }
        public string ContentType { get; set; }
        public string Icon { get; set; }
    }
}