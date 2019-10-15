<Query Kind="Expression">
  <Connection>
    <ID>c3acbdd8-ba7d-493f-9e49-8e882a5a3307</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

//group record collection a single field on the record
//the selected grouping field is reffered to as the group Key

//from x in Tracks
//group x by x.GenreId


//Group record collection using multiple fields on the  record
//The multiple fields become a group key instance
//refering to a property
from x in Tracks
group x by new {x.GenreId, x.MediaTypeId}


// λ - Method Syntax
//Tracks
  // .GroupBy (
    //  x => 
     //    new  
       //  {
       //     GenreId = x.GenreId, 
        //    MediaTypeId = x.MediaTypeId
        // }
  // )
   
   
   
//place the grouping of the large data collection into a temporary data collection
//ANY further reporting on the groups within the temporary data collection will use temporary data collection name
//as its data source


//Report the groups   
  from x in Tracks
  group x by x.GenreId into gGenre

  

   //details on each group
  from x in Tracks
  group x by x.GenreId into gGenre
  select new
  {
  	groupid = gGenre.Key,
	tracks = from x in gGenre
		select new
		{
			trackid = x.TrackId,
			song = x.Name,
			artist = x.Album.Artist.Name,
			lengthSong = x.Milliseconds/1000000.0
		}
  }
   
// Refer to a specific key property
from x in Tracks
group x by new {x.GenreId, x.MediaTypeId}
into gTracks
select new 
{
	genre = gTracks.Key.GenreId,
	media = gTracks.Key.MediaTypeId,
	trackcount = gTracks.Count()
}
   
//You can also group by class
from x in Tracks
group x by x.Genre into gTracks
select new 
{
	genre = gTracks.Key.GenreId,
	name = gTracks.Key.Name,
	trackcount = gTracks.Count()
}



   
from x in Tracks
group x by x.Album into gTracks
select new 
{
	
	name = gTracks.Key.Title,
	artist = gTracks.Key.Artist.Name,
	trackcount = gTracks.Count()
}





//Create a List of Albums by ReleaseYear showing the year, number of albums in that year, album title, count of tracks for each album
//
from x in Albums
group x by  x.ReleaseYear
into gRyear 
select new 
{
	year = gRyear.Key,
	albumcount = gRyear.Count(),
	anAlbum = from y in gRyear
	select new
	{
		title = y.Title,
		trackcount = (from t in y.Tracks select t ).Count()
	}
	
}
   
//Order the previous report by the number of the albums per year descending
//(what wasa the most productive year), order within count by year ascending.
//Report Only albums from 1990 and later.


//tip once you have group, all further clauses are against the group
from x in Albums
where x.ReleaseYear >= 1990
group x by  x.ReleaseYear
into gRyear 
orderby gRyear.Count() descending, gRyear.Key
select new 
{
	year = gRyear.Key,
	albumcount = gRyear.Count(),
	anAlbum = from y in gRyear
	select new
	{
		title = y.Title,
		trackcount = (from t in y.Tracks select t ).Count()
	}
	
}