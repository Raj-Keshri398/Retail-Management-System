using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;


public partial class Login : System.Web.UI.Page
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
        SqlCommand cmd = conn.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "SELECT * FROM login";
        SqlDataReader da = cmd.ExecuteReader();
        int Password = 0;
        int User = 0;
        DateTime manualDate = new DateTime(2024, 07, 19);
        

        while (da.Read())
        {
            if (da.GetString(0) == TextBox1.Text)
            {
                User = 1;
                if (da.GetString(1) == TextBox2.Text)
                {
                    Password = 1;
                    break;
                }
            }
        }
        da.Close();
        if (User == 1)
        {

            if (Password == 1)
            {

                if (DateTime.Now.Date != manualDate.Date)
                {
                        // counter is user for make the restriction of the client
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "UPDATE login set counter = counter -1";
                        cmd.ExecuteNonQuery();
                        Response.Write("<script>alert('Counter Updated')</script>");
                        Response.Write("<script>window.open('Dashboard.aspx','_self')</script>");                    
                }
                else
                {
                    Response.Write("<script>alert('Please renue your payment')</script>");
                }

            }
            else
            {
                Response.Write("<script>alert('Please Enter Valid Password')</script>");
            }

        }
        else
        {
            Response.Write("<script>alert('Please Enter Valid UserId')</script>");
        }

        
        conn.Close();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        TextBox1.Text = "";
        TextBox2.Text = "";
    }
}