<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Albums.ascx.cs" Inherits="MusicPortal.WebForms.Forms.Controls.Albums" %>

<asp:Repeater ID="rptAlbums" runat="server" OnItemDataBound="rptAlbums_ItemDataBound">
    <ItemTemplate>
        <div class="col-md-2 track-holder">
            <img class="square-image col-md-12" src="<%# Eval("PictureURL") %>">
            <div class="text-wrapper">
                <span class="text col-md-12">
                    <b><asp:LinkButton ID="lbtnAlbum" OnClick="lbtnAlbum_Click" runat="server"></asp:LinkButton></b>
                </span>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>