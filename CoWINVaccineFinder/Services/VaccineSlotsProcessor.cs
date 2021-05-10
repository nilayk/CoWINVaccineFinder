using CoWINVaccineFinder.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace CoWINVaccineFinder.Services
{
	public class VaccineSlotsProcessor
	{
		private static readonly int MinAge = 18;

		private static Centers GetVaccineCenters(District district, string date)
		{
			Centers centers = null;
			var endpoint = //new Uri($"https://run.mocky.io/v3/eb152e48-e378-4332-88e3-442fe39ec398");
						   // new Uri($"https://run.mocky.io/v3/36186cac-43e8-4e7e-838a-38a478d34de0");
			new Uri($"https://cowin.gov.in/api/v2/appointment/sessions/public/calendarByDistrict?district_id={district.Id}&date={date}");

			try
			{
				var request = (HttpWebRequest)WebRequest.Create(endpoint);
				request.Method = "GET";
				request.UserAgent = $"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/14.1 Safari/605.1.15";

				//Console.WriteLine($"Requesting vaccine slots in {district.Name} for {date}");

				using var response = request.GetResponse();
				using var stream = response.GetResponseStream();
				using var reader = new StreamReader(stream);

				var data = reader.ReadToEnd();
				centers = JsonConvert.DeserializeObject<Centers>(data, new IsoDateTimeConverter { DateTimeFormat = "dd-MM-yyyy" });

				//Console.WriteLine($"Found and parsed {centers?.CenterList.Count}\n");

				reader.Close();
				stream.Close();
				response.Close();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Could not get vaccine slots for {district.Name} on {date}");
				Console.WriteLine(ex);
			}

			return centers;
		}

		public static Dictionary<Center, List<Session>> GetAvailableCentersForWeek(District district, string date)
		{
			var availableCenters = new Dictionary<Center, List<Session>>();

			var vaccineCenterList = GetVaccineCenters(district, date)?.CenterList;
			foreach (var center in vaccineCenterList)
			{
				var sessions = center.Sessions;
				foreach (var session in sessions)
				{
					if (session.MinAgeLimit <= MinAge && session.AvailableCapacity > 0)
					{
						if (!availableCenters.ContainsKey(center))
						{
							availableCenters.Add(center, new List<Session>());
						}
						availableCenters[center].Add(session);
					}
				}
			}

			return availableCenters;
		}
	}
}
