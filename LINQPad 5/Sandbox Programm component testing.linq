<Query Kind="Program">
  <Connection>
    <ID>b4de7a79-afb7-4051-ace0-ef74b8cccca7</ID>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

void Main()
{
	string artistname = "th";
	var results  = from x in Albums
	where x.Artist.Name.Contains(artistname)
	orderby x.ReleaseYear, x.Title
	select new AlbumsOfArtist
	{
		Title = x.Title,
		ArtistName = x.Artist.Name,
		RYear = x.ReleaseYear,
		RLabel = x.ReleaseLabel
	};
	results.Dump();
	
	
	
	var customerlist = from x in Customers
     where x.Country.Equals("USA")
	 && x.Email.Contains("yahoo.com")
	 orderby x.LastName, x.FirstName
	 select new YahooCustomers
	 {
	 	Name = x.LastName + "," + x.FirstName,
		City = x.City,
		State = x.State,
		Email = x.Email
	 }
	 customerlist.Dump();
	
	
	
		 
	 	var whosang = from x in Tracks
		where x.Name.Equals("Rag Doll")
		select new
		{
			ArtistName = x.Album.Artist.Name,
			AlbumTitle = x.Album.Title,
			AlbumYear = x.Album.ReleaseYear,
			AlbumLabel = x.Album.ReleaseLabel,
			Composer = x.Composer
		};
whosang.Dump();
		
	
	
	
}// end void Main

// Define other methods and classes here
public class AlbumsOfArtist
{
	public string Title {get;set;}
	public string ArtistName{get;set;}
	public int RYear{get;set;}
	public string RLabel{get;set;}
}

	 
	 
	 
	 public class YahooCustomers
	 {
		public string Name {get;set;}
		public string City{get;set;}
		public string State{get;set;}
	
		public string Email{get;set;}
	 }