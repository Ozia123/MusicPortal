<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Tracks.ascx.cs" Inherits="MusicPortal.WebForms.Forms.Controls.Tracks" %>

<asp:Repeater ID="rptTracks" runat="server" OnItemDataBound="rptTracks_ItemDataBound">
    <ItemTemplate>
        <div class="col-md-2 track-holder">
            <img class="rounded-image col-md-12" src="<%# Eval("PictureURL") %>">
            <div class="text-wrapper">
                <span class="text col-md-12">
                    <b><asp:LinkButton ID="lbtnArtistFromTrack"  OnClick="lbtnArtistFromTrack_Click" runat="server"></asp:LinkButton></b> - <%# Eval("Name") %>
                </span>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>