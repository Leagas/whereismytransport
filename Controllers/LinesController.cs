using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace whereismytransport.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class LinesController : InjectedController
	{
		private readonly ILogger<LinesController> _logger;
		private const string URL = "https://platform.whereismytransport.com";
		private string AccessToken;

		public LinesController(ILogger<LinesController> logger, WIMTDataContext context): base(context)
		{
			_logger = logger;
		}

		public async Task<string> FetchToken(string Name)
		{
			var Token = await db.Tokens.FindAsync(1);
			if (Token == null)
			{
				return "";
			}
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
		public async Task<IActionResult> FetchLines()
		{
			// the idea here was to store and check against token expiry date but didn't get that far.
			CheckToken();
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Add("Accept", "application/json");
				client.DefaultRequestHeaders.Add("Authorization", $"Bearer {AccessToken}");
				var response = client.GetStringAsync($"{URL}/api/lines").Result;

				await db.AddRangeAsync(response);
				await db.SaveChangesAsync();
				return Ok(response);
			}
		}
	}
}
