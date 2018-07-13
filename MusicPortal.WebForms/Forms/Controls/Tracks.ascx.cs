using MusicPortal.DAL.Entities;
using MusicPortal.ViewModels.ViewModels;
using MusicPortal.WebForms.Forms.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MusicPortal.WebForms.Forms.Controls {
    public partial class Tracks : AppUserControl {
        protected override void Page_Load(object sender, EventArgs e) {
            if (!Page.IsPostBack) {
                base.Page_Load(sender, e);
            }
        }

        public void BindTracks(Func<Track, bool> predicate, int page, int itemsPerPage) {
            base.Page_Load(null, null);

            var tracks = Database.TrackRepository.GetWithInclude(predicate: predicate, includeProperties: x => x.Artist)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .ToList();

            rptTracks.DataSource = Mapper.Map<List<TrackViewModel>>(tracks);
            rptTracks.DataBind();
        }

        protected void lbtnArtistFromTrack_Click(object sender, EventArgs e) {
            var btn = (LinkButton)sender;
            Response.Redirect($"/forms/pages/ArtistInfo?name={btn.Text}");
        }

        protected void rptTracks_ItemDataBound(object sender, RepeaterItemEventArgs e) {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) {
                var btn = (LinkButton)e.Item.FindControl("lbtnArtistFromTrack");
                btn.Text = ((TrackViewModel)e.Item.DataItem).ArtistName;
            }
        }
    }
}