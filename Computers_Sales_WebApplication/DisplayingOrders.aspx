<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DisplayingOrders.aspx.cs" Inherits="Computers_Sales_WebApplication.DisplayingOrders" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:DataGrid ID="GridView1" AutoGenerateColumns="True" runat="server"></asp:DataGrid>
        </div>
    </form>
</body>
</html>
