using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additonal Namespaces
using ChinookSystem.BLL;
using ChinookSystem.Data.POCOs;
//using ChinookSystem.Data.POCOs;
//using WebApp.Security;
#endregion

namespace Jan2018DemoWebsite.SamplePages
{
    public partial class ManagePlaylist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TracksSelectionList.DataSource = null;
        }

        protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e);
        }

        protected void ArtistFetch_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(ArtistName.Text))
            {
                //using MessageUserControl to display a message.
                MessageUserControl.ShowInfo("Missing Data", "Enter Partial Artist Name.");
            }
            else
            {
                MessageUserControl.TryRun(() =>
                {
                    SearchArg.Text = ArtistName.Text;
                    TracksBy.Text = "Artist";
                    TracksSelectionList.DataBind();//Causes the ODS to execute.

                }, "Tracks Search", "Select from the following list to add to yotur Playlist");
            }


        }

        protected void MediaTypeFetch_Click(object sender, EventArgs e)
        {


            
            MessageUserControl.TryRun(() =>
            {
                SearchArg.Text = MediaTypeDDL.SelectedValue;
                TracksBy.Text = "MediaType";
                TracksSelectionList.DataBind();//Causes the ODS to execute.

            }, "Tracks Search", "Select from the following list to add to yotur Playlist");
            

        }

        protected void GenreFetch_Click(object sender, EventArgs e)
        {

            MessageUserControl.TryRun(() =>
            {
                SearchArg.Text = GenreDDL.SelectedValue;
                TracksBy.Text = "Genre";
                TracksSelectionList.DataBind();//Causes the ODS to execute.

            }, "Tracks Search", "Select from the following list to add to yotur Playlist");


        }

        protected void AlbumFetch_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(AlbumTitle.Text))
            {
                //using MessageUserControl to display a message.
                MessageUserControl.ShowInfo("Missing Data", "Enter Partial Album Name.");
            }
            else
            {
                MessageUserControl.TryRun(() =>
                {
                    SearchArg.Text = AlbumTitle.Text;
                    TracksBy.Text = "Album";
                    TracksSelectionList.DataBind();//Causes the ODS to execute.

                }, "Tracks Search", "Select from the following list to add to yotur Playlist");
            }

        }

        protected void PlayListFetch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Required DATA", "Play List Name is Required to fetch");
            }
            else
            {
                string playlistname = PlaylistName.Text;
                //Until we do security, we will use a hard coded username
                string username = "HansenB";

                //Do standard query look up to your control, use MessageUserControl for Error handling
                MessageUserControl.TryRun(() =>
                {
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    List<UserPlaylistTrack> datainfo = sysmgr.List_TracksForPlaylist(playlistname, username);
                    PlayList.DataSource = datainfo;
                    PlayList.DataBind();
                }, "Play List Tracks", "See current tracks on playlist");
            }
 
        }

        protected void MoveDown_Click(object sender, EventArgs e)
        {
            //code to go here
 
        }

        protected void MoveUp_Click(object sender, EventArgs e)
        {
            //code to go here
 
        }

        protected void MoveTrack(int trackid, int tracknumber, string direction)
        {
            //call BLL to move track
 
        }


        protected void DeleteTrack_Click(object sender, EventArgs e)
        {
            //code to go here
 
        }

        protected void TracksSelectionList_ItemCommand(object sender, 
            ListViewCommandEventArgs e)
        {
            //Do we have the playlist name
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Required Data", "PlayList Name is Required to ADD a Track");
            }
            else
            {
                //Collect the required data for the event
                string playlistname = PlaylistName.Text;
                //The user name will come from the form security 
                //so until security is added, we will use HanseB
                string username = "HansenB";
                //Obtain the TrackId from the listView
                //the TrackId will be in the CommandArg property of the ListViewCommandEventArg e instance
                //The CommandArg in e is returned as an object
                //case it to a string, then you can .Parse the string
                int trackid = int.Parse(e.CommandArgument.ToString());

                //Using the obtained data, issue your call to the BLL Method
                //This work will be done whinin a TryRun()
                MessageUserControl.TryRun(() => 
                {

                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    //there is only one call to add the data to the database
                    sysmgr.Add_TrackToPLaylist(playlistname, username, trackid);

                    //Refresh the PlayList is a READ
                    List<UserPlaylistTrack> datainfo = sysmgr.List_TracksForPlaylist(playlistname, username);
                    PlayList.DataSource = datainfo;
                    PlayList.DataBind();

                },"Adding a Track","Track Has been added to the Play List");
            }
            
        }

    }
}