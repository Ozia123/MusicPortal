<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Artists.ascx.cs" Inherits="MusicPortal.WebForms.Forms.Controls.Artists" %>

<asp:Repeater ID="rptArtists" runat="server" OnItemDataBound="rptArtists_ItemDataBound">
    <ItemTemplate>
        <div class="col-md-2 track-holder">
            <img class="rounded-image col-md-12" src="<%# Eval("PictureURL") %>">
            <div class="text-wrapper">
                <span class="text col-md-12">
                    <b><asp:LinkButton ID="lbtnArtist" OnClick="lbtnArtist_Click" runat="server"></asp:LinkButton></b>
                </span>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>