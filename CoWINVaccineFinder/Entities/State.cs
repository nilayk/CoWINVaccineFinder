using Newtonsoft.Json;

namespace CoWINVaccineFinder.Entities
{
	public class State
	{
		[JsonProperty("state_id")]
		public int Id { get; set; }

		[JsonProperty("state_name")]
		public string Name { get; set; }

		[JsonIgnore]
		public Districts Districts { get; set; }
	}
}
