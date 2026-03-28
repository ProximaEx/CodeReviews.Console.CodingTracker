using System.Globalization;
namespace CodingSessionObj;

public class CodingSession
{
	private static string culture = CultureInfo.CurrentCulture.ToString();

	public int Id { get; set; }
	public string StartDT { get; set; }
	public string EndDT { get; set; }
	public int DurationSecs { get; set; }
	public string StartDateLocal { get; }
	public string StartTime { get; }
	public string EndTime { get; }
	public string Duration { get; }

	public CodingSession(int idIn, string startDateTimeIn, string endDateTimeIn, int durationSecondsIn)
	{
		Id = idIn;
		StartDT = startDateTimeIn;
		EndDT = endDateTimeIn;
		DurationSecs = durationSecondsIn;

		DateTime startUTC = DateTime.ParseExact(startDateTimeIn, "u", new CultureInfo(culture));
		DateTime endUTC = DateTime.ParseExact(endDateTimeIn, "u", new CultureInfo(culture));
		StartDateLocal = startUTC.ToLocalTime().ToShortDateString();
		StartTime = startUTC.ToLocalTime().ToString("h:mm tt");
		EndTime = endUTC.ToLocalTime().ToString("h:mm tt");

		int minutes = (DurationSecs % 3600) / 60;
		int hours = (DurationSecs % 86400) / 3600;
		int days = DurationSecs / 86400;
		string mins = minutes > 0 ? $"{minutes}min" : "";
		string hrs = hours > 0 ? $"{hours}hrs" : "";
		string dys = days > 0 ? $"{days}days" : "";
		string minHrSpace = minutes > 0 && hours > 0 ? " " : "";
		string hrDaySpace = hours > 0 && days > 0 ? " " : "";
		Duration = $"{dys}{hrDaySpace}{hrs}{minHrSpace}{mins}";
	}

	//			 +---------------------------------------------------+
	//			 |	Model			Controller		Parse/Views		 |
	// +---------+---------------------------------------------------+
	// | Id		 |	int				int				int				 |
	// | StartDT |	string ymdhms	DT-UTC	*		DO-Local		 | 
	// | EndDT	 |	string ymdhms	DT-UTC	*		--				 |
	// | ST		 |	--				--				string 0:00	tt	 |
	// | ET		 |	--				--				string 0:00	tt	 |
	// | Dur	 |	int				int		*		--				 |
	// | DString |	--				--				string d:h:m:s	 |
	// +---------+---------------------------------------------------+

	string[] modelClassUnitTest =
		{
			"unit test for model class"
		//	};
		//DateTime start1 = new DateTime(2025, 12, 12, 01, 01, 0);
		//string startTime1 = start1.ToUniversalTime().ToString("u");
		//DateTime end1 = new DateTime(2025, 12, 12, 22, 35, 30);
		//string endTime1 = end1.ToUniversalTime().ToString("u");
		//TimeSpan dur1 = end1 - start1;
		//int durSec = (int)dur1.TotalSeconds;
		//CodingSession abc = new(01, startTime1, endTime1, durSec);
		//Console.WriteLine("Should be 12/12/2025 1:01 to 1:35, 21 hrs 34 min 30 sec\n------------------------------------------");
		//Console.WriteLine("1  " + abc.StartDateTime);
		//Console.WriteLine("2  " + abc.EndDateTime);
		//Console.WriteLine("3  " + abc.DurationSeconds);
		//Console.WriteLine("4  " + abc.StartDateLocal);
		//Console.WriteLine("5  " + abc.StartTime);
		//Console.WriteLine("6  " + abc.EndTime);
		//Console.WriteLine("7  " + abc.Duration);
		//Console.ReadLine();
		};
}
