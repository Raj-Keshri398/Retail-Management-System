using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Products : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\Retail.mdf;Integrated Security=True");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = conn.CreateCommand();
        cmd.CommandType = CommandType.Text;

        // Determine the highest existing SNO
        cmd.CommandText = "SELECT ISNULL(MAX(SNO), 0) FROM Product";
        int highestSNO = (int)cmd.ExecuteScalar();
        int newSNO = highestSNO + 1;

        // Check if the product already exists
        cmd.CommandText = "SELECT * FROM Product WHERE ProductName = @ProductName";
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@ProductName", t1.Text);
        SqlDataReader da = cmd.ExecuteReader();

        int p = 0;
        while (da.Read())
        {
            if (da.GetString(0).Equals(t1.Text))
            {
                p = 1;
                break;
            }
        }
        da.Close();

        if (p == 0)
        {
            // Calculate Total as Quantity * Rate
            int quantity = int.Parse(t3.Text);
            float Purchase_price = float.Parse(t6.Text);
            float total = quantity * Purchase_price;

            // Insert the new record with the generated serial number and calculated total
            cmd.CommandText = "INSERT INTO Product (SNO, ProductName, HSNCode, Quantity, Unit, Rate, Purchase_price, Total, Dealer_Name) " +
                              "VALUES (@SNO, @ProductName, @HSNCode, @Quantity, @Unit, @Rate, @PurchasePrice, @Total, @DealerName)";
            cmd.Parameters.Clear(); // Clear parameters before adding new ones
            cmd.Parameters.AddWithValue("@SNO", newSNO);
            cmd.Parameters.AddWithValue("@ProductName", t1.Text);
            cmd.Parameters.AddWithValue("@HSNCode", t2.Text);
            cmd.Parameters.AddWithValue("@Quantity", quantity);
            cmd.Parameters.AddWithValue("@Unit", t4.Text);
            cmd.Parameters.AddWithValue("@Rate", t5.Text);
            cmd.Parameters.AddWithValue("@PurchasePrice", float.Parse(t6.Text));
            cmd.Parameters.AddWithValue("@Total", total);
            cmd.Parameters.AddWithValue("@DealerName", t7.Text);

            cmd.ExecuteNonQuery();
            Response.Write("<script>alert('Record Saved')</script>");

            // Refresh the GridView
            SqlDataSource2.SelectCommand = "SELECT * FROM Product";
            GridView1.DataSourceID = "SqlDataSource2";
        }
        else
        {
            Response.Write("<script>alert('Product already exists')</script>");
        }
    }



    protected void Button1_Click(object sender, EventArgs e)
    {
        // Calculate Total as Quantity * Rate
        int quantity = int.Parse(t3.Text);
        float Purchase_price = float.Parse(t6.Text);
        float total = quantity * Purchase_price;

        // Update the record with the calculated total
        using (SqlCommand cmd = conn.CreateCommand())
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE Product SET HSNCode = @HSNCode, Quantity = @Quantity, Unit = @Unit, Rate = @Rate, " +
                              "Purchase_price = @PurchasePrice, Total = @Total, Dealer_Name = @DealerName WHERE ProductName = @ProductName";
            cmd.Parameters.Clear(); // Clear parameters before adding new ones
            cmd.Parameters.AddWithValue("@ProductName", t1.Text);
            cmd.Parameters.AddWithValue("@HSNCode", t2.Text);
            cmd.Parameters.AddWithValue("@Quantity", quantity);
            cmd.Parameters.AddWithValue("@Unit", t4.Text);
            cmd.Parameters.AddWithValue("@Rate", t5.Text);
            cmd.Parameters.AddWithValue("@PurchasePrice", float.Parse(t6.Text));
            cmd.Parameters.AddWithValue("@Total", total);
            cmd.Parameters.AddWithValue("@DealerName", t7.Text);
            cmd.ExecuteNonQuery();
            Response.Write("<script>alert('Record Updated')</script>");

            // Refresh the GridView
            SqlDataSource2.SelectCommand = "SELECT * FROM Product";
            GridView1.DataSourceID = "SqlDataSource2";
        }
    }


    protected void Button3_Click(object sender, EventArgs e)
    {
        // To delete the record
        using (SqlCommand cmd = conn.CreateCommand())
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM Product WHERE ProductName = @ProductName";
            cmd.Parameters.Clear(); // Clear parameters before adding new ones
            cmd.Parameters.AddWithValue("@ProductName", TextBox1.Text);

            cmd.ExecuteNonQuery();
            Response.Write("<script>alert('Record Deleted')</script>");

            // Reorder serial numbers
            ReorderSerialNumbers();

            // Refresh the GridView
            RefreshGridView();
        }
    }

    private void ReorderSerialNumbers()
    {
        using (SqlCommand cmd = conn.CreateCommand())
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SET IDENTITY_INSERT Product OFF; " +
                              "DBCC CHECKIDENT ('Product', RESEED, 0); " +
                              "SET IDENTITY_INSERT Product ON;";

            cmd.ExecuteNonQuery();
        }
    }

    private void RefreshGridView()
    {
        SqlDataSource2.SelectCommand = "SELECT * FROM Product";
        GridView1.DataSourceID = "SqlDataSource2";
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        // Refresh the GridView
        SqlDataSource2.SelectCommand = "SELECT * FROM Product";
        GridView1.DataSourceID = "SqlDataSource2";
        TextBox1.Text = "";
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Get the selected row
        GridViewRow row = GridView1.SelectedRow;

        // Populate the text boxes with the selected row data
        t1.Text = row.Cells[2].Text; // ProductName
        t2.Text = row.Cells[3].Text; // HSNCode
        t3.Text = row.Cells[4].Text; // Quantity
        t4.Text = row.Cells[5].Text; // Unit
        t5.Text = row.Cells[6].Text; // Rate
        t6.Text = row.Cells[7].Text; // Purchase_price
        t7.Text = row.Cells[9].Text; // Dealer_Name

        // Open the modal using JavaScript
        ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenModal", "openModal();", true);
    }

    protected void Button5_Click1(object sender, EventArgs e)
    {
        // Search button click event handler
        SqlDataSource2.SelectCommand = "SELECT * FROM Product WHERE ProductName LIKE '%' + @ProductName + '%'";
        SqlDataSource2.SelectParameters.Clear();
        SqlDataSource2.SelectParameters.Add("ProductName", TextBox1.Text);
        GridView1.DataSourceID = "SqlDataSource2";
    }
}
