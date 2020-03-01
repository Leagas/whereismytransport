using System.ComponentModel.DataAnnotations;

namespace whereismytransport.Models
{
	public class Line
	{
		[Required]
        public string id { get; set; }
		[Required]
		public string href { get; set; }
		[Required]
		public string name { get; set; }
		[Required]
		public string shortName { get; set; }
		[Required]
		public string mode { get; set; }
		[Required]
		public string colour { get; set; }
		[Required]
		public string textColour { get; set; }
	}

	/*
		Had to remove Agency as I was unalbe to resolve the following error:
		The instance of entity type 'Agency' cannot be tracked because another instance with the key value
	*/
}
