<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Stocks.aspx.cs" Inherits="Stocks" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 160px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table class="auto-style1">
            <tr>
                <td class="auto-style2">Store ID</td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Product ID</td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Quantity</td>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Button ID="Button1" runat="server" Text="New" />
&nbsp;
                    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Save" />
                </td>
                <td>
                    <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Update" />
&nbsp;
                    <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Delete" />
                </td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;
                    <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="AllSearch" />
                </td>
                <td>
                    <asp:Button ID="Button6" runat="server" OnClick="Button6_Click" style="height: 26px" Text="PSearch" />
&nbsp;&nbsp;
                    <asp:Button ID="Button7" runat="server" Text="Report" OnClick="Button7_Click" />
                </td>
            </tr>
        </table>
    <div>
    
    </div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="storeid" HeaderText="storeid" SortExpression="storeid" />
                <asp:BoundField DataField="productid" HeaderText="productid" SortExpression="productid" />
                <asp:BoundField DataField="quantity" HeaderText="quantity" SortExpression="quantity" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Stocks]"></asp:SqlDataSource>
    </form>
</body>
</html>
