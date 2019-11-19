using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additonal Namespaces
using ChinookSystem.BLL;
using ChinookSystem.Data.Entities;
using ChinookSystem.Data.POCOs;
using DMIT2018Common.UserControls;
using WebApp.Security;
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
            if (Request.IsAuthenticated)
            {
                if (User.IsInRole("Customers") || User.IsInRole("Customer Service"))
                {
                    var username = User.Identity.Name;
                    SecurityController sysmgr = new SecurityController();
                    int? customerid = sysmgr.GetCurrentUserCustomerId(username);
                    if (customerid.HasValue)
                    {
                        MessageUserControl.TryRun((()=> {
                           CustomerController sysmg = new CustomerController();
                           Customer info = sysmg.Customer_Get(customerid.Value);
                           CustomerName.Text = info.FullName;
                         }));
                    }
                    else
                    {
                        MessageUserControl.ShowInfo("UnRegistered User", "This User is not a Registered Customer");
                        CustomerName.Text = "Unregistered User";
                    }
                }
                else
                {
                    //redirect to a page that states no authorization fot the request action
                    Response.Redirect("~/Security/AccessDenied.aspx");
                }
            }
            else
            {
                //redirect to login page
                Response.Redirect("~/Account/Login.aspx");
            }
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
                //string username = "HansenB";
                //Once Security is implemented, you can obtain the user name from User.Identity class property .Name
                string username = User.Identity.Name;

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
            List<string> reasons = new List<string>();
            //Is there a playlist?
            //  No msg
            if(PlayList.Rows.Count == 0)
            {
                reasons.Add("There is no playlist present. Fetch your playlist");
            }
            //is there a playlist name? 
            //  No msg
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                reasons.Add("You must have a playlist name");
            }
            //Traverse playlist and collect selected row or rows
            //Only > 1 row selected
            //  Bad message  
            int trackid = 0;
            int tracknumber = 0;
            int rowsSelected = 0;
            CheckBox playlistselection = null;
            for (int rowindex = 0; rowindex < PlayList.Rows.Count; rowindex++)
            {
                //Access the control on the indexed GridViewRow
                //Set the CheckBox pointer to this checkbox control
                playlistselection = PlayList.Rows[rowindex].FindControl("Selected") as CheckBox;
                if (playlistselection.Checked)
                {
                    //Increment selected number of rows
                    rowsSelected++;
                    //Gather the data needed for the BLL call.
                    trackid = int.Parse((PlayList.Rows[rowindex].FindControl("TrackID")as Label).Text);
                    tracknumber = int.Parse((PlayList.Rows[rowindex].FindControl("TrackNumber") as Label).Text);
                }
            }
            if(rowsSelected != 1)
            {
                reasons.Add("Select Only One Track to Move!");

            }
            //Check if last track
            //  bad msg.
            if(tracknumber == PlayList.Rows.Count)
            {
                reasons.Add("Last Track Cannot be moved down ");
            }
            //Validation good:
            
            //good:
            
            if(reasons.Count == 0)
            {
                //  move track:
                MoveTrack(trackid, tracknumber, "down");

            }
            else
            {
                //no: display all errors
                MessageUserControl.TryRun(() => {

                   throw new BusinessRuleException("Track Move Error", reasons);
                    
                    
                    });
            }

        }

        protected void MoveUp_Click(object sender, EventArgs e)
        {
            List<string> reasons = new List<string>();
            //Is there a playlist?
            //  No msg
            if (PlayList.Rows.Count == 1)
            {
                reasons.Add("There is no playlist present. Fetch your playlist");
            }
            //is there a playlist name? 
            //  No msg
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                reasons.Add("You must have a playlist name");
            }
            //Traverse playlist and collect selected row or rows
            //Only > 1 row selected
            //  Bad message  
            int trackid = 0;
            int tracknumber = 0;
            int rowsSelected = 0;
            CheckBox playlistselection = null;
            for (int rowindex = 0; rowindex < PlayList.Rows.Count; rowindex++)
            {
                //Access the control on the indexed GridViewRow
                //Set the CheckBox pointer to this checkbox control
                playlistselection = PlayList.Rows[rowindex].FindControl("Selected") as CheckBox;
                if (playlistselection.Checked)
                {
                    //Increment selected number of rows
                    rowsSelected++;
                    //Gather the data needed for the BLL call.
                    trackid = int.Parse((PlayList.Rows[rowindex].FindControl("TrackID") as Label).Text);
                    tracknumber = int.Parse((PlayList.Rows[rowindex].FindControl("TrackNumber") as Label).Text);
                }
            }
            if (rowsSelected != 1)
            {
                reasons.Add("Select Only One Track to Move!");

            }
            //Check if last track
            //  bad msg.
            if (tracknumber == 1)
            {
                reasons.Add("First Track Cannot be moved up ");
            }
            //Validation good:

            //good:

            if (reasons.Count == 0)
            {
                //  move track:
                MoveTrack(trackid, tracknumber, "up");

            }
            else
            {
                //no: display all errors
                MessageUserControl.TryRun(() => {

                    throw new BusinessRuleException("Track Move Error", reasons);


                });
            }

        }

        protected void MoveTrack(int trackid, int tracknumber, string direction)
        {
            //call BLL to move track
            MessageUserControl.TryRun(() => {

                PlaylistTracksController sysmgr = new PlaylistTracksController();
                sysmgr.MoveTrack(User.Identity.Name,PlaylistName.Text, trackid, tracknumber, direction);

                List<UserPlaylistTrack> datainfo = sysmgr.List_TracksForPlaylist(PlaylistName.Text, User.Identity.Name);
                PlayList.DataSource = datainfo;
                PlayList.DataBind();

            }, "Success","Track has been moved");
 
        }


        protected void DeleteTrack_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Required Data", "PlayList Name is Required to ADD a Track");
            }
            else
            {
                if(PlayList.Rows.Count == 0)
                {
                    MessageUserControl.ShowInfo("Required Data", "No Play List is available. Retrieve your playlist");
                }
                else
                {
                    //Traverse the firdview and collect the list of tracks to remove.
                    List<int> tracktodelete = new List<int>();
                    int rowSelected = 0;
                    CheckBox  playlistselection = null;
                    for (int rowindex = 0; rowindex < PlayList.Rows.Count; rowindex++)
                    {
                        //Access the control on the indexed GridViewRow
                        //Set the CheckBox pointer to this checkbox control
                        playlistselection = PlayList.Rows[rowindex].FindControl("Selected") as CheckBox;
                        if (playlistselection.Checked)
                        {
                            //Increment selected number of rows
                            rowSelected++;
                            //Gather the data needed for the BLL call.
                            tracktodelete.Add(int.Parse((PlayList.Rows[rowindex].FindControl("TrackID") as Label).Text));

                           
                        }
                    }
                    if (rowSelected == 0)
                    {
                        MessageUserControl.ShowInfo("Required Data", "PlayList Name is Required to Remove a Track");
                    }
                    else
                    {
                        //Using the obtained data, issue your call to the BLL Method
                        //This work will be done whinin a TryRun()
                        MessageUserControl.TryRun(() =>
                        {

                            PlaylistTracksController sysmgr = new PlaylistTracksController();
                            //there is only one call to add the data to the database
                            sysmgr.DeleteTracks(User.Identity.Name, PlaylistName.Text,tracktodelete);

                            //Refresh the PlayList is a READ
                            List<UserPlaylistTrack> datainfo = sysmgr.List_TracksForPlaylist(PlaylistName.Text, User.Identity.Name);
                            PlayList.DataSource = datainfo;
                            PlayList.DataBind();

                        }, "Deleting a Track", "Track Has been Deleted from the Play List");
                    }
                }
            }


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