using System;

namespace MusicPortal.WebForms.Forms.Controls {
    public partial class Search : System.Web.UI.UserControl {
        protected void Page_Load(object sender, EventArgs e) {
        }

        protected void btnSearch_Click(object sender, EventArgs e) {
            if (!string.IsNullOrEmpty(txtSearch.Text)) {
                Response.Redirect($"/forms/pages/search?query={txtSearch.Text}");
            }
        }
    }
}