<%@ Page Language="C#" AutoEventWireup="true" CodeFile="trying.aspx.cs" Inherits="FA_Master_trying" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick="ExportExcel" />
        </div>
    </form>
</body>
</html>
