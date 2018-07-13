using MusicPortal.DAL.Entities;
using MusicPortal.WebForms.Forms.Base;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MusicPortal.WebForms.Forms.Controls {
    public partial class Artists : AppUserControl {
        protected override void Page_Load(object sender, EventArgs e) {
            if (!Page.IsPostBack) {
                base.Page_Load();
            }
        }

        public void BindArtists(Func<Artist, bool> predicate, int page, int itemsPerPage) {
            base.Page_Load();

            var artists = Database.ArtistRepository.GetWithInclude(predicate: predicate, includeProperties: x => x.Tracks)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .ToList();

            rptArtists.DataSource = artists;
            rptArtists.DataBind();
        }

        protected void rptArtists_ItemDataBound(object sender, RepeaterItemEventArgs e) {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) {
                var btn = (LinkButton)e.Item.FindControl("lbtnArtist");
                btn.Text = ((Artist)e.Item.DataItem).Name;
            }
        }

        protected void lbtnArtist_Click(object sender, EventArgs e) {
            var btn = (LinkButton)sender;
            Response.Redirect($"/forms/pages/ArtistInfo?name={btn.Text}");
        }
    }
}