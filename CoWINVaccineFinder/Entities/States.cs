using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoWINVaccineFinder.Entities
{
	public class States
	{
		[JsonProperty("states")]
		public IList<State> StateList { get; set; }

		[JsonProperty("ttl")]
		public int TTL { get; set; }
	}
}
