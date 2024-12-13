<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrontDashboard.aspx.cs" Inherits="shipment" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Sales and Purchase Summary</title>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet" />
    
    <style>
    body {
        font-family: Arial, sans-serif;
        margin: 0;
        padding: 0;
        background-color: #f4f4f4;
    }

    #form1 {
        width: 90%;
        max-width: 1200px;
        margin: 20px auto;
        padding: 30px;
        background-color: #fff;
        border-radius: 8px;
        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
    }

    h2 {
        padding: 0;
        margin: 0;
        box-sizing: border-box;
        margin-bottom: 20px;
        color: #333;
        font-family: 'Sans Serif Collection';
        font-weight: bold;
        font-size: 20px;
        color: #585555;
    }

    .bodydiv {
        width: 100%;
        padding: 0;
        margin: 0;
        display: flex;
        flex-wrap: wrap;
        gap: 20px;
    }

    .SaleSummury, .ProfitSummry, .LossSummry {
        display: flex;
        flex-direction: column;
        gap: 20px;
        flex: 1 1 calc(30% - 20px); /* Three items per row with gap adjustment */
        padding: 10px;
        align-items: center;
        text-align: center;
    }

    .TotalSalesDiv, .TodaySalesDiv, .TotalProfitDiv, .TodayProfitDiv, .TotalLossDiv, .TodayLossDiv {
        width: 100%;
        max-width: 280px; /* Default size for larger screens */
        height: 260px; /* Adjust height to content */
        margin: 0;
        padding: 0;
        box-sizing: border-box;
        border: 1px solid #ced4da;
        border-radius: 8px;
        padding: 20px;
        text-align: left;
        margin-top: 40px;
        font-size: 13px;
    }

    .TotalSalesDiv {
        background-color: #a08227;
    }

    .TodaySalesDiv {
        background-color: #f18f32;
    }

    .TotalProfitDiv {
        background-color: #428f2c;
    }

    .TodayProfitDiv {
        background-color: #4ab14c;
    }

    .TotalLossDiv {
        background-color: #bf1f1f;
    }

    .TodayLossDiv {
        background-color: #ad3e3e;
    }

    .amount {
        font-size: 1.5em;
        font-weight: bold;
        margin-top: 10px;
    }

    h3 + .amount {
        margin-top: 5px;
    }

    .SalesChartDiv {
        width: 100%;
        max-width: 1000px;
        margin: 30px auto;
        background-color: #fff;
        border: 1px solid #ced4da;
        border-radius: 5px;
        padding: 20px;
    }

    .SalesChartDiv h3 {
        text-align: center;
        margin-bottom: 20px;
        color: #333;
    }

    /* GridView Styles */
    .gridview-container {
        margin-top: 40px; /* Space from the top */
        padding: 10px;
        background-color: #fff;
        border: 1px solid #ced4da;
        border-radius: 8px;
        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
    }

    .gridview-container table {
        width: 100%;
        border-collapse: collapse;
    }

    .gridview-container th, .gridview-container td {
        border: 1px solid #ddd;
        padding: 8px;
        text-align: left;
     }

    .gridview-container th {
        background-color: #484672;
        color: #fff;
    }

    .gridview-container td {
            color: #bf1f1f;
            font-weight: bold;
     }

    .gridview-container tr:nth-child(even) {
        background-color: #f9f9f9;
    }

    .gridview-container tr:hover {
        background-color: #f1f1f1;
    }

    /* Responsive Styles */
    @media (max-width: 768px) {
        .bodydiv {
            width: 100%;
            padding: 0;
            margin: 0;
            flex-direction: column;
            align-items: center;
        }

        .SaleSummury, .ProfitSummry, .LossSummury {
            flex: 1 1 100%; /* Full width on small screens */
            max-width: 100%; /* Ensure full width */
        }

        .TotalSalesDiv, .TodaySalesDiv, .TotalProfitDiv, .TodayProfitDiv, .TotalLossDiv, .TodayLossDiv {
            max-width: 100%; /* Full width for each section */
            padding: 15px; /* Reduced padding */
            margin-top: 10px; /* Reduced margin */
            /* Adjust height to content, no fixed height */
        }
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="bodydiv">
            <div class="SaleSummury">
                <h2>Sales & Purchase Summary</h2>
                <div class="TotalSalesDiv">
                    <h3><i class="fas fa-chart-line"></i>Total Sales</h3>
                    <div class="amount"><asp:Label ID="Label1" runat="server" Text="₹0"></asp:Label></div>
                    <h3><i class="fas fa-shopping-cart"></i>Total Purchases</h3>
                    <div class="amount"><asp:Label ID="Label7" runat="server" Text="₹0"></asp:Label></div>
                </div>
                <div class="TodaySalesDiv">
                    <h3><i class="fas fa-calendar-day"></i>Today Sales (Date: <asp:Label ID="Label11" runat="server" Text="₹0"></asp:Label>)</h3>
                    <div class="amount"><asp:Label ID="Label4" runat="server" Text="₹0"></asp:Label></div>
                    <h3><i class="fas fa-cart-plus"></i>Today Purchases</h3>
                    <div class="amount"><asp:Label ID="Label9" runat="server" Text="₹0"></asp:Label></div>
                </div>
            </div>

            <div class="ProfitSummry">
                <h2>Profit Summary</h2>
                <div class="TotalProfitDiv">
                    <h3><i class="fas fa-dollar-sign"></i>Total Profit</h3>
                    <div class="amount"><asp:Label ID="Label2" runat="server" Text="₹0"></asp:Label></div>
                    <h3><i class="fas fa-money-bill-wave"></i>Total Balance Due</h3>
                    <div class="amount"><asp:Label ID="Label3" runat="server" Text="₹0"></asp:Label></div>
                </div>
                <div class="TodayProfitDiv">
                    <h3><i class="fas fa-coins"></i>Today Profit</h3>
                    <div class="amount"><asp:Label ID="Label5" runat="server" Text="₹0"></asp:Label></div>
                    <h3><i class="fas fa-balance-scale"></i>Today Balance Due</h3>
                    <div class="amount"><asp:Label ID="Label6" runat="server" Text="₹0"></asp:Label></div>
                </div>
            </div>

            <div class="LossSummry">
                <h2>Loss Summary</h2>
                <div class="TotalLossDiv">
                    <h3><i class="fas fa-chart-bar"></i>Total Loss</h3>
                    <div class="amount"><asp:Label ID="Label8" runat="server" Text="₹0"></asp:Label></div>
                </div>
                <div class="TodayLossDiv">
                    <h3><i class="fas fa-calendar-day"></i>Today Loss</h3>
                    <div class="amount"><asp:Label ID="Label10" runat="server" Text="₹0"></asp:Label></div>
                </div>
            </div>
        </div>
        <div class="gridview-container">
            <h1>Stock Details</h1>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
                <Columns>
                    <asp:BoundField DataField="SNO" HeaderText="SNO" SortExpression="SNO" />
                    <asp:BoundField DataField="ProductName" HeaderText="ProductName" SortExpression="ProductName" />
                    <asp:BoundField DataField="HSNCode" HeaderText="HSNCode" SortExpression="HSNCode" />
                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" />
                    <asp:BoundField DataField="Unit" HeaderText="Unit" SortExpression="Unit" />
                    <asp:BoundField DataField="Rate" HeaderText="Rate" SortExpression="Rate" />
                    <asp:BoundField DataField="Purchase_price" HeaderText="Purchase_price" SortExpression="Purchase_price" />
                    <asp:BoundField DataField="Total" HeaderText="Total" SortExpression="Total" />
                    <asp:BoundField DataField="Dealer_Name" HeaderText="Dealer_Name" SortExpression="Dealer_Name" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Product] WHERE Quantity <= 6"></asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
