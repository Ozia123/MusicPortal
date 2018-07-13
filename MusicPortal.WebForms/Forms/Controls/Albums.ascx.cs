using MusicPortal.DAL.Entities;
using MusicPortal.WebForms.Forms.Base;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MusicPortal.WebForms.Forms.Controls {
    public partial class Albums : AppUserControl {
        protected override void Page_Load(object sender, EventArgs e) {
            if (!Page.IsPostBack) {
                base.Page_Load();
            }
        }

        public void BindAlbums(Func<Album, bool> predicate, int page, int itemsPerPage) {
            base.Page_Load();

            var albums = Database.AlbumRepository.GetWithInclude(predicate: predicate, includeProperties: x => x.Artist)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .ToList();

            rptAlbums.DataSource = albums;
            rptAlbums.DataBind();
        }

        protected void rptAlbums_ItemDataBound(object sender, RepeaterItemEventArgs e) {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) {
                var btn = (LinkButton)e.Item.FindControl("lbtnAlbum");
                btn.Text = ((Album)e.Item.DataItem).Name;
            }
        }

        protected void lbtnAlbum_Click(object sender, EventArgs e) {
            var btn = (LinkButton)sender;
            Response.Redirect($"/forms/pages/AlbumInfo?name={btn.Text}");
        }
    }
}