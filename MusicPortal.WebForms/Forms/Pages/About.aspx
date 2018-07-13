<%@ Page Title="About" Language="C#" MasterPageFile="~/Forms/Masters/Site.Master" AutoEventWireup="true"  
   CodeBehind="About.aspx.cs" Inherits="MusicPortal.WebForms.Forms.Pages.About" %>

<%@ Register Src="../Controls/Search.ascx" TagName="Search" TagPrefix="uc1" %>
<%@ Register Src="../Controls/Tracks.ascx" TagName="Tracks" TagPrefix="uc1" %>
<%@ Register Src="../Controls/Artists.ascx" TagName="Artists" TagPrefix="uc1" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="artist-info-header"><%: Title %></h1>
    <h5 style="line-height: 1.2;">
        MusicPortal website uses <a href="https://last.fm" target="_blank">last.fm</a> api to collect newest information about music.
        <br/>
        So anytime you visit MusicPortal you see only latest updates from <a href="https://last.fm" target="_blank">last.fm</a>.
        <br/>
        Also there is functionality to listen music for free)
        <br/><br/>
        Now our database has collected:
    </h5>
    <ul>
        <li>
            <h5>
                <span class="highlighted-text"><asp:Literal runat="server" ID="artistsCountLiteral"/></span> unique artists
            </h5>
        </li>
        <li>
            <h5>
                <span class="highlighted-text"><asp:Literal runat="server" ID="albumsCountLiteral"/></span> unique albums
            </h5>
        </li>
        <li>
            <h5>
                <span class="highlighted-text"><asp:Literal runat="server" ID="tracksCountLiteral"/></span> unique tracks
            </h5>
        </li>
        <li>
            <h5>
                <span class="highlighted-text"><asp:Literal runat="server" ID="uploadedTracksCountLiteral"/></span> tracks you can listen right now!
            </h5>
        </li>
    </ul>
    
    <h2 class="block-header any-header">Listen right now!</h2>
    <div class="tracks">
        <uc1:Tracks ID="TracksControl" runat="server" />
    </div>

    <h2 class="block-header any-header">Or check artists, which tracks we have uploaded</h2>
    <div class="tracks">
        <uc1:Artists ID="ArtistsControl" runat="server" />
    </div>

</asp:Content>
