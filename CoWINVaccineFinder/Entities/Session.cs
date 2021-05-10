using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CoWINVaccineFinder.Entities
{
	public class Session
	{
		[JsonProperty("session_id")]
		public string SessionId { get; set; }

		[JsonProperty("date")]
		public DateTime Date { get; set; }

		[JsonProperty("available_capacity")]
		public decimal AvailableCapacity { get; set; }

		[JsonProperty("min_age_limit")]
		public decimal MinAgeLimit { get; set; }

		[JsonProperty("vaccine")]
		public string Vaccine { get; set; }

		[JsonProperty("slots")]
		public List<string> Slots { get; set; }
	}
}
