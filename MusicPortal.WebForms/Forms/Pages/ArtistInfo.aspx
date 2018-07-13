<%@ Page Title="Artist Info" Language="C#" MasterPageFile="../Masters/Site.Master" AutoEventWireup="true" 
    CodeBehind="ArtistInfo.aspx.cs" Inherits="MusicPortal.WebForms.Forms.Pages.ArtistInfo" %>

<%@ Register Src="../Controls/Search.ascx" TagName="Search" TagPrefix="uc1" %>
<%@ Register Src="../Controls/Tracks.ascx" TagName="Tracks" TagPrefix="uc1" %>
<%@ Register Src="../Controls/Albums.ascx" TagName="Albums" TagPrefix="uc1" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="artist-info-header"><%: Title %></h1>
    
    <div class="artist-info-content">
        <asp:Image ID="imgArtist" CssClass="header-image rounded-image" runat="server" />
        <span class="artist-bio">
            <asp:Literal ID="litArtistBio" runat="server" />
        </span>
    </div>
    
    <h2 class="any-header"><%: Title %>'s albums:</h2>
    <div class="tracks col-md-12">
        <uc1:Albums ID="AlbumsControl" runat="server" />
    </div>

    <h2 class="any-header"><%: Title %>'s tracks:</h2>
    <div class="tracks col-md-12">
        <uc1:Tracks ID="TracksControl" runat="server" />
    </div>

</asp:Content>