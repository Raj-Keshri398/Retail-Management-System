using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PurchaseTransaction : System.Web.UI.Page
{
    private string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\Retail.mdf;Integrated Security=True";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Retrieve the PhoneNumber from the query string
            string phoneNumber = Request.QueryString["PhoneNumber"];

            if (!string.IsNullOrEmpty(phoneNumber))
            {
                // Filter the SqlDataSource to show only the Purchasetransactions related to the passed PhoneNumber
                SqlDataSource1.SelectCommand = "SELECT * FROM Purchase WHERE PhoneNumber = @PhoneNumber";
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("PhoneNumber", phoneNumber);

                // Update the statistics for the selected Supplier
                UpdateStats(phoneNumber);
            }
            else
            {
                // If no PhoneNumber is passed, show statistics for all supplier
                UpdateStats(null);
            }
        }
    }
    private void UpdateStats(string phoneNumber)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();

            // SQL query to calculate total sales and balance due
            string query = phoneNumber == null
                ? "SELECT SUM(CONVERT(DECIMAL, TotalAmount)), SUM(CONVERT(DECIMAL, BalanceDue)) FROM Purchase"
                : "SELECT SUM(CONVERT(DECIMAL, TotalAmount)), SUM(CONVERT(DECIMAL, BalanceDue)) FROM Purchase WHERE PhoneNumber = @PhoneNumber";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                if (phoneNumber != null)
                {
                    cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                }

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Update the labels with the calculated values
                        lblTotalPurchase.Text = "₹" + (reader.IsDBNull(0) ? "0" : Convert.ToDecimal(reader[0]).ToString("N2"));
                        lblBalanceDue.Text = "₹" + (reader.IsDBNull(1) ? "0" : Convert.ToDecimal(reader[1]).ToString("N2"));
                    }
                }
            }
        }
    }

    protected void btnAddSales_Click(object sender, EventArgs e)
    {
        Response.Redirect("Purchase.aspx");
    }

    protected void btnAction_Click(object sender, EventArgs e)
    {
        // Get the button that was clicked
        Button button = (Button)sender;

        // Get the BillNumber from the CommandArgument
        string billNumber = button.CommandArgument;

        // Redirect to CustomerBillDetails.aspx and pass the BillNumber
        Response.Redirect("SupplierBillDetails.aspx?BillNumber=" + billNumber);
    }

    protected void Button2_Click1(object sender, EventArgs e)
    {
        // Retrieve and trim the input from TextBox1
        string searchTerm = TextBox1.Text.Trim();

        // Check if the search term is either a phone number or bill number
        if (!string.IsNullOrEmpty(searchTerm))
        {
            // Assuming that we want to search by either PhoneNumber or BillNumber
            SqlDataSource1.SelectCommand = "SELECT * FROM Purchase WHERE PhoneNumber = @SearchTerm OR BillNumber = @SearchTerm";
            SqlDataSource1.SelectParameters.Clear();
            SqlDataSource1.SelectParameters.Add("SearchTerm", searchTerm);
            GridView1.DataSourceID = "SqlDataSource1";
        }
        else
        {
            // Handle case where search term is empty
            SqlDataSource1.SelectCommand = "SELECT * FROM Purchase";
            GridView1.DataSourceID = "SqlDataSource1"; // Reset to show all records
        }
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        // Refresh the GridView
        SqlDataSource1.SelectCommand = "SELECT * FROM Purchase";
        GridView1.DataSourceID = "SqlDataSource1";
        TextBox1.Text = "";
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        // To delete the record
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand("DELETE FROM Purchase WHERE BillNumber = @BillNumber", conn))
            {
                cmd.Parameters.AddWithValue("@BillNumber", TextBox1.Text);
                cmd.ExecuteNonQuery();
            }
        }

        Response.Write("<script>alert('Record Deleted')</script>");

        // Refresh the GridView
        RefreshGridView();
    }

    private void RefreshGridView()
    {
        SqlDataSource1.SelectCommand = "SELECT * FROM Purchase";
        GridView1.DataSourceID = "SqlDataSource1";
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        // Redirect to the previous page or default to Transaction.aspx
        if (Session["PreviousPage"] != null)
        {
            Response.Redirect(Session["PreviousPage"].ToString());
        }

    }
}