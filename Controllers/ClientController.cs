using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using whereismytransport.Models;

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

			if (ClientId == null)
			{
				throw new HttpRequestException("Invalid credentials");
			};

			if (ClientSecret == null)
			{
				throw new HttpRequestException("Invalid credentials");
			}

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
				// var response = client.PostAsync($"{URL}/connect/token", formData).Result;
				// string json = response.Content.ReadAsStringAsync().Result;
				// var jObject = JObject.Parse(json);

				var Token = new Token();
				Token.access_token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjUzM0M3QzJFMDVERjI5RDBBMjA1NEY3NzA4OTM4Njk2NEIzNzc3MTgiLCJ0eXAiOiJKV1QiLCJ4NXQiOiJVeng4TGdYZktkQ2lCVTkzQ0pPR2xrczNkeGcifQ.eyJuYmYiOjE1ODMwNzg0MzYsImV4cCI6MTU4MzA4MjAzNiwiaXNzIjoiaHR0cHM6Ly9pZGVudGl0eS53aGVyZWlzbXl0cmFuc3BvcnQuY29tIiwiYXVkIjoiaHR0cHM6Ly9pZGVudGl0eS53aGVyZWlzbXl0cmFuc3BvcnQuY29tL3Jlc291cmNlcyIsImNsaWVudF9pZCI6IjdlMzQwNDZhLTc0ZjYtNDhkYi1hMzU4LTJmZGUzNTZmZGU4NSIsImNsaWVudF9jb3ZlcmFnZSI6IjhXWVpORDBtYTBTQXktdk5kYXNybHciLCJjbGllbnRfdGVuYW50IjoiMjQ1NGM4YjItOWY1Yy00Mjg3LTg1YzUtZmNiYjkxMGIxMmNiIiwianRpIjoiNWZlNDM0ZDk0YmE2OGUyM2UxMmIwZjlkZDU1NDEzODMiLCJzY29wZSI6WyJ0cmFuc3BvcnRhcGk6YWxsIl19.O8eUYzHxTCaozwmEsVoNkGA5qqf_MIN7t2y4PaXORbrQJBmW0da0qg4S73oSib3EaM2lrv_08iMH3J3o1qWj1LkSaKpgKRkDgVAWXzhztIrfkXQGgls85gKfAjqsNup_Co6bHBWNzHhrCUXz4o1apSRO0yIaIzks4dZ3ogX0_Q2f3e3-y3zNFkjj_nGHqYx9jeymIqjQ55BQr1nOCXd-nvBey7NGwaEqM4iU3kcDhkIsu6bJlQrxmLtfxSCmJ_h1qvKXxpNwLpFcuciDm5exhxcnj5og6aAfR0zPuhQaS8jGTGbl2jsrmZun5kWjVM7NNzp93XXM6EdOBTEa61bG0sOG7c2V-EZPz9Gcumi-vrjGrZxIR4_owgYYNdBexJzpmFSkd_m_UHt9brsQAKIG_6tMh-X5qFgatw2ejHfNKT2oUh8Pu2gHmJtxXeJQ3odhSvNhBbR1nsXuNJxdz1cfhK4hTFNOo0A1HK1VIwHJm6ePdm8AJr1RFlsZ7GS2S_EM3b3YHTa-NafyfZWL04JAVyQkwahVOUbZV2aJ3ySuSyDQEHqkP-7-JAig0csr-tDfrcR8vBI1FQeC9eHxPTlikNBZH8Ltkkanp1nleH7mDXwQFF7Yxw-dVlJGnhK3YzDmN6jv-C-3F2XioUVjKnTqtzH4WnwzrUZLACqbCPn37QI";
				Token.expires_in = "3600";

				// Token.access_token = jObject["access_token"].ToString();
				// Token.expires_in = jObject["expires_in"].ToString();

				await db.Tokens.AddAsync(Token);
				await db.SaveChangesAsync();
				return Ok(Token);
			}
		}
	}
}
