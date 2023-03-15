using eServiceDemo.Configurations;
using eServiceDemo.Helpers;
using eServiceDemo.Interfaces;
using eServiceDemo.Models;
using Microsoft.Extensions.Options;

namespace eServiceDemo.Clients
{
    public class UcemClient : ApimClient, IUcemClient
    {
        private readonly NtucApimConfiguration config;
        public UcemClient(IOptions<NtucApimConfiguration> config) : base(config.Value.ClientOptions)
        {
            this.config = config.Value;
        }

        public MembershipEligibilityResponse MembershipEligibility(string uin, string queryParams)
        {
            MembershipEligibilityResponse response = new MembershipEligibilityResponse();
            var path = config.ClientOptions.BaseAddress + $"{config.MembershipEligibility}{uin}?{queryParams}";
            response = Send<MembershipEligibilityResponse>(path, HttpMethod.Get);
            return response;
        }
    }
}
