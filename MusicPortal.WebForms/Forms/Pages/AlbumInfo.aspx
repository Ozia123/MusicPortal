<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AlbumInfo.aspx.cs" MasterPageFile="../Masters/Site.Master"
    Inherits="MusicPortal.WebForms.Forms.Pages.AlbumInfo" %>

<%@ Register Src="../Controls/Albums.ascx" TagName="Albums" TagPrefix="uc1" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="artist-info-header"><%: Title %> by <asp:LinkButton runat="server" ID="lbtnArtist" OnClick="lbtnArtist_Click" /></h1>
    
    <div class="artist-info-content">
        <asp:Image ID="imgAlbum" CssClass="header-image" runat="server" />
        <span class="artist-bio">
            <asp:Repeater ID="rptTracks" runat="server">
                <ItemTemplate>
                    <div class="col-md-12">
                        <%# Eval("Name") %>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </span>
    </div>

    <h2 class="any-header">Other albums by <asp:LinkButton runat="server" ID="lbtnArtistAlbums" OnClick="lbtnArtist_Click" />:</h2>
    <div class="tracks col-md-12">
        <uc1:Albums ID="AlbumsControl" runat="server" />
    </div>
</asp:Content>