using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespaces
using ChinookSystem.BLL;
using ChinookSystem.Data.Entities;
#endregion

namespace WebApp.SamplePages
{
    public partial class FilterSearchCRUD : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindArtistList();
                //Set  the max value for the validation control
                //RangeEditReleaseYear
                RangeEditReleaseYear.MaximumValue = DateTime.Today.Year.ToString();
            }
        }


        protected void BindArtistList()
        {
            ArtistController sysmgr = new ArtistController();
            List<Artist> info = sysmgr.Artist_List();
            info.Sort((x, y) => x.Name.CompareTo(y.Name));
            ArtistList.DataSource = info;
            ArtistList.DataTextField = nameof(Artist.Name);
            ArtistList.DataValueField = nameof(Artist.ArtistId);
            ArtistList.DataBind();
          //  ArtistList.Items.Insert(0, "Select...");
        }

        protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e);
        }

        protected void ArtistList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void AlbumList_SelectedIndexChanged(object sender, EventArgs e)
        {

            //Standard lookup
            GridViewRow agvrow = AlbumList.Rows[AlbumList.SelectedIndex];
            //Retreive the value from a web control located witnin the GridView cell
            string albumid = (agvrow.FindControl("AlbumId") as Label).Text;


            //error handling will need to be added
            MessageUserControl.TryRun(() =>
            {

                AlbumController sysmgr = new AlbumController();
                Album datainfo = sysmgr.Album_Get(int.Parse(albumid));
                if (datainfo == null)
                {
                    //clear the controls
                    //throw an exception

                    ClearControls();
                    throw new Exception("Record No longer Exist on file");
                }
                else
                {
                    EditAlbumID.Text = datainfo.AlbumId.ToString();
                    EditTitle.Text = datainfo.Title;
                    EditAlbumArtistList.SelectedValue = datainfo.ArtistId.ToString();
                    EditReleaseYear.Text = datainfo.ReleaseYear.ToString();
                    EditReleaseLabel.Text = datainfo.ReleaseLabel == null ?
                        "" : datainfo.ReleaseLabel;
                }
            },"Find Album", "Album Found");//strings on this line are success message.

        
            
            
            
            

        }

        protected void ClearControls()
        {
            EditAlbumID.Text = "";
            EditTitle.Text = "";
            EditReleaseYear.Text = "";
            EditReleaseLabel.Text = "";
            EditAlbumArtistList.SelectedIndex = 0;
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string albumtitle = EditTitle.Text;
                int albumyear =  int.Parse(EditReleaseYear.Text);
                string albumlabel = EditReleaseLabel.Text == "" ? null : EditReleaseLabel.Text;
                int albumartist = int.Parse(EditAlbumArtistList.SelectedValue);

                Album theAlbum = new Album();
                theAlbum.Title = albumtitle;
                theAlbum.ArtistId = albumartist;
                theAlbum.ReleaseYear = albumyear;
                theAlbum.ReleaseLabel = albumlabel;

                MessageUserControl.TryRun(() => 
                {

                    AlbumController sysmgr = new AlbumController();
                    int albumid = sysmgr.Album_Add(theAlbum);
                    EditAlbumID.Text = albumid.ToString();
                    if(AlbumList.Rows.Count > 0)
                    {
                        AlbumList.DataBind();
                    }
                    

                }, "Successful","Album Added");
            }
        }

        protected void Update_Click(object sender, EventArgs e)
        {
                if (Page.IsValid)
                {
                    int editalbumid = 0;
                    string albumid = EditAlbumID.Text;
                    if (string.IsNullOrEmpty(albumid))
                    {
                        MessageUserControl.ShowInfo("Attention","Look up the album before editing");
                    }
                    else if(int.TryParse(albumid, out editalbumid))
                    {
                        MessageUserControl.ShowInfo("Attention","Current Album ID is Invalid, Look Again!");
                    }
                    else
                    {

                

                        //string albumtitle = 
                        //int albumyear = 
                        //string albumlabel = EditReleaseLabel.Text == "" ? null : EditReleaseLabel.Text;
                        //int albumartist = int.Parse(EditAlbumArtistList.SelectedValue);

                        Album theAlbum = new Album();
                        theAlbum.AlbumId = editalbumid ;
                        theAlbum.Title = EditTitle.Text;
                        theAlbum.ArtistId = int.Parse(EditAlbumArtistList.SelectedValue);
                        theAlbum.ReleaseYear = int.Parse(EditReleaseYear.Text);
                        theAlbum.ReleaseLabel = EditReleaseLabel.Text == "" ? null : EditReleaseLabel.Text;

                    MessageUserControl.TryRun(() =>
                    {

                                AlbumController sysmgr = new AlbumController();
                                int rowsaffected = sysmgr.Album_Update(theAlbum);
                                EditAlbumID.Text = albumid.ToString();
                                if (rowsaffected > 0)
                                {
                                    AlbumList.DataBind();
                                }
                                else
                                {
                                    throw new Exception("Album was not found. Look up again");
                                }


                        }, "Successful", "Album Updated");
                    }
                }
        }

        protected void Remove_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                int editalbumid = 0;
                string albumid = EditAlbumID.Text;
                if (string.IsNullOrEmpty(albumid))
                {
                    MessageUserControl.ShowInfo("Attention", "Look up the album before editing");
                }
                else if (int.TryParse(albumid, out editalbumid))
                {
                    MessageUserControl.ShowInfo("Attention", "Current Album ID is Invalid, Look Again!");
                }
                else
                {

                    MessageUserControl.TryRun(() =>
                    {

                        AlbumController sysmgr = new AlbumController();
                        int rowsaffected = sysmgr.Album_Delete(editalbumid);
                        EditAlbumID.Text = albumid.ToString();
                        if (rowsaffected > 0)
                        {
                            AlbumList.DataBind();
                            EditAlbumID.Text = "";
                        }
                        else
                        {
                            throw new Exception("Album was not found. Remove again again");
                        }


                    }, "Successful", "Album Removed");
                }
            }

        }
    }
}