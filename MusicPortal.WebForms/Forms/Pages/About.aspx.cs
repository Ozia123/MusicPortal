using MusicPortal.DAL.EF;
using MusicPortal.DAL.Factories;
using MusicPortal.WebForms.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MusicPortal.WebForms.Forms.Pages {
    public partial class About : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!Page.IsPostBack) {
                var context = ContextFactory.GetContext();

                InitializePage(context);
                FillTracks(context);
                FillArtists(context);
            }
        }

        private void InitializePage(ApplicationContext context) {
            artistsCountLiteral.Text = context.Artists.Count().ToString();

            albumsCountLiteral.Text = context.Albums.Count().ToString();

            tracksCountLiteral.Text = context.Tracks.Count().ToString();

            uploadedTracksCountLiteral.Text = context.Tracks.Count(x => x.CloudURL != null).ToString();
        }

        private void FillTracks(ApplicationContext context) {
            rptTracks.DataSource = context.Tracks.Where(x => x.CloudURL != null && x.PictureURL != null).OrderByDescending(x => x.TrackId).Take(5).ToList();
            rptTracks.DataBind();
        }

        private void FillArtists(ApplicationContext context) {
            rptArtists.DataSource = context.Artists.Where(x => x.Albums.Any(y => y.Tracks.Any(z => z.CloudURL != null))).Take(5).ToList();
            rptArtists.DataBind();
        }
    }
}