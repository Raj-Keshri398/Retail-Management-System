using System;
using System.Data.SqlClient;
using System.Web.UI;

public partial class SupplierBillDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Check if the BillNumber query string parameter exists
            if (Request.QueryString["BillNumber"] != null)
            {
                string billNumber = Request.QueryString["BillNumber"];

                // Display the BillNumber for debugging
                lblBillNumber.Text = "Bill Number: " + billNumber;

                // Set the parameter value for SqlDataSource
                SqlDataSource2.SelectParameters["BillNumber"].DefaultValue = billNumber;

                // DataBind to ensure GridView is bound to the SqlDataSource
                GridView2.DataBind();

                // Fetch and display the BillDate
                DisplayBillDate(billNumber);
            }
            else
            {
                lblBillNumber.Text = "Bill Number not provided";
            }
        }
    }
    private void DisplayBillDate(string billNumber)
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        string query = "SELECT BillDate FROM Purchase WHERE BillNumber = @BillNumber";

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@BillNumber", billNumber);
                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    lblBillDate.Text = "Bill Date: " + Convert.ToDateTime(result).ToString("dd-MM-yyyy");
                }
                else
                {
                    lblBillDate.Text = "Bill Date not found";
                }
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        // Redirect to the previous page or default to Transaction.aspx
        if (Session["PreviousPage"] != null)
        {
            Response.Redirect(Session["PreviousPage"].ToString());
        }

    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        // Store the current page URL in the session to use for the back button
        if (Request.UrlReferrer != null)
        {
            Session["PreviousPage"] = Request.UrlReferrer.ToString();
        }
    }
}