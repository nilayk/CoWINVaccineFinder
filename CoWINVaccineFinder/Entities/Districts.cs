using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoWINVaccineFinder.Entities
{
	public class Districts
	{
		[JsonProperty("districts")]
		public IList<District> DistrictList { get; set; }

		[JsonProperty("ttl")]
		public int TTL { get; set; }
	}
}
