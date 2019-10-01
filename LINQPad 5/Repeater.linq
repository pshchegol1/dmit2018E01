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
	var results = from x in Albums // First Option
where x.Tracks.Count() > 25
select new AlbumDTO
{
	AlbumTitle = x.Title,
	AlbumArtist = x.Artist.Name,
	Trackcount = x.Tracks.Count(),
	PlayTime = x.Tracks.Sum(z => z.Milliseconds),
	AlbumTracks = (from y in x.Tracks
				select new TruckPOCO
				{
					SongName = y.Name,
					SongGenre = y.Genre.Name,
					SongLength = y.Milliseconds
				}).ToList()
				
};
results.Dump();


 

}


public class TruckPOCO
{
	public string SongName {get; set;}
	public string SongGenre {get; set;}
	public int SongLength {get; set;}
}

public class AlbumDTO 
{
	public string AlbumTitle {get;set;}
	public string AlbumArtist {get;set;}
	public int Trackcount {get;set;}
	public int PlayTime {get;set;}
	public List<TruckPOCO> AlbumTracks{get;set;}
}

// Define other methods and classes here
