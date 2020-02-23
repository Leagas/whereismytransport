using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace whereismytransport.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {

        private readonly ILogger<VehicleController> _logger;

        public VehicleController(ILogger<VehicleController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            Console.WriteLine("System check...");
			return "ok";
        }

		[HttpPost]
		public string Post([FromBody] Request body)
		{
			Console.WriteLine("System check...");
			return body.type;
		}

		[HttpPut]
		public string Put([FromBody] Request body)
		{
			Console.WriteLine("System check...");
			return body.type;
		}

		[HttpDelete]
		public string Delete()
		{
			Console.WriteLine("System check...");
			return "ok";
		}
    }
}
