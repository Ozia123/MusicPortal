using MusicPortal.DAL.Entities;
using MusicPortal.WebForms.Forms.Base;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MusicPortal.WebForms.Forms.Pages {
    public partial class AlbumInfo : AppPage {
        protected override void Page_Load(object sender, EventArgs e) {
            if (!Page.IsPostBack) {
                base.Page_Load(sender, e);

                InitializePage();
            }
        }

        private void InitializePage() {
            string albumName = Request.QueryString["name"];

            var album = Database.AlbumRepository
                .GetWithInclude(predicate: x => x.Name == albumName, includeProperties: x => x.Artist)
                .FirstOrDefault();

            if (album == null) {
                Response.Redirect("/forms/pages/about/");
            }

            Title = album.Name;
            imgAlbum.ImageUrl = album.PictureURL;
            lbtnArtist.Text = album.Artist.Name;
            lbtnArtistAlbums.Text = album.Artist.Name;

            rptTracks.DataSource = Database.TrackRepository.Query().Where(x => x.AlbumId == album.AlbumId).OrderByDescending(x => x.Rank).ToList();
            rptTracks.DataBind();

            Func<Album, bool> predicate = x => x.ArtistId == album.ArtistId && x.AlbumId != album.AlbumId;
            AlbumsControl.BindAlbums(predicate, 1, 5);
        }

        protected void lbtnArtist_Click(object sender, EventArgs e) {
            var btn = (LinkButton)sender;
            Response.Redirect($"/forms/pages/ArtistInfo?name={btn.Text}");
        }
    }
}