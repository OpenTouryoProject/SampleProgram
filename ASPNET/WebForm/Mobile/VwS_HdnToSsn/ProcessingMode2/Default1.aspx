<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default1.aspx.cs" Inherits="Default1" Title="Untitled Page" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>無題のページ</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Button ID="btnDataBind" runat="server" Text="データバインド（ViewState設定）" OnClick="btnDataBind_Click"  /><br/>
        <asp:GridView ID="GridView1" runat="server"></asp:GridView>
        <asp:Button ID="btnPostBack" runat="server" Text="空のポストバック" OnClick="btnPostBack_Click"  />
        <asp:Button ID="btnNext" runat="server" Text="次画面へ" OnClick="btnNext_Click"  />
    </div>
    </form>
</body>
</html>