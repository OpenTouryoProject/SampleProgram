<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Translate.aspx.cs" Inherits="MicrosoftTranslator.Translate" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">

        function translateLanguage() {
            var textToTranslate = document.getElementById('<%= txtTranslate.ClientID %>').value;

            if (textToTranslate != "") {
                PageMethods.GetAccessToken(OnSucceeded, OnFailed);
            }
        }

        function OnSucceeded(result, usercontext, methodName) {
            var textToTranslate = document.getElementById('<%= txtTranslate.ClientID %>').value;
            var languageFrom = "en";
            var languageTo = "ja";

            if (textToTranslate != "") {
                window.mycallback = function (response) {
                    document.getElementById('<%= lblShow.ClientID %>').innerHTML = "The Translated Text is:" + response;
                }

                var text = encodeURIComponent(textToTranslate);
                var s = document.createElement("script");

                s.src = "http://api.microsofttranslator.com/V2/Ajax.svc/Translate?oncomplete=mycallback&appId=Bearer " + encodeURIComponent(result.access_token) + 
                        "&from=" + languageFrom + "&to=" + languageTo + "&text=" + text;
                document.getElementsByTagName("head")[0].appendChild(s);
            }
        }

        function OnFailed(error, userContext, methodName) {
            alert("Error");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager" runat="server" EnablePageMethods="true" />
        <asp:Label ID="lblText" runat="server" Text="Enter Text to Translate"></asp:Label>
        <asp:TextBox ID="txtTranslate" runat="server">
        </asp:TextBox>
        <asp:Button ID="btnTranslate" runat="server" Text="Translate language" OnClientClick="translateLanguage();return false;" />
        <br />
        <asp:Label ID="lblShow" runat="server">
        </asp:Label>
        <br />
    </div>
    </form>
</body>
</html>
