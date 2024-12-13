using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Purchase : System.Web.UI.Page
{
    private string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\Retail.mdf;Integrated Security=True";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Populate the DropDownList with supplier names
                using (SqlCommand cmd = new SqlCommand("SELECT Name FROM Supplier", conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        t1.Items.Add(reader.GetString(0));
                    }
                    reader.Close();
                }

                // Populate the DropDownList with customer names
                using (SqlCommand cmd = new SqlCommand("SELECT ProductName FROM Product", conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        DropDownList1.Items.Add(reader.GetString(0));
                    }
                    reader.Close();
                }
            }

            BindGridView();
        }
    }

    protected void t1_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Get the selected customer name from the dropdown
        string selectedSupplierName = t1.SelectedItem.Text;

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();

            // Query to fetch customer details based on selected customer name
            string query = "SELECT Name, Address, Phone, GSTIN FROM Supplier WHERE Name = @Name";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Name", selectedSupplierName);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Populate the textboxes with the fetched data
                        t8.Text = reader["Name"].ToString();
                        t2.Text = reader["Address"].ToString();
                        t3.Text = reader["Phone"].ToString();
                        t4.Text = reader["GSTIN"].ToString();
                    }
                    else
                    {
                        // Clear fields if no data is found
                        t8.Text = "";
                        t2.Text = "";
                        t3.Text = "";
                        t4.Text = "";
                        
                    }
                }
            }
        }
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Get the selected customer name from the dropdown
        string selectedProductName = DropDownList1.SelectedItem.Text;

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();

            // Query to fetch customer details based on selected customer name
            string query = "SELECT  ProductName, HSNCode, Unit, Rate FROM Product WHERE ProductName = @ProductName";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ProductName", selectedProductName);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Populate the textboxes with the fetched data
                        TextBox1.Text = reader["ProductName"].ToString();
                        TextBox2.Text = reader["HSNCode"].ToString();
                        TextBox4.Text = reader["Unit"].ToString();
                        TextBox5.Text = reader["Rate"].ToString();
                    }
                    else
                    {
                        // Clear fields if no data is found
                        TextBox1.Text = "";
                        TextBox2.Text = "";
                        TextBox4.Text = "";
                        TextBox5.Text = "";
                    }
                }
            }
        }
    }

    private DataTable ItemsTable
    {
        get
        {
            if (Session["ItemsTable"] == null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("SNo", typeof(int));
                dt.Columns.Add("ProductName", typeof(string));
                dt.Columns.Add("HSNCode", typeof(string));
                dt.Columns.Add("Quantity", typeof(int));
                dt.Columns.Add("Unit", typeof(string));
                dt.Columns.Add("Rate", typeof(decimal));
                dt.Columns.Add("Discount", typeof(decimal));
                dt.Columns.Add("Amount", typeof(decimal));
                dt.Columns.Add("GST", typeof(decimal));
                dt.Columns.Add("Total", typeof(decimal));
                Session["ItemsTable"] = dt;
            }
            return (DataTable)Session["ItemsTable"];
        }
    }

    private void BindGridView()
    {
        GridView1.DataSource = ItemsTable;
        GridView1.DataBind();
    }

    

    protected void Button1_Click(object sender, EventArgs e)
    {
        DataTable dt = ItemsTable;
        DataRow newRow = dt.NewRow();

        // Get values from the textboxes
        string productName = TextBox1.Text;
        string hsnCode = TextBox2.Text;
        int quantity = int.TryParse(TextBox3.Text, out quantity) ? quantity : 0;
        string unit = TextBox4.Text;
        decimal rate = decimal.TryParse(TextBox5.Text, out rate) ? rate : 0;
        decimal discount = decimal.TryParse(TextBox6.Text, out discount) ? discount : 0;

        // Calculate amount
        decimal amount = (quantity * rate) - ((discount / 100) * (rate * quantity));

        decimal netAmount = amount;

        // Calculate GST and total
        decimal gst = netAmount * 0.18m; // 18% GST
        decimal total = netAmount + gst;

        // Get the next serial number
        int serialNumber = dt.Rows.Count + 1;

        // Populate the new row
        newRow["SNo"] = serialNumber;
        newRow["ProductName"] = productName;
        newRow["HSNCode"] = hsnCode;
        newRow["Quantity"] = quantity;
        newRow["Unit"] = unit;
        newRow["Rate"] = rate;
        newRow["Discount"] = discount;
        newRow["Amount"] = amount;
        newRow["GST"] = gst;
        newRow["Total"] = total;

        // Add the row to the DataTable
        dt.Rows.Add(newRow);

        // Rebind the GridView
        BindGridView();

        // Calculate and update the total amount
        decimal totalAmount = dt.AsEnumerable().Sum(row => row.Field<decimal>("Total"));
        TextBox7.Text = totalAmount.ToString("0.00");

        // Clear input fields
        TextBox1.Text = "";
        TextBox2.Text = "";
        TextBox3.Text = "";
        TextBox4.Text = "";
        TextBox5.Text = "";
        TextBox6.Text = "";
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        // Clear all input fields and GridView
        TextBox1.Text = "";
        TextBox2.Text = "";
        TextBox3.Text = "";
        TextBox4.Text = "";
        TextBox5.Text = "";
        TextBox6.Text = "";
        TextBox7.Text = "";
        TextBox8.Text = "";
        TextBox9.Text = "";
        t2.Text = "";
        t3.Text = "";
        t4.Text = "";
        t5.Text = "";
        t7.Text = "";
        t8.Text = "";


        // Clear the DataTable
        Session["ItemsTable"] = null;
        GridView1.DataSource = null;
        GridView1.DataBind();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();

            // Start a transaction
            SqlTransaction transaction = conn.BeginTransaction();

            try
            {
                // Calculate totals
                decimal totalAmount = ItemsTable.AsEnumerable().Sum(row => row.Field<decimal>("Total"));
                decimal balanceDue;
                if (!decimal.TryParse(TextBox9.Text, out balanceDue))
                {
                    balanceDue = 0; // Default value if parsing fails
                }
                int billNumber = int.Parse(t5.Text); // Assuming t5.Text contains a valid integer

                // Prepare and execute the invoice details query
                string invoiceQuery = "INSERT INTO Purchase (PartyName, PartyAddress, PhoneNumber, GSTIN, BillNumber, BillDate, TotalAmount, BalanceDue) " +
                                      "VALUES (@PartyName, @PartyAddress, @PhoneNumber, @GSTIN, @BillNumber, @BillDate, @TotalAmount, @BalanceDue)";

                using (SqlCommand cmd = new SqlCommand(invoiceQuery, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@PartyName", t8.Text);
                    cmd.Parameters.AddWithValue("@PartyAddress", t2.Text);
                    cmd.Parameters.AddWithValue("@PhoneNumber", t3.Text);
                    cmd.Parameters.AddWithValue("@GSTIN", t4.Text);
                    cmd.Parameters.AddWithValue("@BillNumber", billNumber);
                    cmd.Parameters.AddWithValue("@BillDate", t7.Text);
                    cmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                    cmd.Parameters.AddWithValue("@BalanceDue", balanceDue);

                    cmd.ExecuteNonQuery();
                }

                // Prepare and execute the items details query
                foreach (DataRow row in ItemsTable.Rows)
                {
                    string productName = row["ProductName"].ToString();
                    string HSNCode = row["HSNCode"].ToString();
                    int quantity = Convert.ToInt32(row["Quantity"]);
                    string unit = row["Unit"].ToString();
                    decimal rate = Convert.ToDecimal(row["Rate"]);                   
                    decimal totalValue = Convert.ToDecimal(row["Total"]);
                    decimal purchaseValue = totalValue / quantity;
                    decimal purchasetotal = quantity * purchaseValue;
                    string Dealer = row["Dealer_Name"].ToString();

                    
                    // Check if the product already exists
                    string checkProductQuery = "SELECT COUNT(*) FROM Product WHERE ProductName = @ProductName";
                    using (SqlCommand cmdCheck = new SqlCommand(checkProductQuery, conn, transaction))
                    {
                        cmdCheck.Parameters.AddWithValue("@ProductName", productName);
                        int productCount = (int)cmdCheck.ExecuteScalar();

                        if (productCount > 0)
                        {
                            // Product exists, update the quantity and purchase price
                            string updateProductQuery = @"
                            UPDATE Product 
                            SET HSNCode = @HSNCode,
                                Quantity = Quantity + @Quantity,
                                Unit = @Unit, 
                                Rate = @Rate,                                 
                                Purchase_price = @Purchase_price,
                                Total = (Quantity + @Quantity) * @Purchase_price,
                                Dealer_Name =
                            WHERE ProductName = @ProductName";

                            using (SqlCommand cmdUpdate = new SqlCommand(updateProductQuery, conn, transaction))
                            {
                                cmdUpdate.Parameters.AddWithValue("@HSNCode", HSNCode);
                                cmdUpdate.Parameters.AddWithValue("@Quantity", quantity);
                                cmdUpdate.Parameters.AddWithValue("@Unit", unit);
                                cmdUpdate.Parameters.AddWithValue("@Rate", rate);                                
                                cmdUpdate.Parameters.AddWithValue("@Purchase_price", purchaseValue);
                                cmdUpdate.Parameters.AddWithValue("@total", purchasetotal);
                                cmdUpdate.Parameters.AddWithValue("@ProductName", productName);

                                cmdUpdate.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            // Product doesn't exist, insert a new record
                            string insertProductQuery = "INSERT INTO Product (ProductName,HSNCode, Quantity, Unit, Rate, Purchase_price, Total) " +
                                                        "VALUES (@ProductName,@HSNCode, @Quantity, @Unit, @Rate, @PurchasePrice, @Total)";

                            using (SqlCommand cmdInsert = new SqlCommand(insertProductQuery, conn, transaction))
                            {
                                cmdInsert.Parameters.AddWithValue("@ProductName", productName);
                                cmdInsert.Parameters.AddWithValue("@HSNCode", HSNCode);
                                cmdInsert.Parameters.AddWithValue("@Quantity", quantity);
                                cmdInsert.Parameters.AddWithValue("@Unit", unit);
                                cmdInsert.Parameters.AddWithValue("@Rate", rate);
                                cmdInsert.Parameters.AddWithValue("@PurchasePrice", purchaseValue); // Initially set PurchasePrice divided by total quantity
                                cmdInsert.Parameters.AddWithValue("@Total", totalValue);
                                cmdInsert.ExecuteNonQuery();
                            }
                        }
                    }

                    // Insert the purchase item details
                    string itemsQuery = "INSERT INTO PurchaseItems (BillNumber,BillDate, SNo, ProductName, HSNCode, Quantity, Unit, Rate, Discount, Amount, GST, Total) " +
                                        "VALUES (@BillNumber,@BillDate, @SNo, @ProductName, @HSNCode, @Quantity, @Unit, @Rate, @Discount, @Amount, @GST, @Total)";

                    using (SqlCommand cmd = new SqlCommand(itemsQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@BillNumber", int.Parse(t5.Text)); // Ensure this is an integer
                        cmd.Parameters.AddWithValue("@BillDate", t7.Text);
                        cmd.Parameters.AddWithValue("@SNo", row["SNo"]);
                        cmd.Parameters.AddWithValue("@ProductName", row["ProductName"]);
                        cmd.Parameters.AddWithValue("@HSNCode", row["HSNCode"]);
                        cmd.Parameters.AddWithValue("@Quantity", row["Quantity"]);
                        cmd.Parameters.AddWithValue("@Unit", row["Unit"]);
                        cmd.Parameters.AddWithValue("@Rate", row["Rate"]);
                        cmd.Parameters.AddWithValue("@Discount", row["Discount"]);
                        cmd.Parameters.AddWithValue("@Amount", row["Amount"]);
                        cmd.Parameters.AddWithValue("@GST", row["GST"]);
                        cmd.Parameters.AddWithValue("@Total", row["Total"]);

                        cmd.ExecuteNonQuery();
                    }
                }

                // Commit the transaction
                transaction.Commit();

                Response.Write("<script>alert('Purchase saved successfully.')</script>");
            }
            catch (Exception ex)
            {
                // Rollback the transaction if an error occurs
                transaction.Rollback();

                // Log the error
                Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
            }
            finally
            {
                // Clear the form
                Button3_Click(sender, e);
            }
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteRow")
        {
            // Delete the selected row
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            DataTable dt = ItemsTable;
            if (rowIndex >= 0 && rowIndex < dt.Rows.Count)
            {
                dt.Rows.RemoveAt(rowIndex);
                BindGridView();
            }
        }
        else if (e.CommandName == "EditRow")
        {
            // Edit the selected row
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            DataTable dt = ItemsTable;
            if (rowIndex >= 0 && rowIndex < dt.Rows.Count)
            {
                DataRow row = dt.Rows[rowIndex];

                // Populate fields with selected row's values
                TextBox1.Text = row["ProductName"].ToString();
                TextBox2.Text = row["HSNCode"].ToString();
                TextBox3.Text = row["Quantity"].ToString();
                TextBox4.Text = row["Unit"].ToString();
                TextBox5.Text = row["Rate"].ToString();
                TextBox6.Text = row["Discount"].ToString();

                // Remove the row from the DataTable to allow re-addition
                dt.Rows.RemoveAt(rowIndex);
                BindGridView();
            }
        }
    }

}