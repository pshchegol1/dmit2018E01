<Query Kind="Program">
  <Connection>
    <ID>b4de7a79-afb7-4051-ace0-ef74b8cccca7</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

void Main()
{
	var results  = from x in Albums
	where x.Artist.Name.Contains("Deep Purple")
	orderby x.ReleaseYear, x.Title
	select new AlbumsOfArtist
	{
		Title = x.Title,
		ArtistName = x.Artist.Name,
		RYear = x.ReleaseYear,
		RLabel = x.ReleaseLabel
	};
	results.Dump();
}

// Define other methods and classes here
public class AlbumsOfArtist
{
	public string Title {get;set;}
	public string ArtistName{get;set;}
	public int RYear{get;set;}
	public string RLabel{get;set;}
}