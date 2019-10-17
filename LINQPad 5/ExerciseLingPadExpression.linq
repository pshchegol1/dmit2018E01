<Query Kind="Expression">
  <Connection>
    <ID>b4de7a79-afb7-4051-ace0-ef74b8cccca7</ID>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

// If the release year is unknown, use the string Unknown
from x in Albums
where x.ReleaseYear == 2001
select new 
{
	Albutitle = x.Title,
	ArtistName = x.Artist.Name,
	Label = x.ReleaseLabel == null ? "Unknown": x.ReleaseLabel
}


//Terenary operator
from x in Albums
select new 
{
	title = x.Title,
	decade = x.ReleaseYear > 1969 && x.ReleaseYear < 1980 ? "70s" :
	x.ReleaseYear > 1979 && x.ReleaseYear < 1990 ? "80s" :
	x.ReleaseYear > 1989 && x.ReleaseYear < 2000 ? "90s" : "modern"
	
}