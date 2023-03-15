namespace eServiceDemo.Configurations
{
    public class NtucApimConfiguration
    {
        public ClientOptions ClientOptions { get; set; }
        public string MembershipEligibility { get; set; }
    }

    public class ClientOptions
    {
        public string BaseAddress { get; set; }
        public string RequestContentType { get; set; }
        public Dictionary<string, string> DefaultRequestHeaders { get; set; }
    }
}
