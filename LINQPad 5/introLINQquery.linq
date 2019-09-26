<Query Kind="Expression">
  <Connection>
    <ID>b4de7a79-afb7-4051-ace0-ef74b8cccca7</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//Sample of query syntax to dump the artist data

from x in Artists
select x

//Sample of method syntax to dump the artist data
Artists
   .Select (x => x)
   
//Sort datainfo.Sort((x,y) => x.AttributeName.CompareTo(y.AttributeName))

//Find any Artist whose name contains the string 'son'
from x in Artists 
where x.Name.Contains("son")
select x

Artists
	.Where(x => x.Name.Contains("son"))
	.Select(x => x)
	
//Create a list of Albums released in 1970
//Orderby title

from x in Albums
where x.ReleaseYear == 1970
orderby x.Title
select x

Albums
	.Where(x => x.ReleaseYear== 1970)
	.OrderBy(x => x.Title)
	.Select(x => x)
	
	//Create list of album released between 2007 and 2018
	//Order by release year then by title
	
	from x in Albums
	where x.ReleaseYear  >=2007
	&& x.ReleaseYear <=2018
	orderby x.ReleaseYear descending, x.Title
	select x
	
//Note the deferences in method names using the method syntax
//A descending orderby is .OrderByDescending
//Secondary and beyond ordering is .ThenBy
Albums
   .Where (x => ((x.ReleaseYear >= 2007) && (x.ReleaseYear <= 2018)))
   .OrderByDescending (x => x.ReleaseYear)
   .ThenBy (x => x.Title)
   
//Can Navigational properties by used in queries
// Create a list of albums by Deep Purple
//Order By release year and title
//Show only title, artist name release year and release label.

// Use the navigational properties to obtain the artist data.
// new {....} creates new dataset (class definition) 
from x in Albums
where x.Artist.Name.Contains("Deep Purple")
orderby x.ReleaseYear, x.Title
select new
{
	Title = x.Title,
	ArtistName = x.Artist.Name,
	RYear = x.ReleaseYear,
	RLabel = x.ReleaseLabel
}
