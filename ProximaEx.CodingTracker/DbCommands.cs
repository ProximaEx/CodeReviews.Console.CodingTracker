using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using CodingSessionObj;

namespace DbCommands;

public class DbCmds
{
	static IConfiguration config = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json")
			.Build();
	static readonly string? connectionString = config.GetConnectionString("Default");
	static readonly int seedQuant = config.GetValue<int>("SeedSettings:Quantity");
	static readonly SqliteConnection connection = new(connectionString);
	static Random rand = new();

	public static void CreateIfNotExists()
	{
		using (connection)
		{
			connection.Execute(
				@"CREATE TABLE IF NOT EXISTS coding_sessions (
				Id INTEGER PRIMARY KEY AUTOINCREMENT,
				StartDT TEXT,
				EndDT TEXT,
				DurationSecs INTEGER
				)");
		}
	}

	public static void SeedData()
	{
		List<CodingSession> seedSessions = [];
		DateTime now = DateTime.Now.ToUniversalTime();

		for (int i = 0; i < seedQuant; i++)
		{
			DateTime start = now.AddDays( -i - 2 ).AddMinutes( rand.Next( 0 , 720 ) );
			DateTime end = start.AddMinutes( rand.Next( 10 , 360 ) );
			string sDT = start.ToString("u");
			string eDT = end.ToString("u");
			int span = (int) (end - start).TotalSeconds;
			seedSessions.Add( new CodingSession( i+1 , sDT , eDT , span ) );
		}
		using (connection)
		{
			connection.Execute(
				@"INSERT INTO coding_sessions
				( StartDT, EndDT, DurationSecs )
				VALUES ( @StartDT, @EndDT, @DurationSecs )
				", seedSessions);
			// if this doesn't work, try a foreach loop cycling through each seedSession item
		}
	}

	public static void Insert(CodingSession newSession)
	{
		using (connection)
		{
			connection.Execute(
				@"INSERT INTO coding_sessions
				( StartDT, EndDT, DurationSecs )
				VALUES ( @StartDT, @EndDT, @DurationSecs )
				", newSession);
		}
	}

	// DateTime1 and DateTime2 arguments must be in UTC and in "u" Universal Sortable format
	public static void View( string DateTime1 = "", string DateTime2 = "", bool sortDesc = false )
	{
		string filter = DateTime1 != "" ? $"WHERE StartDT BETWEEN {DateTime1} AND {DateTime2}" : "";
		string sort = sortDesc ? "DESC" : "";
		string queryCmd = 
			$"SELECT * FROM coding_sessions {filter} ORDER BY StartDT {sort}";

		using (connection)
		{
			var queriedSessions = connection.Query<CodingSession>(queryCmd).ToList();
			// express list, send to view or return as list object
		}
	}

	public static void Update(CodingSession editedSession)
	{		
		using (connection)
		{
			connection.Execute(
				@"UPDATE coding_sessions SET
				StartDT = @StartDT,
				EndDT = @EndDT,
				DurationSecs = @DurationSecs
				WHERE Id = @Id
				", editedSession );
		}
	}

	public static void Delete(int idIn)
	{
		using (connection)
		{
			connection.Execute(
				@"DELETE FROM coding_sessions
				WHERE Id = @Id
				", new {Id = idIn} );
		}
	}
}