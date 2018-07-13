using MusicPortal.DAL.Entities;
using MusicPortal.WebForms.Forms.Base;
using System;
using System.Linq;
using System.Web.UI;

namespace MusicPortal.WebForms.Forms.Pages {
    public partial class About : AppPage {
        protected override void Page_Load(object sender, EventArgs e) {
            if (!Page.IsPostBack) {
                base.Page_Load(sender, e);

                InitializePage();

                Func<Track, bool> tracksPredicate = x => x.CloudURL != null && x.ArtistId != null;
                Func<Artist, bool> artistsPredicate = x => x.Tracks.Any(y => y.CloudURL != null);

                TracksControl.BindTracks(tracksPredicate, 1, 5);
                ArtistsControl.BindArtists(artistsPredicate, 1, 5);
            }
        }

        private void InitializePage() {
            artistsCountLiteral.Text = Database.ArtistRepository.Query().Count().ToString();

            albumsCountLiteral.Text = Database.AlbumRepository.Query().Count().ToString();

            tracksCountLiteral.Text = Database.TrackRepository.Query().Count().ToString();

            uploadedTracksCountLiteral.Text = Database.TrackRepository.Query().Count(x => x.CloudURL != null).ToString();
        }
    }
}