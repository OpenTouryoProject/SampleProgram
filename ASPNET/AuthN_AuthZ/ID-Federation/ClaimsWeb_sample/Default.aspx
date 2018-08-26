<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ClaimsWeb_sample.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>
            Welcome to claims application</h1>
        <h3>
            This is a simple website</h3>
    </div>
    <div>
        <br />
        <br />
        Claim Information
        <br />
        <br />
        <asp:Repeater ID="Repeater1" runat="server">
            <HeaderTemplate>
                <table border="1" width="600">
                    <tr>
                        <td style="width: 300px">
                            <b>Claim Type</b>
                        </td>
                        <td style="width: 700px">
                            <b>Claim Value</b>
                        </td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td style="width: 300px;">
                        <%# DataBinder.Eval(Container.DataItem, "key") %>
                    </td>
                    <td style="width: 700px">
                        <%# DataBinder.Eval(Container.DataItem, "Value") %>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>
