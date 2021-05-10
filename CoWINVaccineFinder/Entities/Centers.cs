using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoWINVaccineFinder.Entities
{
	public class Centers
	{
		[JsonProperty("centers")]
		public IList<Center> CenterList { get; set; }
	}
}
