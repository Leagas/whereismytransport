using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;

namespace whereismytransport.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {

        private readonly ILogger<VehicleController> _logger;
		private readonly IConfiguration _configuration;
		private const string URL = "https://sub.domain.com/objects.json";
		private readonly string accessToken = "";

        public VehicleController(ILogger<VehicleController> logger, IConfiguration Configuration)
        {
            _logger = logger;
			_configuration = Configuration;
        }

		[HttpGet("/lines")]
		public void FetchLines()
		{
			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri(URL);

			client.DefaultRequestHeaders.Add("Accept", "application/json");
			client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
			var response = client.GetStringAsync("https://platform.whereismytransport.com/api/agencies").Result;
		}

        [HttpGet]
        public string Get()
        {
            Console.WriteLine("System check...");
			return "ok";
        }

		[HttpPost]
		public string Post([FromBody] Payload body)
		{
			Console.WriteLine("System check...");
			return body.type;
		}

		[HttpPut]
		public string Put([FromBody] Payload body)
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
