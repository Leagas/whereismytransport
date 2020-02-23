using Microsoft.Extensions.Configuration;

namespace whereismytransport
{
	public class Secrets
	{
		/**
			Not sure if this is neccessary or standardardly used as the docs do mention
			that the secrets store is not secure and for use in development only (so should be fine here).
		*/
		public Secrets(IConfiguration Configuration)
		{
			clientId = Configuration["clientId"];
			clientSecret = Configuration["clientSecret"];
		}

		public string clientId { get; set; }
		public string clientSecret { get; set; }
	}
}
