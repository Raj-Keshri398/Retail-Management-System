using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;


public partial class AdminLogin : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\Retail.mdf;Integrated Security=True");
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button2_Click1(object sender, EventArgs e)
    {
        SqlCommand cmd = conn.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "SELECT * FROM login";

        try
        {
            conn.Open();
            SqlDataReader da = cmd.ExecuteReader();
            int Password = 0;
            int User = 0;

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

            if (User == 1)
            {
                if (Password == 1)
                {
                    if (TextBox1.Text == "Admin")
                    {
                        Response.Write("<script>alert('Welcome, Admin')</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('Please Enter Valid Password')</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Please Enter Valid UserId. This Panel for Admin.')</script>");
            }

            da.Close();
        }
        catch (Exception ex)
        {
            // Handle exception
            Response.Write("<script>alert('An error occurred: " + ex.Message + "')</script>");
        }
        finally
        {
            conn.Close();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        TextBox1.Text = "";
        TextBox2.Text = "";
    }
}