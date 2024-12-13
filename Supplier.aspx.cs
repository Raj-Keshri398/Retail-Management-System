using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Supplier : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\Retail.mdf;Integrated Security=True");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // No need to open the connection here since you are opening it in individual methods.
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            conn.Open();

            string phoneNumber = t4.Text;

            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Supplier WHERE Phone = @PhoneNumber", conn);
            cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);

            int count = (int)cmd.ExecuteScalar();

            if (count > 0)
            {
                Response.Write("<script>alert('This Supplier already exists')</script>");
            }
            else
            {
                cmd.CommandText = "INSERT INTO Supplier (ID, Name, Address, Phone, GSTIN) VALUES (@ID, @Name, @Address, @Phone, @GSTIN)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID", t1.Text);
                cmd.Parameters.AddWithValue("@Name", t2.Text);
                cmd.Parameters.AddWithValue("@Address", t3.Text);
                cmd.Parameters.AddWithValue("@Phone", phoneNumber);
                cmd.Parameters.AddWithValue("@GSTIN", t5.Text);

                cmd.ExecuteNonQuery();
                Response.Write("<script>alert('Supplier Data Saved')</script>");

                // Update the GridView
                SqlDataSource1.SelectCommand = "SELECT * FROM Supplier";
                GridView1.DataSourceID = "SqlDataSource1";
                GridView1.DataBind();
            }
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE Supplier SET Name=@Name, Address=@Address, Phone=@Phone, GSTIN=@GSTIN WHERE ID=@ID", conn);
            cmd.Parameters.AddWithValue("@Name", t2.Text);
            cmd.Parameters.AddWithValue("@Address", t3.Text);
            cmd.Parameters.AddWithValue("@Phone", t4.Text);
            cmd.Parameters.AddWithValue("@GSTIN", t5.Text);
            cmd.Parameters.AddWithValue("@ID", t1.Text);

            cmd.ExecuteNonQuery();
            Response.Write("<script>alert('Record Edited')</script>");

            // Update the GridView
            SqlDataSource1.SelectCommand = "SELECT * FROM Supplier";
            GridView1.DataSourceID = "SqlDataSource1";
            GridView1.DataBind();
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = GridView1.SelectedRow;
        t1.Text = row.Cells[1].Text.Trim();
        t2.Text = row.Cells[2].Text.Trim();
        t3.Text = row.Cells[3].Text.Trim();
        t4.Text = row.Cells[4].Text.Trim();
        t5.Text = row.Cells[5].Text.Trim();
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        t1.Text = "";
        t2.Text = "";
        t3.Text = "";
        t4.Text = "";
        t5.Text = "";
        TextBox1.Text = "";       
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        // Search button click event handler
        SqlDataSource1.SelectCommand = "SELECT * FROM Supplier WHERE Phone LIKE '%" + TextBox1.Text + "%'";
        GridView1.DataSourceID = "SqlDataSource1";
    }

    protected void RefreshButton_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = conn.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "SELECT * FROM Supplier";
        GridView1.DataSourceID = "SqlDataSource1";
    }

    protected void DeleteButton_Click(object sender, EventArgs e)
    {
        try
        {
            conn.Open(); // Open the connection

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM Supplier WHERE Phone='" + TextBox1.Text + "'";
            cmd.ExecuteNonQuery();

            Response.Write("<script>alert('Record Deleted')</script>");

            SqlDataSource1.SelectCommand = "SELECT * FROM Supplier";
            GridView1.DataSourceID = "SqlDataSource1";
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close(); // Close the connection
            }
        }
    }

    protected void btnAction_Click(object sender, EventArgs e)
    {
        // Get the button that was clicked
        Button button = (Button)sender;

        // Get the BillNumber from the CommandArgument
        string Phone = button.CommandArgument;

        // Redirect to CustomerBillDetails.aspx and pass the BillNumber
        Response.Redirect("PurchaseTransaction.aspx?PhoneNumber=" + Phone);
    }

}
