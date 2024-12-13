using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;


public partial class Customers : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\Retail.mdf;Integrated Security=True");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (conn.State == ConnectionState.Open)
        {
            conn.Close();
        }
        conn.Open();
 
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        //To save the record
        SqlCommand cmd = conn.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "SELECT * FROM Customers";
        SqlDataReader da = cmd.ExecuteReader();
        int c = 0;
        while (da.Read())
        {
            if (da.GetInt32(0).ToString().Equals(t1.Text))
            {
                c = 1;
                break;
            }

        }
        da.Close();
        if (c == 0)
        {
            cmd.CommandText = "insert into Customers values('" + t1.Text + "', '" + t2.Text + "', '" + t3.Text + "', '" + t4.Text + "', '" + t5.Text + "', '" + t6.Text + "')";
            cmd.ExecuteNonQuery();
            Response.Write("<script>alert('Record Saved')</script>");
            SqlDataSource1.SelectCommand = "SELECT * FROM Customers";
            GridView1.DataSourceID = "SqlDataSource1";
        }
        else
        {
            Response.Write("<script> alert('Customer id already exist')</script>");
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //To update the record
        SqlCommand cmd = conn.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "update Customers  set PartyName='" + t2.Text + "',PartyAddress='" + t3.Text + "',PhoneNumber='" + t4.Text + "',Email='" + t5.Text + "',GSTIN='" + t6.Text + "' where id='" + t1.Text + "'";
        cmd.ExecuteNonQuery();
        Response.Write(" <script>alert('Record Updated')</script>");
        SqlDataSource1.SelectCommand = "SELECT * FROM Customers";
        GridView1.DataSourceID = "SqlDataSource1";
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        //To delete the record

        SqlCommand cmd = conn.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "delete from Customers where id='" + t1.Text + "'";
        cmd.ExecuteNonQuery();
        Response.Write(" <script>alert('Record Deleted')</script>");
        SqlDataSource1.SelectCommand = "SELECT * FROM Customers";
        GridView1.DataSourceID = "SqlDataSource1";
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = conn.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "SELECT * FROM Customers";
        GridView1.DataSourceID = "SqlDataSource1";
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Populate the text boxes with selected row data
        GridViewRow row = GridView1.SelectedRow;
        t1.Text = row.Cells[1].Text; // first column is ID
        t2.Text = row.Cells[2].Text; // second column is customer Name
        t3.Text = row.Cells[3].Text; // third column is address
        t4.Text = row.Cells[4].Text; // fourth column is phone
        t5.Text = row.Cells[5].Text; // fifth column is email 
        t6.Text = row.Cells[6].Text; //sixth column is gstin
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        t1.Text = "";
        t2.Text = "";
        t3.Text = "";
        t4.Text = "";
        t5.Text = "";
        t6.Text = "";
        TextBox1.Text = "";
        
    }
    
    protected void btnAction_Click(object sender, EventArgs e)
    {
        // Get the button that was clicked
        Button button = (Button)sender;

        // Get the BillNumber from the CommandArgument
        string PhoneNumber = button.CommandArgument;

        // Redirect to CustomerBillDetails.aspx and pass the BillNumber
        Response.Redirect("Transaction.aspx?PhoneNumber=" + PhoneNumber);
    }
    protected void Button5_Click1(object sender, EventArgs e)
    {
        // Search button click event handler
        SqlDataSource1.SelectCommand = "SELECT * FROM Customers WHERE id LIKE '%" + TextBox1.Text + "%'";
        GridView1.DataSourceID = "SqlDataSource1";
    }
}