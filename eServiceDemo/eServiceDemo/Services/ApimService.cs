using eServiceDemo.Interfaces;
using eServiceDemo.Models;
using NTUC.Web.APIs.Interfaces;

namespace NTUC.Web.APIs.Services
{
    public class ApimService : IApimService
    {
        private readonly IUcemClient ucemClient;

        public ApimService(IUcemClient ucemClient)
        {
            this.ucemClient = ucemClient;
        }

        public MembershipEligibilityResponse MembershipEligibilityResponse()
        {
            var membershipEligibilityResponse = this.ucemClient.MembershipEligibility("S6005043J", "occupationGroupCode=FL02&companycode=224566");

            return membershipEligibilityResponse;

        }

    }
}
