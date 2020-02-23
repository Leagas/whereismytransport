using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace whereismytransport.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ClientController : InjectedController
	{
		private readonly string ClientId;
		private readonly string ClientSecret;
		private readonly ILogger<ClientController> _logger;
		private const string URL = "https://identity.whereismytransport.com";

		public ClientController(ILogger<ClientController> logger, Secrets Secrets, WIMTDataContext context): base(context)
		{
			_logger = logger;
			ClientId = Secrets.clientId;
			ClientSecret = Secrets.clientSecret;
		}

		[HttpGet("/login")]
		public async Task<IActionResult> Login()
		{
			/**
				I included this here to simulate some sort of login/register process with our system, I imagine service providers/transit operators
				such as MyCiti dispatch/Driver would go through something like this to access the API to Add/Remove/CheckStatus against Vehicles etc.
			*/

			var payload = new Dictionary<string, string>
			{
				{ "client_id",     ClientId },
				{ "client_secret", ClientSecret },
				{ "grant_type",    "client_credentials" },
				{ "scope",         "transportapi:all" }
			};

			var formData = new FormUrlEncodedContent(payload);

			using(var client = new HttpClient())
			{
				var response = client.PostAsync($"{URL}/connect/token", formData).Result;
				string json = response.Content.ReadAsStringAsync().Result;
				var jObject = JObject.Parse(json);

				var Token = new Token();
				Token.access_token = jObject["access_token"].ToString();
				Token.expires_in = jObject["expires_in"].ToString();

				await db.AddAsync(Token);
				await db.SaveChangesAsync();
				return Ok(Token);
			}
		}
	}
}
