<Query Kind="Expression">
  <Connection>
    <ID>b4de7a79-afb7-4051-ace0-ef74b8cccca7</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

// Create a list of all album contain the album title and artist
//along with all the tracks (song name and genre, length) of that album

from x in Albums // First Option
select new 
{
	title = x.Title,
	artist = x.Artist.Name,
	tracks = from y in x.Tracks
				select new 
				{
					song = y.Name,
					genre = y.Genre.Name,
					length = y.Milliseconds
				}
}



from x in Albums //Second option
select new 
{
	title = x.Title,
	artist = x.Artist.Name,
	tracks = from y in Tracks
			 where x.AlbumId == y.AlbumId
				select new 
				{
					song = y.Name,
					genre = y.Genre.Name,
					length = y.Milliseconds
				}
}