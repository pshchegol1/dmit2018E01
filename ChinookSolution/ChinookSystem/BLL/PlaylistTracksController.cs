using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.Data.Entities;
using ChinookSystem.Data.DTOs;
using ChinookSystem.Data.POCOs;
using ChinookSystem.DAL;
using System.ComponentModel;
using DMIT2018Common.UserControls;
#endregion

namespace ChinookSystem.BLL
{
    public class PlaylistTracksController
    {
        public List<UserPlaylistTrack> List_TracksForPlaylist(
            string playlistname, string username)
        {
            using (var context = new ChinookContext())
            {

                //What would happen if there is no match for the incoming parameter value
                //We need to ensure that the results have a valid value
                //this value will need to resolve to either a null or an IEnumerable<T> collection
                //To achieve a valid value you will need to determine using .FirstOrDefault() whether data exist or not.
                var results = (from x in context.Playlists
                               where x.UserName.Equals(username) && x.Name.Equals(playlistname)
                               select x).FirstOrDefault();
                //If the playlist does not exist .FirstOrDefault returns NULL
                if(results== null)
                {
                    return null;
                }
                else
                {
                    //IF the playlist does exist, query for the playlist tracks
                    var thetracks = from x in context.PlaylistTracks
                                    where x.PlaylistId.Equals(results.PlaylistId)
                                    orderby x.TrackNumber
                                    select new UserPlaylistTrack
                                    {
                                        TrackID = x.TrackId,
                                        TrackNumber = x.TrackNumber,
                                        TrackName = x.Track.Name,
                                        Milliseconds = x.Track.Milliseconds,
                                        UnitPrice = x.Track.UnitPrice
                                    };
                    return thetracks.ToList();
                }
               
            }
        }//eom
        public void Add_TrackToPLaylist(string playlistname, string username, int trackid)
        {
            using (var context = new ChinookContext())
            {
                //Use the businessRuleExeption to throw errors to the Web Page
                List<string> reasons = new List<string>();
                PlaylistTrack newTrack = null;
                int tracknumber = 0;


                //Part One:
                //Determine if Playlist exists
                //query the table using the playlist name and username
                //if the playlist exists, one will get a record
                //if the playlist does not exist, one will get a NULL
                //to ensure this results the query will be wrap in a .FirstOrDefault()


                //Playlist exists = context.Playlists
                //                    .Where(x => x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)
                //                    && x.Name.Equals(playlistname, StringComparison.OrdinalIgnoreCase))
                //                    .Select(x => x)
                //                    .FirstOrDefault();


                Playlist exists = (from x in context.Playlists
                                    .Where(x => x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)
                                     && x.Name.Equals(playlistname, StringComparison.OrdinalIgnoreCase))
                                   select x).FirstOrDefault();

                //Does the Playlist exists
                if(exists == null)
                {
                    //this is a new playlist
                    //create the Playlist record
                    exists = new Playlist();
                    exists.Name = playlistname;
                    exists.UserName = username;

                    //stage the add
                    exists = context.Playlists.Add(exists);
                    //since this is a new playlist, the track number will be one
                    tracknumber = 1;

                }
                else
                {
                    //Since the playlist exists, so may the track exist on the olaylisttracks
                    newTrack = exists.PlaylistTracks.SingleOrDefault(x => x.TrackId == trackid);
                    if(newTrack == null)
                    {
                        tracknumber = exists.PlaylistTracks.Count() + 1;
                    }
                    else
                    {
                        reasons.Add("Track Already Exists on Playlist");
                    }
                }

                //Part Two:
                    //Create the playlisttrack entry
                    //if there are any reasons not to create, then throw the businessRuleExeption
                    if(reasons.Count() > 0)
                    {
                    //issue with adding the track
                    throw new BusinessRuleException("Adding track to playlist", reasons);
                    }
                    else
                    {
                        //Use the Playlist navigation to playlisttracks to do the add to playlisttracks
                        newTrack = new PlaylistTrack();
                        newTrack.TrackId = trackid;
                        newTrack.TrackNumber = tracknumber;

                        //Note: the PK for playlistId may not yet exist
                        //using the navigation property on the PlayList entity one can let HshSet handle the PlayListId PKey
                        //value ti be properly created on PlayList ABD placed correctly in the "child" record of PlayListTracks

                        //What is wrong to the attempt: newTrack.PlayListId = exists.PlayListId;
                        exists.PlaylistTracks.Add(newTrack); //PlayListTrack staging
                                                             //Physically add any/all data to the database
                                                             //commit
                        context.SaveChanges();

                    }


            }
        }//eom
        public void MoveTrack(string username, string playlistname, int trackid, int tracknumber, string direction)
        {
            using (var context = new ChinookContext())
            {
                //Get PlayList ID 
                var exists = (from x in context.Playlists
                              where x.UserName.Equals(username) && x.Name.Equals(playlistname, StringComparison.OrdinalIgnoreCase)

                              select x).FirstOrDefault();
                if (exists == null)
                {
                    throw new Exception("PlayList does not exist.");
                }
                else
                {
                    PlaylistTrack moveTrack = (from x in exists.PlaylistTracks
                                               where x.TrackId == trackid
                                               select x).FirstOrDefault();
                    if (moveTrack == null)
                    {
                        throw new Exception("PlayList Track does not exist.");
                    }
                    else
                    {
                        PlaylistTrack otherTrack = null;
                        //Up or Down
                        if (direction.Equals("up"))
                        {
                            //up
                            if (tracknumber == 1)
                            {
                                throw new Exception("Track 1  cannot be moved up.");
                            }
                            else
                            {
                                otherTrack = (from x in exists.PlaylistTracks
                                              where x.TrackNumber == moveTrack.TrackNumber - 1
                                              select x).FirstOrDefault();
                                if (otherTrack == null)
                                {
                                    throw new Exception("PlayList is corrupt. Fetch Playlist Again");
                                }
                                else
                                {
                                    moveTrack.TrackNumber -= 1;
                                    otherTrack.TrackNumber += 1;
                                }
                            }
                        }
                        else
                        {
                            //down
                            
                            if (tracknumber == exists.PlaylistTracks.Count())
                            {
                                throw new Exception("Last track  cannot be moved down.");
                            }
                            else
                            {
                                otherTrack = (from x in exists.PlaylistTracks
                                              where x.TrackNumber == moveTrack.TrackNumber + 1
                                              select x).FirstOrDefault();
                                if (otherTrack == null)
                                {
                                    throw new Exception("PlayList is corrupt. Fetch Playlist Again");
                                }
                                else
                                {
                                    moveTrack.TrackNumber += 1;
                                    otherTrack.TrackNumber -= 1;
                                }
                            }
                        }//eof up or down
                             //staging
                             context.Entry(moveTrack).Property(y => y.TrackNumber).IsModified = true;
                             context.Entry(otherTrack).Property(y => y.TrackNumber).IsModified = true;

                            //commit
                            context.SaveChanges();
                        
                    }



                }
            }
        }//eom


        public void DeleteTracks(string username, string playlistname, List<int> trackstodelete)
        {
            using (var context = new ChinookContext())
            {

                //Playlist exists?

                //  NO: Message
          
                var exists = (from x in context.Playlists
                              where x.UserName.Equals(username,StringComparison.OrdinalIgnoreCase) && 
                              x.Name.Equals(playlistname,StringComparison.OrdinalIgnoreCase)
                              select x).FirstOrDefault();
                if (exists == null)
                {
                    throw new Exception("Playlist Has beem removed from the system");
                }
                else
                {
                    //  YES: create a list of playlisttracks that are to be kept
                    var trackskept = exists.PlaylistTracks
                        .Where(tr => !trackstodelete.Any(tod => tr.TrackId == tod))
                        .Select(tr => tr).ToList();
                    //stage removal of tracks
                    PlaylistTrack item = null;
                    foreach(var dtrackid in trackstodelete)
                    {
                        item = exists.PlaylistTracks
                            .Where(tr => tr.TrackId == dtrackid)
                            .FirstOrDefault();
                        if(item != null)
                        {
                            exists.PlaylistTracks.Remove(item);
                        }
                      
                    }
                    //renumbering of kept tracks an stage update
                    int number = 1;
                    trackskept.Sort((x, y) => x.TrackNumber.CompareTo(y.TrackNumber));
                    foreach(var tkept in trackskept)
                    {
                        tkept.TrackNumber = number;
                        context.Entry(tkept).Property(y => y.TrackNumber).IsModified = true;
                        number++;
                    }
                    //commit
                    context.SaveChanges();
                }

            }
        }//eom
    }
}
