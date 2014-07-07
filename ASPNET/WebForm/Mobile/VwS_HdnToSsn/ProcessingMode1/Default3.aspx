<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default3.aspx.cs" Inherits="Default3" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <asp:Button ID="btnDataBind" runat="server" Text="データバインド（ViewState設定）" OnClick="btnDataBind_Click"  /><br/>
    <asp:GridView ID="GridView1" runat="server"></asp:GridView>
    <asp:Button ID="btnPostBack" runat="server" Text="空のポストバック" OnClick="btnPostBack_Click"  />
    <asp:Button ID="btnNext" runat="server" Text="次画面へ" OnClick="btnNext_Click"  />
</asp:Content>

