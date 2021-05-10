using CoWINVaccineFinder.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CoWINVaccineFinder
{
	class Program
	{
		static void Main(string[] args)
		{
			var eligibleSlots = new List<object>();

			var states = StatesProcessor.Build();
			var statesJson = JsonConvert.SerializeObject(states, Formatting.Indented);

			do
			{
				// foreach (var state in states?.StateList)
				Parallel.ForEach(states?.StateList, state =>
				{
					Console.WriteLine($"Processing districts for {state.Name}...");
					//if (string.Compare(state.Name, "Karnataka", StringComparison.OrdinalIgnoreCase) == 0)
					Parallel.ForEach(state.Districts?.DistrictList, district =>
					{
						Console.WriteLine($"Processing vaccine slots for {district.Name}, {state.Name}...");
						//if (string.Compare(district.Name, "BBMP", StringComparison.OrdinalIgnoreCase) == 0)
						var dateFormatter = $"dd-MM-yyyy";
						var today = DateTime.Now;
						int weeksToCheck = 1;
						//while (true)
						for (int i = 0; i < weeksToCheck; i++)
						{
							var dateToPass = today.AddDays(7 * i);
							//String districtId = "294"; // Bangalore
							//Console.WriteLine($"Checking week of {dateToPass.ToString(dateFormatter)}:");

							var availableCenters = VaccineSlotsProcessor.GetAvailableCentersForWeek(district, dateToPass.ToString(dateFormatter));

							//Console.WriteLine($"{availableCenters.Count} slots are available for 18+ age group.");
							if (availableCenters.Count > 0)
							{
								Console.WriteLine($"Found {availableCenters.Count} eligible vaccine slots for {district.Name}, {state.Name}");
								// Console.WriteLine($"{availableCenters.Count} available vaccine center found!");

								var centerIndex = 1;
								foreach (var center in availableCenters)
								{
									var centerObj = new
									{
										center.Key.CenterId,
										center.Key.Name,
										center.Key.BlockName,
										center.Key.DistrictName,
										center.Key.Pincode,
										State = state.Name,
										center.Key.FeeType,
										Hours = $"{center.Key.From} - {center.Key.To}"
									};
									eligibleSlots.Add(centerObj);
									// Console.WriteLine($"\n{centerIndex}: {JsonConvert.SerializeObject(centerObj, Formatting.Indented)}");
									centerIndex++;
								}
							}
						}
					});
				});

				var output = JsonConvert.SerializeObject(eligibleSlots, Formatting.Indented);
				Console.WriteLine($"{output}");

				Thread.Sleep(600 * 1000); // check every 10 minutes seconds

			} while (true);
		}
	}
}
