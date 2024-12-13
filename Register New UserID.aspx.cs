using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;


public partial class Register_New_UserID : System.Web.UI.Page
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
        cmd.CommandText = "SELECT * FROM login";

        SqlDataReader da = cmd.ExecuteReader();
        int userId = 0;

        while (da.Read())
        {
            if (da.GetString(0).ToString().Equals(TextBox1.Text))
            {
                userId = 1;
                break;
            }

        }
        da.Close();
        if (userId == 0)
        {
            if (TextBox2.Text == TextBox3.Text)
            {
                cmd.CommandText = "SELECT TOP 1 counter FROM login";
                int counters = 0;
                SqlDataReader counterReader = cmd.ExecuteReader();
                if (counterReader.Read())
                {
                    counters = counterReader.GetInt32(0);
                }
                counterReader.Close();

                cmd.CommandText = "insert into login values('" + TextBox1.Text + "', '" + TextBox2.Text + "','" + counters + "')";
                cmd.ExecuteNonQuery();
                Response.Write("<script>alert('Registered Successfully')</script>");
            }
            else
            {
                Response.Write("<script>alert('Password and Confirm Password do not match')</script>");
            }
        }
        else if(userId == 1)
        {
            cmd.CommandText = "update login  set password ='" + TextBox2.Text + "',counter='" + TextBox4.Text + "' where userid='" + TextBox1.Text + "'";
            cmd.ExecuteNonQuery();
                
            Response.Write("<script> alert('Counter Updated')</script>");
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        TextBox1.Text = "";
        TextBox2.Text = "";
        TextBox3.Text = "";
    }
}