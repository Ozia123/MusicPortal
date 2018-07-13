<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Search.ascx.cs" Inherits="MusicPortal.WebForms.Forms.Controls.Search" %>

<script>
    var timerForSearch;
    var doneTypingIntervalMs = 1000;
    var lastKeyCode;

    function keyPressed(e) {
        lastKeyCode = e.keyCode;
            
        clearTimeout(timerForSearch);
        timerForSearch = setTimeout(function () {
            if (lastKeyCode != 13) {
                $('#ctl00_ContentPlaceHolder1_btnSearch').click();
            }
        }, doneTypingIntervalMs);
    }
</script>

<asp:Panel ID="pnlSearch" runat="server">
    <div runat="server" id="searchBox" class="search-box">
        <asp:TextBox ID="txtSearch" runat="server" CssClass="search_text_box" onkeydown="keyPressed(event)" placeholder="Search..."></asp:TextBox>
    </div>
    <div style="display: none;">
        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />
    </div>
</asp:Panel>