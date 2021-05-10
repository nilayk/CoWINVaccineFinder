using CoWINVaccineFinder.Entities;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace CoWINVaccineFinder.Services
{
	public static class StatesProcessor
	{
		private static States _states { get; set; }

		private static void GetDistrictsForState(State state)
		{
			var endpoint = // new Uri($"https://run.mocky.io/v3/0e4d8d07-ecd8-49e5-b66d-0fc96f4fb0a5");
			new Uri($"https://cowin.gov.in/api/v2/admin/location/districts/{state.Id}");

			try
			{
				var request = (HttpWebRequest)WebRequest.Create(endpoint);
				request.Method = "GET";
				request.UserAgent = $"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/14.1 Safari/605.1.15";

				//Console.WriteLine($"Start       =>  Fetching Districts list for {state.Name}...");
				//Console.WriteLine($"Endpoint    =>  {request.RequestUri}");
				using var response = request.GetResponse();
				using var stream = response.GetResponseStream();
				using var reader = new StreamReader(stream);

				var rawDistricts = reader.ReadToEnd();

				state.Districts = JsonConvert.DeserializeObject<Districts>(rawDistricts);

				reader.Close();
				stream.Close();
				response.Close();
				//Console.WriteLine($"Done        =>  Fetched and processed {state.Districts.DistrictList.Count} districts list built for {state.Name}");
				//Console.WriteLine($"Districts   =>");
				//Console.WriteLine($"{JsonConvert.SerializeObject(state.Districts, Formatting.Indented)}\n");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Could not get districts for {state.Id}");
				Console.WriteLine(ex);
			}
		}

		public static States Build()
		{
			try
			{
				var path = Path.Combine(Directory.GetCurrentDirectory(), @"Resources", @"states.json");
				var rawStates = File.ReadAllText(path);
				//Console.WriteLine("Start => Building States list...");
				_states = JsonConvert.DeserializeObject<States>(rawStates);
				Console.WriteLine($"Done => {_states.StateList.Count} States & UTs list processed...\n");
			}
			catch (Exception ex)
			{
				Console.WriteLine("Unable to read states list from resources folder.");
				Console.WriteLine(ex);
			}

			Parallel.ForEach(_states?.StateList, state =>
			{
				GetDistrictsForState(state);
			});

			//foreach (var state in _states?.StateList)
			//{
			//    GetDistrictsForState(state);
			//}ˆ

			return _states;
		}
	}
}
