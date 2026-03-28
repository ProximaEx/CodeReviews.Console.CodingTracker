using Microsoft.Extensions.Configuration;
using System;
using PExFormatting;
using DbCommands;
using CodingSessionObj;
using System.Reflection.Metadata;
using System.Dynamic;

namespace PExCodingTrackerProgram;

/*
----------------- Requirements ----------------------

Wait to implement feedback from habit tracker submission, if any

This application has the same requirements as the previous project, except
that now you'll be logging your daily coding time.

To show the data on the console, you should use the Spectre.Console library.

You're required to have separate classes in different files (i.e.
UserInput.cs, Validation.cs, CodingController.cs)

You should tell the user the specific format you want the date and time to
be logged and not allow any other format.

You'll need to create a configuration file called appsettings.json, which
will contain your database path and connection strings (and any other
configs you might need).

You'll need to create a CodingSession class in a separate file. It will
contain the properties of your coding session: Id, StartTime, EndTime,
Duration. When reading from the database, you can't use an anonymous object,
you have to read your table into a List of CodingSession.

The user shouldn't input the duration of the session. It should be
calculated based on the Start and End times

The user should be able to input the start and end times manually.

You need to use Dapper ORM for the data access instead of ADO.NET. (This
requirement was included in Feb/2024)

Follow the DRY Principle, and avoid code repetition.

Don't forget the ReadMe explaining your thought process.

__ORDER:___________________________
DONE	- Config file, 
DONE	- Model class, 
DONE	- DB/table creation, 
- View layout, 
DONE	- CRUD controller,
- Table visualisation, 
- UI controller,
- Validation, 
- Unit tests


------------------ Challenges -----------------------------

Real-time stopwatch to let users track coding session in app

Let users filter coding records by period (wk,M,yr) and sort (asc/dec)

MAUI desktop app, using tutorial from C# foundations course

------------------ Todo List -----------------------------

Spectre Console Series by Tim Corey w/ practice cs file

DONE	Menu tree outline and layout

DONE	Tutorial on Dapper

DONE	Build model architecture and nodes(required data vars)

...

*/

class Program
{
	static void Main(string[] args)
	{
		IConfiguration config = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json")
			.Build();
		bool hasRun = config.GetValue<bool>("AppData:HasRun");
		bool seedData = config.GetValue<bool>("SeedSettings:Enabled");
		//bool seedData = bool.Parse(config.GetSection("SeedSettings")["Enabled"]!); old way if above throws excep.
		bool askForUsername = false;

		if (!hasRun)
		{
			DbCmds.CreateIfNotExists();
			if (seedData) { DbCmds.SeedData(); }
			//write to config

			askForUsername = true;
		}

		Console.Write("success");
		Console.ReadLine();
		
		//LoadScreen();
		//LoadAnimation();

		//UsernameMessage(askForUsername); description below
		if (askForUsername)
		{
			//AskForUsername();
			//store username in json w/ seperate method
		}
		else
		{
			//WelcomeBackUser();
		}

		ProgramLoop();
		Goodbye();
	}

	static void ProgramLoop()
	{
		bool exit = false;
		while (!exit)
		{
			// draw main menu screen
			// option prompt
			var input = 0;
			switch (input)
			{
				case 0:
					NewSession();
					break;
				case 1:
					History();
					break;
				case 2:
					exit = true;
					break;
				default:
					break;
			}
		}
	}

	static void NewSession()
	{
		// draw new session view
		bool exitNew = false;
		while (!exitNew)
		{
			// option prompt
			var input = 0;
			if (input == 0) { EnterTimes(); exitNew = true; SessionMsg("Added"); }
			else if (input == 1) { UseTimer(); exitNew = true; SessionMsg("Added"); }
			else if (input == 2) { exitNew = true; }
			else { Console.WriteLine("sorry, invalid"); }
		}
	}

	static void EnterTimes()
	{
		// draw over enter times section
		// ask for date (press enter to use today)
			// validate for future date, date format, ridiculous past date
		// ask for start time
			// validate format
		// ask for end time
			// ask "Are you sure?" for durations that pass into the next day, validate format
		CodingSession newSesh = new(13,"","",100);
		DbCmds.Insert(newSesh);
	}

	static void UseTimer()
	{
		// draw timer screen
		bool stopTimer = false;
		bool paused = true;
		// timer thread
		while (!stopTimer)
		{
			// readkey for pause/start/stop
			var input = 0;
			switch (input)
			{
				case 0:
					paused = !paused;
					break;
				case 1:
					stopTimer = true;
					break;
				default:
					break;
			}
		}
		CodingSession newSesh = new(13, "", "", 100);
		DbCmds.Insert(newSesh);
	}

	static void SessionMsg(string action)
	{
		// message box
		Console.Write($"Record {action}.");
	}

	static void History()
	{
		// draw history view
		bool exitNew = false;
		while (!exitNew)
		{
			// option prompt
			var input = 0;
			if (input == 0) { ViewAll(); exitNew = true; }
			else if (input == 1) { FilterRange(); exitNew = true; }
			else if (input == 2) { break; }
			else { Console.WriteLine("sorry, invalid"); }
		}
		if (!exitNew) { return; }

		// draw modify, delete, exit section
		exitNew = false;
		while (!exitNew)
		{
			//option prompt
			var input = 0;
			if (input == 0) { GetRecordInfo("modify"); exitNew = true; }
			else if (input == 1) { GetRecordInfo("delete"); exitNew = true; }
			else if (input == 2) { break; }
			else { Console.WriteLine("sorry, invalid"); }
		}
	}

	static void GetRecordInfo(string actionType)
	{
		// ask for RecordID
		// ask for start date, take enter for using current
		// ask for start time
		// end time
		CodingSession session = new(13,"","",1); // placeholder session, to be constructed using answer inputs
		if (actionType == "modify") { DbCmds.Update(session); SessionMsg("Modified"); }
		else if (actionType == "delete") { DbCmds.Update(session); SessionMsg("Deleted"); }
	}

	static void ViewAll(string d1 = "", string d2 = "")
	{
		// ask sort descending message, return input
		var input = false;
		DbCmds.View(d1, d2, input);
	}

	static void FilterRange()
	{
		// draw filter range section
		// ask period
		// validate for week, month, year
		// number of periods
		// validate for positive integer >0 and <int max value
		// optional start date (if none, calculate start date from period before current datetime
		// validate for min datetime
		string date1 = DateTime.Today.ToUniversalTime().ToString("u"); // placeholder datetime conversion
		string date2 = DateTime.Today.ToUniversalTime().ToString("u"); // placeholder datetime conversion
		ViewAll(date1, date2);
	}

	static void Goodbye()
	{
		Console.Clear();
		Console.WriteLine("Goodbye!");
		Thread.Sleep(2000);
	}
}