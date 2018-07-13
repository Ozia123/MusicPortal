using MusicPortal.DAL.Entities;
using MusicPortal.WebForms.Forms.Base;
using System;
using System.Threading.Tasks;
using System.Web.UI;

namespace MusicPortal.WebForms.Forms.Pages {
    public partial class ArtistInfo : AppPage {
        protected override void Page_Load(object sender, EventArgs e) {
            if (!Page.IsPostBack) {
                base.Page_Load(sender, e);
                
                InitializePage();
            }
        }

        private void InitializePage() {
            string artistName = Request.QueryString["name"];

            var artist = Task.Run(() => Database.ArtistRepository.GetByName(artistName)).Result;
            if (artist == null) {
                Response.Redirect("/forms/pages/about/");
            }

            Title = artist.Name;
            imgArtist.ImageUrl = artist.PictureURL;
            litArtistBio.Text = artist.Biography;

            Func<Album, bool> albumsPredicate = x => x.ArtistId == artist.ArtistId;
            Func<Track, bool> tracksPredicate = x => x.ArtistId == artist.ArtistId;

            AlbumsControl.BindAlbums(albumsPredicate, 1, 5);
            TracksControl.BindTracks(tracksPredicate, 1, 10);
        }
    }
}