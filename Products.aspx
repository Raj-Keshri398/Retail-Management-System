<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Products.aspx.cs" Inherits="Products" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Product Details</title>
    <link rel="stylesheet" type="text/css" href="Product.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css">
</head>
<body>
    <form id="form1" runat="server">
        <div class="customer-management">
            <h2>Products Management</h2>
                <div class="customers-list">
                    <h3>Product List</h3>
                    <div class="search-box">
                        <div class="left-section">
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="delete button" OnClick="Button3_Click"><i class="fas fa-trash-alt"></i></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton2" runat="server" CssClass="refresh button" OnClick="Button5_Click"><i class="fas fa-sync-alt"></i></asp:LinkButton>
                            <asp:Button class="add button" ID="btnAddProduct" runat="server" Text="Add Product" OnClientClick="openModal(); return false;"  />
                        </div>
                        <div class="right-section">
                            <asp:TextBox ID="TextBox1" runat="server" placeholder="Search by Name" CssClass="asp-textboxx" AutoPostBack="True"></asp:TextBox>
                            <asp:LinkButton ID="Button5" runat="server" CssClass="refresh button" OnClick="Button5_Click1" ><i class="fas fa-search"></i></asp:LinkButton>
                        </div>
                    </div>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource2" SelectionMode="Single" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" />
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
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Product]"></asp:SqlDataSource>
               </div>
         </div>

        <!-- Modal -->
        <div id="addProductModal" class="modal">
            <div class="modal-content">
                <span class="close" onclick="closeModal()">&times;</span>
                <h3>Add Product</h3>
                    <div class="form-row">
                        <label for="t1">Product Name:</label>
                        <asp:TextBox ID="t1" runat="server" CssClass="input" ></asp:TextBox>
                        <label for="t2">HSN Code:</label>
                        <asp:TextBox ID="t2" runat="server" CssClass="input"></asp:TextBox>
                        <label for="t3">Quantity:</label>
                        <asp:TextBox ID="t3" runat="server" CssClass="input" list="itemList" />
                        <label for="t4">Unit:</label>
                        <asp:TextBox ID="t4" runat="server" CssClass="input"></asp:TextBox>
                        <label for="t5">Rate:</label>
                        <asp:TextBox ID="t5" runat="server" CssClass="input"></asp:TextBox> 
                        <label for="t6">Purchase Price:</label>
                        <asp:TextBox ID="t6" runat="server" CssClass="input"></asp:TextBox>                      
                        <label for="t7">Delear Name:</label>
                        <asp:TextBox ID="t7" runat="server" CssClass="input"></asp:TextBox>                                           
                        <asp:CheckBox ID="CheckBox2" runat="server" CssClass="toggle-input" AutoPostBack="true" />
                        <span class="slider"></span>
                    </div>
                    <div class="buttons">
                        <asp:Button class="edit button" ID="Button1" runat="server" Text="Edit" OnClick="Button1_Click" />
                        <asp:Button class="add-item button" ID="Button2" runat="server" Text="Add Item" OnClick="Button2_Click" />
                        <asp:Button class="clear button" ID="Button4" runat="server" Text="Clear" OnClientClick="clearForm(); return false;" />
                    </div>               
            </div>
        </div>
    </form>

    <script>
        function openModal() {
            document.getElementById("addProductModal").style.display = "block";
        }

        function closeModal() {
            document.getElementById("addProductModal").style.display = "none";
        }

        // Close the modal when clicking outside of it
        window.onclick = function (event) {
            if (event.target == document.getElementById("addProductModal")) {
                closeModal();
            }
        }

        
        // Handle Clear button click
        function clearForm() {
            document.getElementById('<%= t1.ClientID %>').value = '';
            document.getElementById('<%= t2.ClientID %>').value = '';
            document.getElementById('<%= t3.ClientID %>').value = '';
            document.getElementById('<%= t4.ClientID %>').value = '';
            document.getElementById('<%= t5.ClientID %>').value = '';
            document.getElementById('<%= t6.ClientID %>').value = '';
            document.getElementById('<%= t7.ClientID %>').value = '';
    }
</script>


</body>
</html>
