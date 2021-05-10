using Newtonsoft.Json;

namespace CoWINVaccineFinder.Entities
{
	public class District
	{
		[JsonProperty("district_id")]
		public int Id { get; set; }

		[JsonProperty("district_name")]
		public string Name { get; set; }
	}
}
