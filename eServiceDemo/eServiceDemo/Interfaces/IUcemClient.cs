using eServiceDemo.Models;

namespace eServiceDemo.Interfaces
{
    public interface IUcemClient
    {
        MembershipEligibilityResponse MembershipEligibility(string uin, string queryParams);
    }
}
