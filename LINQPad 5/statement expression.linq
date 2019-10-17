<Query Kind="Statements">
  <Connection>
    <ID>b4de7a79-afb7-4051-ace0-ef74b8cccca7</ID>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

// When using the language C# Statement or Statements your work will need to confirm to C# statement syntax
// ie datatype variable = expression;


//Can Navigational properties by used in queries
// Create a list of albums by Deep Purple
//Order By release year and title
//Show only title, artist name release year and release label.

// Use the navigational properties to obtain the artist data.
// new {....} creates new dataset (class definition) 
var results  = from x in Albums
	where x.Artist.Name.Contains("Deep Purple")
	orderby x.ReleaseYear, x.Title
	select new
	{
		Title = x.Title,
		ArtistName = x.Artist.Name,
		RYear = x.ReleaseYear,
		RLabel = x.ReleaseLabel
	};
	
// To desplay the contents of a variable in linqPad use the method .Dump()
// This method is only used in LiqPad it is NOT a C# method
results.Dump();