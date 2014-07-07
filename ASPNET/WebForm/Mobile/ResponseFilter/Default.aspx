<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>無題のページ</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            アアア<br/>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button1：なにもしない" Width="500px" /><br/>
            <asp:Button ID="Button2" runat="server" Text="Button2：フィルタを仕掛ける" OnClick="Button2_Click" Width="500px" /><br/>
            <asp:Button ID="Button3" runat="server" Text="Button3：フィルタを仕掛ける（フラッシュする）" OnClick="Button3_Click" Width="500px" /><br/>
            イイイ<br/>
        </div>
    </form>
</body>
</html>
