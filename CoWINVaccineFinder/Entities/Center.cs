using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoWINVaccineFinder.Entities
{
	public class Center
	{
		[JsonProperty("center_id")]
		public int CenterId { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("state_name")]
		public string StateName { get; set; }

		[JsonProperty("district_name")]
		public string DistrictName { get; set; }

		[JsonProperty("block_name")]
		public string BlockName { get; set; }

		[JsonProperty("pincode")]
		public int Pincode { get; set; }

		[JsonProperty("lat")]
		public double Latitude { get; set; }

		[JsonProperty("long")]
		public double Longitude { get; set; }

		[JsonProperty("from")]
		public string From { get; set; }

		[JsonProperty("to")]
		public string To { get; set; }

		[JsonProperty("fee_type")]
		public string FeeType { get; set; }

		[JsonProperty("sessions")]
		public IList<Session> Sessions { get; set; }
	}
}
