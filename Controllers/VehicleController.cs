using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace whereismytransport.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : InjectedController
    {
        private readonly ILogger<VehicleController> _logger;

        public VehicleController(ILogger<VehicleController> logger, WIMTDataContext context): base(context)
        {
            _logger = logger;
        }
    }
}
