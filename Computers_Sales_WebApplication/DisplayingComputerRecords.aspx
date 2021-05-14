<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DisplayingComputerRecords.aspx.cs" Inherits="Computers_Sales_WebApplication.DisplayingComputerRecords" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>

<body>
   <h2><a href="Computers/">Filter Date</a></h2>
<h2><a href="DisplayingOrders.aspx">Display Orders</a></h2>
<form id="form1" runat="server">
    <div>
        <h1>The List Of Computers Available At Our Store Is:</h1>
        <asp:DataGrid ID="GridView1" AutoGenerateColumns="True" runat="server"></asp:DataGrid>
    </div>
</form>
</body>
</html>