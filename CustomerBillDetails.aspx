<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerBillDetails.aspx.cs" Inherits="CustomerBillDetails" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Customer Bill Details</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css">
    <style>
        /* General body styles */
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            background-color: #f4f4f4;
        }

        /* Form container styling */
        form {
            display: flex;
            flex-direction: column;
            align-items: center;
            margin-top: 20px;
        }

        /* Bill details section */
        .bill-details {
            background-color: #fff;
            padding: 30px;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            width: 80%;
            box-sizing: border-box;
        }

        .bill-details h2 {
            margin: 0;
            padding: 0;
            text-align: center;
            color: #335968;
        }

        /* Bill number and date styling */
        .bill-info {
            display: flex;
            justify-content: space-between;
            margin-bottom: 20px;
        }

        .bill-info span {
            font-weight: bold;
        }

        /* Table styling */
        table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }

        th, td {
            padding: 10px;
            border: 1px solid #ddd;       
        }

        th {
            background-color: #1e4b5e;
            color: #fff;
            text-align: center;
            font-weight: bold;
        }

        td {
            text-align: left;
            font-size: 13px;
        }

        tr:nth-child(even) {
            background-color: #f9f9f9;
        }

        tr:hover {
            background-color: #f1f1f1;
        }

        /* Button styling */
        .button {
            padding: 10px 20px;
            border: none;
            cursor: pointer;
            font-size: 16px;
            border-radius: 4px;
            background-color: #42c3ab;
            color: #fff;
            margin-top: 20px;
        }

        .button:hover {
            opacity: 0.8;
        }

        /* Font Awesome button styling */
        .fa-button {
            display: inline-block;
            padding: 5px 10px;
            border-radius: 4px;
            background-color: #42c3ab;
            color: #fff;
            text-align: center;
            text-decoration: none;
            font-size: 14px;
            margin: 0 5px;
            cursor: pointer;
        }

        .fa-button:hover {
            opacity: 0.8;
        }

        .fa-button i {
            margin-right: 5px;
        }

        /* Specific button colors */
        .fa-button.delete {
            background-color: #e74c3c;
        }

        .fa-button.edit {
            background-color: #f39c12;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="bill-details">
            <h2>Bill Details</h2>
            <div class="bill-info">
                <asp:Label ID="lblBillNumber" runat="server" />
                <asp:Label ID="lblBillDate" runat="server" />
            </div>

            <asp:PlaceHolder ID="phBillDetails" runat="server">
               <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource2"
              OnRowDeleting="GridView2_RowDeleting" DataKeyNames="ProductName">
                    <Columns>
                        <asp:BoundField DataField="SNo" HeaderText="SNo" SortExpression="SNo" />
                        <asp:BoundField DataField="ProductName" HeaderText="ProductName" SortExpression="ProductName" />
                        <asp:BoundField DataField="HSNCode" HeaderText="HSNCode" SortExpression="HSNCode" />
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" />
                        <asp:BoundField DataField="Unit" HeaderText="Unit" SortExpression="Unit" />
                        <asp:BoundField DataField="Rate" HeaderText="Rate" SortExpression="Rate" />
                        <asp:BoundField DataField="Discount" HeaderText="Discount" SortExpression="Discount" />
                        <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" />
                        <asp:BoundField DataField="GST" HeaderText="GST" SortExpression="GST" />
                        <asp:BoundField DataField="Total" HeaderText="Total" SortExpression="Total" />
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ProductName") %>' CssClass="fa-button delete" />
                                <a href="#" class="fa-button edit">
                                    <i class="fas fa-edit"></i>Edit
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </asp:PlaceHolder>

            <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                SelectCommand="SELECT * FROM [Items] WHERE [BillNumber] = @BillNumber"
                DeleteCommand="DELETE FROM [Items] WHERE [ProductName] = @ProductName">
                <SelectParameters>
                    <asp:QueryStringParameter Name="BillNumber" QueryStringField="BillNumber" Type="String" />
                </SelectParameters>
                <DeleteParameters>
                    <asp:Parameter Name="ProductName" Type="String" />
                </DeleteParameters>
            </asp:SqlDataSource>


            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="button" OnClick="btnBack_Click" />
            <asp:Button ID="Button1" runat="server" Text="Print" CssClass="button" OnClick="printBill_Click"/>

        </div>
    </form>
</body>
</html>
