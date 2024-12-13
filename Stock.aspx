<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Stock.aspx.cs" Inherits="Stock" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Stock Management</title>
    <style type="text/css">
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            background-color: #f0f0f0;
            padding: 30px;
            display: inline-flexbox;
            justify-content: center;
            align-items: center;
            height: 100vh;
        }

        .container {
            background-color: #fff;
            padding: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            border-radius: 8px;
            width: 100%;
            max-width: 1200px;
            display: flex;
            height: auto;
            box-sizing: border-box;
        }

        .form-section, .grid-section {
            box-sizing: border-box;
            padding: 10px;
            margin: 0;
        }

        .form-section {
            width: 40%;
            border: 2px solid #000;
        }

        .grid-section {
            width: 60%;
            border: 2px solid #000;
            background: #e8dfaf;
        }

        h3 {
            text-align: center;
            margin-bottom: 20px;
            font-size: 1.5em;
            color: #333;
        }

        table {
            width: 101%;
        }

        td {
            padding: 8px;
            vertical-align: middle;
        }

        
        .asp-textbox {
            width: 100%;
            padding: 8px;
            box-sizing: border-box;
            border: 1px solid #ccc;
            border-radius: 4px;
        }

        .buttons td {
            text-align: center;
        }

        .newbutton td {
            text-align: left;
        }

        .grid-section .storedetails {
            text-align: center;
            font-size: 1.2em;
            margin-bottom: 10px;
        }

        .grid-section asp:gridview {
            width: 100%;
            border-collapse: collapse;
        }

        .grid-section asp:gridview th,td {
            border: 1px solid #ccc;
            padding: 8px;
            text-align: left;
        }

        .grid-section Columns {
            background-color: #fff;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="form-section">
                <h3>Stores</h3>
                <table>
                    <tr>
                        <td>Store ID</td>
                        <td class="auto-style1">
                            <asp:TextBox ID="t1" runat="server" CssClass="asp-textbox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Product ID</td>
                        <td class="auto-style1">
                            <asp:TextBox ID="t2" runat="server" CssClass="asp-textbox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Quantity</td>
                        <td class="auto-style1">
                            <asp:TextBox ID="t3" runat="server" CssClass="asp-textbox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="newbutton">
                        <td colspan="2">
                            <asp:Button ID="b1" runat="server" Text="New" Width="72px" />
                        </td>
                    </tr>
                    <tr class="buttons">
                        <td colspan="2">
                            <asp:Button ID="Button1" runat="server" Text="Save" OnClick="Button1_Click" Width="72px" />
                            <asp:Button ID="Button2" runat="server" Text="Delete" Width="72px" OnClick="Button2_Click" />
                            <asp:Button ID="Button4" runat="server" Text="Search" Width="72px" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="grid-section">
                <h3 class="storedetails">Store Details</h3>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="storeid" HeaderText="Store ID" SortExpression="storeid" />
                        <asp:BoundField DataField="productid" HeaderText="Product ID" SortExpression="productid" />
                        <asp:BoundField DataField="quantity" HeaderText="Quantity" SortExpression="quantity" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                    SelectCommand="SELECT * FROM [Stocks]"></asp:SqlDataSource>
            </div>
        </div>
    </form>
</body>
</html>
