<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>



<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Dashboard</title>
    <link rel="stylesheet" type="text/css" href="Style.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css">
</head>
<body>
    <form id="form1" runat="server">
        <div class="main-container">
            <div class="sidebar">
                <div class="sidebar-header">
                    <h2><i class="fas fa-tools"></i> Logo</h2>
                </div>
                    
                <ul>
                    <li><a href="FrontDashboard.aspx" target="main-content"><i class="fas fa-tachometer-alt"></i> Dashboard</a></li>
                    <h4>PARTIES</h4>
                    <li><a href="Customers.aspx" target="main-content"><i class="fas fa-user"></i> Customer</a></li>
                    <li><a href="Supplier.aspx" target="main-content"><i class="fas fa-user"></i> Supplier</a></li>
                    <h4>MANAGE INVENTORY</h4>
                    <li><a href="PurchaseTransaction.aspx" target="main-content"><i class="fas fa-box"></i>Purchase Transaction</a></li>
                    <li><a href="Transaction.aspx" target="main-content"><i class="fas fa-chart-line"></i>Sales Transaction</a></li>
                    <h4>BILLS</h4>
                    <li><a href="Sales.aspx" target="main-content"><i class="fas fa-shopping-cart"></i>Sales</a></li>
                    <li><a href="Purchase.aspx" target="main-content"><i class="fas fa-chart-line"></i>Purchase</a></li>
                    <li><a href="Products.aspx" target="main-content"><i class="fas fa-truck"></i> Stocks</a></li>
                    <li><a href="Payment.aspx" target="main-content"><i class="fas fa-credit-card"></i> Payment</a></li>
                    <li><a href="Stores.aspx" target="main-content"><i class="fas fa-store"></i> Stores</a></li>
               </ul>
                <button onclick="close()" class="logout"><i class="fas fa-sign-out-alt"></i> Logout</button>
                
                
            </div>
            <div class="main-content">
                <div class="header">
                    <h1>Retail Shop Management System</h1>
                </div>
                <iframe name="main-content" frameborder="0"></iframe>
            </div>
        </div>
    </form>
</body>
    <script>
        function close() {
            window.close();
        }
    </script>
</html>
