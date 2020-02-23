using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace whereismytransport.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {

		private readonly string ClientId;
		private readonly string ClientSecret;
        private readonly ILogger<VehicleController> _logger;
		private const string URL = "https://platform.whereismytransport.com";
		private string AccessToken = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjUzM0M3QzJFMDVERjI5RDBBMjA1NEY3NzA4OTM4Njk2NEIzNzc3MTgiLCJ0eXAiOiJKV1QiLCJ4NXQiOiJVeng4TGdYZktkQ2lCVTkzQ0pPR2xrczNkeGcifQ.eyJuYmYiOjE1ODI0NTg5ODYsImV4cCI6MTU4MjQ2MjU4NiwiaXNzIjoiaHR0cHM6Ly9pZGVudGl0eS53aGVyZWlzbXl0cmFuc3BvcnQuY29tIiwiYXVkIjoiaHR0cHM6Ly9pZGVudGl0eS53aGVyZWlzbXl0cmFuc3BvcnQuY29tL3Jlc291cmNlcyIsImNsaWVudF9pZCI6IjdlMzQwNDZhLTc0ZjYtNDhkYi1hMzU4LTJmZGUzNTZmZGU4NSIsImNsaWVudF9jb3ZlcmFnZSI6IjhXWVpORDBtYTBTQXktdk5kYXNybHciLCJjbGllbnRfdGVuYW50IjoiMjQ1NGM4YjItOWY1Yy00Mjg3LTg1YzUtZmNiYjkxMGIxMmNiIiwianRpIjoiOTk0ODc4MWRhMmM4ZTVmNDMyNDFmZGEyNTAxM2M5MTQiLCJzY29wZSI6WyJ0cmFuc2l0YXBpOmFsbCJdfQ.iFoiZfAktfdvvwjMnGE-Pl--R7A-stoVFBbyf0mFeFd-720UFboWzMf5hxZ_P10xP1v9rG6uLZZFFsoqdBCNbWxzGT7ygQqCDt89PHDpe4glcxwlOUoN2k3N2JXpVo8EXvfnChstPAWxrboZCZpgjBxnJWwQ_V02p0zWr93pHSwdP5a2OR5eb7FUtBd0NXCdOWwQnBzxXGoGALT0u5-TV2uGIKU-lWuI1cfBr1-fI_iP7V0uHdJdgW_8ePQZxA0CLZaqLBlOpyzsTmK3E-m-R8fbkn9URFwoRiDrXoJ_B2P8kncZouvtsFg5nT2C9enQuDLFeZGxBBH1ex2mS8DeecLTATCzz1b-5j3uuK3FIb2FszPXRi-IlB_LNVgPoGcwMLHDHrr9k3AEjTDng7c7Z3UOImCKQN78zJfB11z8lOGesEvL1Dma8Eu89lHyu3Q368_zi3eE2968Oyi4DZZGmLNj0aImL6XRoY7DwAnEqQhaYnoUm_XXZjWiCPeILLPw3bNdLT5yezrVugdXvPdVo6v9lK7PF7PCN9FBXgVl-p6WLtSewJ1kzPSBZh2m-15XI-SNSqK7-v6kbFTs670yIUlfWm6H6GsYg7gMqBJ4OcHExc2vCIrizQaxVxp6WEj63fhiqtit34lXODweq9bzDili5WvaZpM8eoJZQrVU8Os";

        public VehicleController(ILogger<VehicleController> logger, Secrets Secrets)
        {
            _logger = logger;
			ClientId = Secrets.clientId;
			ClientSecret = Secrets.clientSecret;
        }

		[HttpGet("/login")]
		public string Login()
		{
			/**
				I included this here to simulate some sort of login/register process with our system, I imagine service providers
				such as MyCiti would go through something like this to access the API to Add/Remove Vehicles etc.
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
				var token = jObject["access_token"].ToString();

				AccessToken = token;
			}

			return "ok";
		}

		[HttpGet("/lines")]
		public string FetchLines()
		{
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
