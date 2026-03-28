Habit Tracker Project
=====================



Requirements:
-------------


Features
--------


Thought Process
---------------


What Was Hard
-------------


What Was Easy
-------------


What Have I Learned
-------------------


Areas to Improve
----------------


Build Notes
-----------

	---Model---
Sqlite db for records, json variables, local variables

	---View---
possibly use mix of PEx formats and Spectre Console

outline menu screens, ui design for option pages

when to allow scrolling

	---Controller---
all the code to interact with menus, gather input,
interact with the model, and change views

	---Possible classes needed---
- views
MADE - code record object
- PEx formats
- navigation for views
- CRUD and db functions
- user input
- validation
- animation arrays and call methods

		   +---------------------------------------------------+
		   |	Model			Controller		Parse/Views	   |
 +---------+---------------------------------------------------+
 | Id	   |	int				int				int			   |
 | StartDT |	string ymdhms	DT-UTC	*		DO-Local	   | 
 | EndDT   |	string ymdhms	DT-UTC	*		--			   |
 | ST	   |	--				--				string 0:00	tt |
 | ET	   |	--				--				string 0:00	tt |
 | Dur	   |	int				int		*		--			   |
 | DString |	--				--				string d:h:m:s |
 +---------+---------------------------------------------------+

