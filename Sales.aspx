<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sales.aspx.cs" Inherits="Sales" %>

<!DOCTYPE html>
<html>
<head>
    <title>Sales Form</title>
    <link href="Sales.css" rel="stylesheet" />
    <script type="text/javascript">
        function calculateBalance() {
            var totalAmount = parseFloat(document.getElementById('<%= TextBox7.ClientID %>').value) || 0;
            var paidAmount = parseFloat(document.getElementById('<%= TextBox8.ClientID %>').value) || 0;
            var balanceDue = totalAmount - paidAmount;
            document.getElementById('<%= TextBox9.ClientID %>').value = balanceDue.toFixed(2);
        }

        function attachEventHandlers() {
            var totalAmountTextbox = document.getElementById('<%= TextBox7.ClientID %>');
            var paidAmountTextbox = document.getElementById('<%= TextBox8.ClientID %>');

            totalAmountTextbox.onchange = calculateBalance;
            paidAmountTextbox.onchange = calculateBalance;
        }

        window.onload = attachEventHandlers;
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="header">
                <h2>Create Sale</h2>
            </div>

            <div class="content">
                
               <div class="section">
                    <h3>Party Details</h3>
                    <div class="form-row">
                        <label for="t1">Select Party Name:</label>
                        <asp:DropDownList ID="t1" runat="server" CssClass="input" OnSelectedIndexChanged="t1_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem Value="Select Party Name" Text="Select Party Name"></asp:ListItem>                            
                        </asp:DropDownList>
                    </div>
                    <div class="form-row">
                        <label for="t8">Party Name:</label>
                        <asp:TextBox ID="t8" runat="server" CssClass="input"></asp:TextBox>
                    </div>
                    <div class="form-row">
                        <label for="t2">Party Address (optional):</label>
                        <asp:TextBox ID="t2" runat="server" CssClass="input"></asp:TextBox>
                    </div>
                    <div class="form-row">
                        <label for="t3">Phone Number:</label>
                        <asp:TextBox ID="t3" runat="server" CssClass="input"></asp:TextBox>
                    </div>
                    <div class="form-row">
                        <label for="t4">GSTIN (optional):</label>
                        <asp:TextBox ID="t4" runat="server" CssClass="input"></asp:TextBox>
                    </div>
                </div>

                <div class="section">
                    <h3>Invoice Details</h3>
                    <div class="form-row">
                        <label for="t5">Bill Number:</label>
                        <asp:TextBox ID="t5" runat="server" CssClass="input"></asp:TextBox>
                    </div>
                    <div class="form-row">
                        <label for="t6">Your GSTIN:</label>
                        <asp:TextBox ID="t6" runat="server" CssClass="input"></asp:TextBox>
                    </div>
                    <div class="form-row">
                        <label for="t7">Bill Date:</label>
                        <asp:TextBox ID="t7" runat="server" CssClass="input" TextMode="Date"></asp:TextBox>
                    </div>
                    <div class="form-row">
                        <label for="d1">State of Supply:</label>
                        <asp:DropDownList ID="d1" runat="server" CssClass="input">
                            <asp:ListItem Value="Karnataka" Text="Karnataka"></asp:ListItem>                           
                        </asp:DropDownList>
                    </div>
                </div>

            </div>

            
            <div class="table-section">
                <h3>Items on the Invoice</h3>
                <div class="bill-item">
                    <div class="form-rows">
                        <div class="form-item">
                            <label for="DropDownList1">Select Product Name:</label>
                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="input" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
                                <asp:ListItem Value="Select Product Name" Text="Select Product Name"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-item">
                            <label for="TextBox1">Product Name:</label>
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="input"></asp:TextBox>
                        </div>
                        <div class="form-item">
                            <label for="TextBox2">HSN Code:</label>
                            <asp:TextBox ID="TextBox2" runat="server" CssClass="input1"></asp:TextBox>
                        </div>
                        <div class="form-item">
                            <label for="TextBox3">Quantity:</label>
                            <asp:TextBox ID="TextBox3" runat="server" CssClass="input1"></asp:TextBox>
                        </div>
                        <div class="form-item">
                            <label for="TextBox4">Unit:</label>
                            <asp:TextBox ID="TextBox4" runat="server" CssClass="input1"></asp:TextBox>
                        </div>
                        <div class="form-item">
                            <label for="TextBox5">Rate:</label>
                            <asp:TextBox ID="TextBox5" runat="server" CssClass="input1"></asp:TextBox>
                        </div>
                        <div class="form-item">
                            <label for="TextBox6">Discount:</label>
                            <asp:TextBox ID="TextBox6" runat="server" CssClass="input1"></asp:TextBox>
                        </div>
                    </div>
                    <asp:Button ID="Button1" runat="server" Text="+ Add Item" CssClass="button add" OnClick="Button1_Click" />
                    <asp:Button ID="Button3" runat="server" Text="Clear" CssClass="button" OnClick="Button3_Click" />
                </div>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" OnRowCommand="GridView1_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="SNo" HeaderText="S.No" />
                        <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                        <asp:BoundField DataField="HSNCode" HeaderText="HSN Code" />
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                        <asp:BoundField DataField="Unit" HeaderText="Unit" />
                        <asp:BoundField DataField="Rate" HeaderText="Rate" />
                        <asp:BoundField DataField="Discount" HeaderText="Discount" />
                        <asp:BoundField DataField="Amount" HeaderText="Amount" />
                        <asp:BoundField DataField="GST" HeaderText="GST" />
                        <asp:BoundField DataField="Total" HeaderText="Total" />
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnDelete" runat="server" CommandName="DeleteRow" CommandArgument='<%# Container.DataItemIndex %>' CssClass="fa-button delete">
                                    <i class="fa fa-trash"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnEdit" runat="server" CommandName="EditRow" CommandArgument='<%# Container.DataItemIndex %>' CssClass="fa-button edit">
                                    <i class="fa fa-edit"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

            </div>

            
            <div class="amount-section">
                <h3>Total Amount</h3>
                <div class="form-row">
                    <label for="TextBox7">Total Amount:</label>
                    <asp:TextBox ID="TextBox7" runat="server" CssClass="input" Enabled="false"></asp:TextBox>
                    <label for="TextBox8">Paid Amount:</label>
                    <asp:TextBox ID="TextBox8" runat="server" CssClass="input"></asp:TextBox>
                    <label for="TextBox9">Balance Due:</label>
                    <asp:TextBox ID="TextBox9" runat="server" CssClass="input"></asp:TextBox>
                </div>
            </div>

            <!-- Footer Section -->
            <div class="footer">
                <asp:Button ID="Button2" runat="server" Text="Save Sale" CssClass="button save" OnClick="Button2_Click" />
            </div>
        </div>
    </form>
</body>
</html>
