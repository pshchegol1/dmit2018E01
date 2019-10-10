<Query Kind="Statements">
  <Connection>
    <ID>b4de7a79-afb7-4051-ace0-ef74b8cccca7</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//To get both the Albums with Tracks and without Tracks you can use .Uniom()
//In a Union you need to ensure  cast typing is correct
//									column cast types match identically.
//									each query has the same number of columns.
//									same order of columns.

//Create a list of all Albums, show the title, number of Tracks, total cost of Tracks
//average length(milliseconds) of the Tracks.

//Problem exists for albums without any Tracks. Summing and Averages need data to work
//If  an Album has no tracks, your work get an abort.
//Solution
//Create two queries: a) with tracks b)without tracks then .Union results

//Syntax: (query1).Union(query2).Union(queryn).OrderBy(first sort).ThenBy(sortn)

var unionsample = (from x in Albums
					where x.Tracks.Count() > 0
					select new
					{
						title = x.Title,
						trackcount = x.Tracks.Count(),
						priceoftracks = x.Tracks.Sum(y => y.UnitPrice),
						avereagelengthA = x.Tracks.Average(y => y.Milliseconds)/1000.0,
						avereagelengthB = x.Tracks.Average(y => y.Milliseconds)/1000.0
					}).Union(
				    from x in Albums
					where x.Tracks.Count() == 0
					select new
					{
						title = x.Title,
						trackcount = 0,
						priceoftracks = 0.00m,
						avereagelengthA = 0.00,
						avereagelengthB = 0.00
					}).OrderBy(y => y.trackcount).ThenBy(y => y.title);		
		unionsample.Dump();
	
		
		
// Boolean Filters .All() or .Any()
//.Any() method iterates throught the entire collection to see if any of the items match the specific condition
//returns a true or false
//an instance of the collection that receives a true is selected for processiong

Genres.OrderBy(x => x.Name).Dump();
//List Genres that have Trakcs which are not on any playlist
var genrelist = from x in Genres
				where x.Tracks.Any(tr => tr.PlaylistTracks.Count()==0)
				orderby x.Name
				select new
				{
					name = x.Name
				};
			
		genrelist.Dump();   
		
		
		
//.All() method iterates throught entire collection to see if all of the items match the specified condition
//returns a true or false.
//an instance of the collection that receives a true is selected for processiong.

//List Genres that have all the Trakcs appearing at least once on a playlist.
var populargenres = from x in Genres
					where x.Tracks.All(tr => tr.PlaylistTracks.Count()>0)
					orderby x.Name
					select new
					{
						name = x.Name,
						thetracks = (from y in Tracks where y.PlaylistTracks.Count() > 0
						select new
						{
							song = y.Name,
							cont = y.PlaylistTracks.Count()
						})
					};
					
	
	populargenres.Dump();	
		
		
		
//Sometimes you have two lists that need to be compare
//Usually you looking up for items that are the same(in both collections)
//OR you are looking for items that are different.
//In either case: you are comparing one collection to a secont collection.

//Obtain distinct list of all playlist tracks for Roberto Almedia (username AlmediaR)
var almeida = (from x in PlaylistTracks
where x.Playlist.UserName.Contains("Almeida")


orderby x.Track.Name
select new
{
	genre = x.Track.Genre.Name,
	id = x.TrackId,
	song = x.Track.Name
}


).Distinct();
almeida.Dump();		



//Obtain distinct list of all playlist tracks for Michalle (username BrooksM)		
var brooks = (from x in PlaylistTracks
where x.Playlist.UserName.Contains("Brooks")


orderby x.Track.Name
select new
{
	genre = x.Track.Genre.Name,
	id = x.TrackId,
	song = x.Track.Name
}


).Distinct();
//brooks.Dump();
		
// List tracks that both Roberto and Michelle like
var likes = almeida.Where(a => brooks.Any(b => b.id == a.id)).OrderBy(a => a.genre).Select(a => a);
//likes.Dump();
		
//list the Roberto's tracks that Michelle does not have.
var almeidaif = almeida.Where(a => brooks.Any(b => b.id == a.id)).OrderBy(a => a.genre).Select(a => a);
//almeidaif.Dump();
		
		
//list the Michell's tracks that Roberto does not have.
var brooksif = brooks.Where(a => almeida.Any(b => b.id == a.id)).OrderBy(a => a.genre).Select(a => a);
//brooksif.Dump();		
		
		

//Joins:
//Joins can be used where navigational properties do not exist
//joins can be used between associated entities
//scenario pkey = fkey

//left side of the joins should be the support data
//right side of the join is the record collection to be processed

//List Albums showig title, releaseyear, label, artist name and track count.

var results = from xrightside in Albums
		join yleftside in Artists
		on xrightside.ArtistId equals yleftside.ArtistId
		select new 
		{
			title = xrightside.Title,
			year = xrightside.ReleaseYear,
			label = xrightside.ReleaseLabel == null ? "Unknown" : xrightside.ReleaseLabel,
			artist = yleftside.Name,
			trackcount = xrightside.Tracks.Count()
		};
		results.Dump();


// Great examples at the following url:
// www.dotnetlearners.com/ling















		
		
		
		
		
		