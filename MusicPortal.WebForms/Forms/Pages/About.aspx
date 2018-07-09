<%@ Page Title="About" Language="C#" MasterPageFile="~/Forms/Masters/Site.Master" AutoEventWireup="true"  
   CodeBehind="About.aspx.cs" Inherits="MusicPortal.WebForms.Forms.Pages.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h5 style="line-height: 1.2">
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
    
    <h3 class="block-header">Listen right now!</h3>
    <div class="tracks">
        <asp:Repeater ID="rptTracks" runat="server">
            <ItemTemplate>
                <div class="col-md-2 track-holder">
                    <img class="rounded-image col-md-12" src="<%# Eval("PictureURL") %>">
                    <div class="text-wrapper">
                        <span class="text col-md-12"><%# Eval("Name") %></span>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

    <h3 class="block-header">Or check artists, which tracks we have uploaded</h3>
    <div class="tracks">
        <asp:Repeater ID="rptArtists" runat="server">
            <ItemTemplate>
                <div class="col-md-2 track-holder">
                    <img class="rounded-image col-md-12" src="<%# Eval("PictureURL") %>">
                    <div class="text-wrapper">
                        <span class="text col-md-12"><%# Eval("Name") %></span>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

</asp:Content>
