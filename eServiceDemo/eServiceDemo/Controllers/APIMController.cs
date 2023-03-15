using Microsoft.AspNetCore.Mvc;
using NTUC.Web.APIs.Interfaces;

namespace eServiceDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class APIMController : ControllerBase
    {
        private readonly IApimService apimService;

        public APIMController(IApimService apimService)
        {
            this.apimService = apimService;
        }

        [HttpGet("MembershipEligibility")]
        public IActionResult MembershipEligibility()
        {
            var result = apimService.MembershipEligibilityResponse();

            return Ok(result);
        }
    }
}
