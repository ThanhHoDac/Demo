namespace eServiceDemo.Models
{
    public class MembershipEligibilityResponse
    {
        public List<MembershipeligibilityResult> MembershipeligibilityResult { get; set; }
            = new List<MembershipeligibilityResult>();
    }

    public class MembershipeligibilityResult
    {
        public string BlacklistReason { get; set; } = string.Empty;
        public bool CheckIneligibleJobFlag { get; set; }
        public bool Checkblist { get; set; }
        public bool Checkclist { get; set; }
        public bool Checkelig { get; set; }
        public string Eligibiity { get; set; } = string.Empty;
        public string IneligibleMessage { get; set; } = string.Empty;
        public string ShowActiveMMessage { get; set; } = string.Empty;
        public string ShowBlacklistMessage { get; set; } = string.Empty;
    }
}
