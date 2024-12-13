using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;


public partial class Stock : System.Web.UI.Page
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        //To save the record

        SqlCommand cmd = conn.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "insert into stocks values('" + t1.Text + "','" + t2.Text + "','" + t3.Text + "')";
        cmd.ExecuteNonQuery();
        Response.Write(" <script>alert('Record Saved')</script>");
        SqlDataSource1.SelectCommand = "SELECT * FROM Stocks";
        GridView1.DataSourceID = "SqlDataSource1";
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        //To delete the record

        SqlCommand cmd = conn.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "delete from stocks where storeid='" + t1.Text + "'";
        cmd.ExecuteNonQuery();
        Response.Write(" <script>alert('Record Deleted')</script>");
        SqlDataSource1.SelectCommand = "SELECT * FROM Stocks";
        GridView1.DataSourceID = "SqlDataSource1";
    }


    
}