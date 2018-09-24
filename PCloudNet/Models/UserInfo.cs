using System;

namespace PCloudNet.Models
{
    public class UserInfo : PCloudAuth
    {
        public string Email { get; set; }
        public bool EmailVerified { get; set; }
        public DateTime Registered { get; set; }
        public bool Premium { get; set; }
        public DateTime PremiumExpires { get; set; }
        public double Quota { get; set; }
        public double UsedQuota { get; set; }
        public string Language { get; set; }
        public int UserId { get; set; }
    }

    public abstract class PCloudAuth
    {
        public string Auth { get; set; }
    }
}