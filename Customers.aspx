<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Customers.aspx.cs" Inherits="Customers" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Customer Details</title>
    <link rel="stylesheet" type="text/css" href="Style2.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css">
</head>
<body>
    <form id="form1" runat="server">
        <div class="customer-management">
            <h2>Add New Customers</h2>
               <div class="input-group">
                    <div class="form-row">
                        <label for="t1">Customer ID:</label>
                        <asp:TextBox ID="t1" runat="server" CssClass="input"></asp:TextBox>
                        <label for="t2">Customer Name:</label>
                        <asp:TextBox ID="t2" runat="server" CssClass="input"></asp:TextBox>
                        <label for="t3">Address:</label>
                        <asp:TextBox ID="t3" runat="server" CssClass="input"></asp:TextBox>
                        <label for="t4">Phone Number:</label>
                        <asp:TextBox ID="t4" runat="server" CssClass="input"></asp:TextBox>
                        <label for="t5">Email:</label>
                        <asp:TextBox ID="t5" runat="server" CssClass="input"></asp:TextBox>
                        <label for="t6">GSTIN:</label>
                        <asp:TextBox ID="t6" runat="server" CssClass="input"></asp:TextBox>   
                    </div>
                    
               </div>
                <div class="buttons">
                    <asp:Button class="edit button" ID="Button1" runat="server" Text="Edit" OnClick="Button1_Click"/>
                    <asp:Button class="add-item button" ID="Button2" runat="server" Text="Add Item" OnClick="Button2_Click"/>
                    <asp:Button class="clear button" ID="Button4" runat="server" Text="Clear" OnClick="Button4_Click"/>
                </div>
                <div class="customers-list">
                    <h3>Customers List</h3>
                    <div class="search-box">
                        <div class="left-section">
                             <asp:LinkButton ID="LinkButton1" runat="server" CssClass="delete button" OnClick="Button3_Click"><i class="fas fa-trash-alt"></i></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton2" runat="server" CssClass="refresh button" OnClick="Button5_Click"><i class="fas fa-sync-alt"></i></asp:LinkButton>
                        </div>
                        <div class="right-section">
                            <asp:TextBox ID="TextBox1" runat="server" placeholder="Search by ID" CssClass="asp-textboxx"></asp:TextBox>
                            <asp:Button class="refresh button" ID="Button5" runat="server" Text="Search Data" OnClick="Button5_Click1"  />
                        </div>
                    </div>
                     <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                    <Columns>
                          <asp:CommandField ShowSelectButton="True" />
                          <asp:BoundField DataField="id" HeaderText="id" SortExpression="id" />
                          <asp:BoundField DataField="PartyName" HeaderText="PartyName" SortExpression="PartyName" />
                          <asp:BoundField DataField="PartyAddress" HeaderText="PartyAddress" SortExpression="PartyAddress" />
                          <asp:BoundField DataField="PhoneNumber" HeaderText="PhoneNumber" SortExpression="PhoneNumber" />
                          <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                          <asp:BoundField DataField="GSTIN" HeaderText="GSTIN" SortExpression="GSTIN" />
                          <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:Button ID="btnAction" runat="server" Text="Sales Details" CssClass="button" CommandName="Action" OnClick="btnAction_Click" CommandArgument='<%# Eval("PhoneNumber") %>' />
                            </ItemTemplate>
                          </asp:TemplateField>
                      </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Customers]"></asp:SqlDataSource>
               </div>
        </div>
    </form>
</body>
</html>
