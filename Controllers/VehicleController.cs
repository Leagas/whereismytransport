using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System;

namespace whereismytransport.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : InjectedController
    {
        private readonly ILogger<VehicleController> _logger;
		private const string URL = "https://platform.whereismytransport.com";
		private string AccessToken;

        public VehicleController(ILogger<VehicleController> logger, TokenContext context): base(context)
        {
            _logger = logger;
        }

		public async Task<string> FetchToken(string Name)
		{
			var Token = await db.Tokens.FindAsync(1);
			return Token.access_token.ToString();
		}

		public void CheckToken()
		{
			if (AccessToken == null)
			{
				AccessToken = FetchToken("MyCiti").Result;
			}
		}

		[HttpGet("/lines")]
		public string FetchLines()
		{

			CheckToken();
			Console.WriteLine(AccessToken);
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Add("Accept", "application/json");
				client.DefaultRequestHeaders.Add("Authorization", $"Bearer {AccessToken}");
				var response = client.GetStringAsync($"{URL}/api/lines").Result;

				return response;
			}
		}
    }
}
