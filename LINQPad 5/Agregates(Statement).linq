<Query Kind="Statements">
  <Connection>
    <ID>b4de7a79-afb7-4051-ace0-ef74b8cccca7</ID>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

var resultavg = (from x in Tracks //First option
select x.Milliseconds).Average();

resultavg.Dump();


var resultAve = Tracks.Average(x => x.Milliseconds);// Second Option

resultAve.Dump();




var resultreport = from x in Tracks
select new
{
	song = x.Name,
	lengt = x.Milliseconds,
	LongShortAvg = x.Milliseconds > resultavg ? "Long":
	x.Milliseconds < resultavg ? "Short" : "Average"
};
resultreport.Dump();





// List all playlist which have a track showing the playlist name,
// number of tracks on the playlist, the cost of the playlist,
// total storage size for the playlist in megabytes.

//  Option #1
var resultPlaylist = from x in Playlists
where x.PlaylistTracks.Count() > 0
select new
{
	name = x.Name,
	trackcount = x.PlaylistTracks.Count(),
	cost = x.PlaylistTracks.Sum(p => p.Track.UnitPrice),
	storage = x.PlaylistTracks.Sum(p => p.Track.Bytes / 1000000.0)
};
resultPlaylist.Dump();

// Option #2
//var resultPlaylist = from x in Playlists
//where x.PlaylistTracks.Count() > 0
//select new
//{
//	name = x.Name,
//	trackcount = x.PlaylistTracks.Count(),
//	cost = x.PlaylistTracks.Sum(p => p.Track.UnitPrice),
//	storage =(from x in x.PlayListTracks
//				select x.Track.Bytes/1000000.0).Sum()
//};
//resultPlaylist.Dump();


// List all Albums with Tracks showing the Album title, Artist Name, Number of Tracks,
// and the Album Cost.

var albumresult = from x in Albums
select new
{
	albttitle = x.Title,
	artname = x.Artist.Name,
	numtrack = x.Tracks.Count() > 0
	
};
albumresult.Dump();


// What is the maximum album count for all the artists.

var result5 = (Artists.Select(x => x.Albums.Count())).Max();
		result5.Dump();
		
		
//var maxcount = result5.Max();
//maxcount.Dump();