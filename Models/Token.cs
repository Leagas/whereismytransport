
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace whereismytransport
{
	public class Token
	{
		[Required]
        public int ID { get; set; }
		[Required]
		public string Name { get; set; } = "MyCiti";
		[Required]
		public string access_token { get; set; }
		[Required]
		public string expires_in { get; set; }
	}

}
