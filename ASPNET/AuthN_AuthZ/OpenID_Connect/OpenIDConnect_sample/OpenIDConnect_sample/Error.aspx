<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="OpenIDConnect_sample.Error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Error!</h1>
            <h3>There is problem while signing in.</h3>            
        </div>
        <div>
            <h4>Error message:</h4>
            <p><asp:Label ID="lblErrorMessage" runat="server"></asp:Label></p>            
        </div>
        <div>
            Please contact your server administrator.
        </div>
    </form>
</body>
</html>
