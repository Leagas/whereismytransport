using System.ComponentModel.DataAnnotations;

namespace whereismytransport
{
	public class Line
	{
		[Required]
        public int ID { get; set; }
		[Required]
		public string href { get; set; }
		[Required]
		public Agency agency { get; set; }
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

	public class Agency
	{
		public string id {  get; set; }
		public string href { get; set; }
		public string name { get; set; }
		public string culture { get; set; }
	}
}
