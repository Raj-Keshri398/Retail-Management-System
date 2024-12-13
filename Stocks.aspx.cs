using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Stocks : System.Web.UI.Page
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
        cmd.CommandText = "insert into stocks values('" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "')";
        cmd.ExecuteNonQuery();
        Response.Write(" <script>alert('Record Saved')</script>");
        SqlDataSource1.SelectCommand = "SELECT * FROM Stocks";
        GridView1.DataSourceID = "SqlDataSource1";
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        //To update the record
        SqlCommand cmd = conn.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "update stocks  set productid='" + TextBox2.Text + "',quantity='" + TextBox3.Text + "' where storeid='" + TextBox1.Text + "'";
        cmd.ExecuteNonQuery();
        Response.Write(" <script>alert('Record Updated')</script>");
        SqlDataSource1.SelectCommand = "SELECT * FROM Stocks";
        GridView1.DataSourceID = "SqlDataSource1";
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        //To delete the record

        SqlCommand cmd = conn.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "delete from stocks where storeid='" + TextBox1.Text + "'";
        cmd.ExecuteNonQuery();
        Response.Write(" <script>alert('Record Deleted')</script>");
        SqlDataSource1.SelectCommand = "SELECT * FROM Stocks";
        GridView1.DataSourceID = "SqlDataSource1";
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        SqlDataSource1.SelectCommand = "SELECT * FROM Stocks";
        GridView1.DataSourceID = "SqlDataSource1";
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        SqlDataSource1.SelectCommand = "SELECT * FROM Stocks where storeid='"+TextBox1.Text+"' ";
        GridView1.DataSourceID = "SqlDataSource1";
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        Button7.OnClientClick = "~/RepostStock.aspx";
    }
}