<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Transaction.aspx.cs" Inherits="Transaction" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Sales Transactions Summary</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css">
    <style>
        /* General body styles */
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            background-color: #f4f4f4;
            display: flex;
            flex-direction: column;
            min-height: 100vh;
            width: 100%;
        }
        

        /* Form container styling */
        form {
            flex: 1;
            display: flex;
            flex-direction: column;
            align-items: center;
        }

        /* Customer management section */
        .Transation-management {
            background-color: #fff;
            padding: 50px;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            width: 100%;
            
            box-sizing: border-box;
            margin-top: 20px;
        }

        .customer-management h2 {
            margin: 0;
            padding: 0;
            text-align: center;
            color: #335968;
        }

        /* Stats section styling */
        .stats {
            display: flex;
            justify-content: space-between;
            margin-bottom: 30px;
        }

        .stat {
            text-align: center;
        }

        .stat h3 {
            margin: 0;
            color: #990000;
        }

        .amount {
            font-size: 24px;
            font-weight: bold;
            color: #333;
        }

        /* Customers list section */
        .customers-list {
            padding: 0;
            margin-top: 20px;
        }

        .customers-list h3 {
            color: #990000;
            margin-bottom: 20px;
        }

        /* Search box styling */
        .search-box {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 20px;
            width: 100%;
            box-sizing: border-box;
        }

        .search-box .left-section,
        .search-box .right-section {
            display: flex;
            align-items: center;
            gap: 10px;
        }

        .search-box .asp-textboxx {
            padding: 8px;
            border: 1px solid #ddd;
            border-radius: 4px;
            width: 200px;
        }

        /* Button styling */
        .button {
            padding: 10px 20px;
            border: none;
            cursor: pointer;
            font-size: 16px;
            border-radius: 4px;
            transition: background-color 0.3s ease;
        }

        .viewbill {
            background-color: #be103c;
            color: #fff;
        }

       .viewbill:hover {
           background-color: #fff;
           color: #000;
          
       }

        /* Specific button stylings */
        .delete { background-color: #922828; color: #fff; }
        .refresh { background-color: #42c3ab; color: #fff; }
        .add { background-color: #257525; color: #fff; }

        .button:hover {
            opacity: 0.8;           
        }

        

        /* GridView styling */
        #GridView1 {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
            background-color: #fff;
            border: 1px solid #ddd;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        #GridView1 th, #GridView1 td {
            padding: 6px;
            border: 1px solid #ddd;
            text-align: left;
        }

       #GridView1 td {
          font-size: 13px;
        }

        #GridView1 th {
            background-color: #1e4b5e;
            color: #fff;
            font-weight: bold;
            font-size: 15px;
            text-align: center;
        }

        #GridView1 tr:nth-child(even) {
            background-color: #f9f9f9;
        }

        
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="Transation-management">
            <h2>Sales Transaction Summary</h2>
            <div class="stats">
            <div class="stat">
                <h3>Total Sales</h3>
                <div class="amount"><asp:Label ID="lblTotalSales" runat="server" Text="₹0"></asp:Label></div>
            </div>
            <div class="stat">
                <h3>Total Balance Due</h3>
                <div class="amount"><asp:Label ID="lblBalanceDue" runat="server" Text="₹0"></asp:Label></div>
            </div>
        </div>

            <div class="customers-list">
                <h3>Transaction List</h3>
                <div class="search-box">
                    <div class="left-section">
                        <asp:LinkButton ID="Button4" runat="server" CssClass="delete button" OnClick="Button4_Click"><i class="fas fa-trash-alt"></i></asp:LinkButton>
                        <asp:LinkButton ID="Button3" runat="server" CssClass="refresh button" OnClick="Button3_Click"><i class="fas fa-sync-alt"></i></asp:LinkButton>
                        <asp:Button CssClass="add button" ID="btnAddSales" runat="server" Text="Add Sales" OnClick="btnAddSales_Click" />
                        <asp:Button CssClass="add button" ID="Button1" runat="server" Text="Back" OnClick="btnBack_Click" />
                    </div>
                    <div class="right-section">
                        <asp:TextBox ID="TextBox1" runat="server" placeholder="Search by Bill" CssClass="asp-textboxx" AutoPostBack="True"></asp:TextBox>
                        <asp:LinkButton ID="Button2" runat="server" CssClass="refresh button" OnClick="Button2_Click1"><i class="fas fa-search"></i></asp:LinkButton>
                    </div>
                </div>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" SelectionMode="Single">
                    <Columns>
                        <asp:BoundField DataField="PartyName" HeaderText="Party Name" SortExpression="PartyName" />
                        <asp:BoundField DataField="PartyAddress" HeaderText="Party Address" SortExpression="PartyAddress" />
                        <asp:BoundField DataField="PhoneNumber" HeaderText="Phone Number" SortExpression="PhoneNumber" />
                        <asp:BoundField DataField="GSTIN" HeaderText="GSTIN" SortExpression="GSTIN" />
                        <asp:BoundField DataField="BillNumber" HeaderText="Bill Number" SortExpression="BillNumber" />
                        <asp:BoundField DataField="YourGSTIN" HeaderText="Your GSTIN" SortExpression="YourGSTIN" />
                        <asp:BoundField DataField="BillDate" HeaderText="Bill Date" SortExpression="BillDate" />
                        <asp:BoundField DataField="StateOfSupply" HeaderText="State Of Supply" SortExpression="StateOfSupply" />
                        <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" SortExpression="TotalAmount" />
                        <asp:BoundField DataField="BalanceDue" HeaderText="Balance Due" SortExpression="BalanceDue" />
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:Button ID="btnAction" runat="server" Text="View Customer Bill" CssClass="viewbill button" CommandName="Action" OnClick="btnAction_Click" CommandArgument='<%# Eval("BillNumber") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Sales]"></asp:SqlDataSource>
            </div>
        </div>
    </form>
</body>
</html>
