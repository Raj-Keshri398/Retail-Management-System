using System;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;

public partial class CustomerBillDetails : System.Web.UI.Page
{
    private string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\Retail.mdf;Integrated Security=True";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["BillNumber"] != null)
            {
                string billNumber = Request.QueryString["BillNumber"];
                lblBillNumber.Text = "Bill Number: " + billNumber;
                SqlDataSource2.SelectParameters["BillNumber"].DefaultValue = billNumber;
                GridView2.DataBind();
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
        string query = "SELECT BillDate FROM Sales WHERE BillNumber = @BillNumber";

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
        if (Session["PreviousPage"] != null)
        {
            Response.Redirect(Session["PreviousPage"].ToString());
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Request.UrlReferrer != null)
        {
            Session["PreviousPage"] = Request.UrlReferrer.ToString();
        }
    }

    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        // Set the ProductName parameter
        string productName = GridView2.DataKeys[e.RowIndex].Value.ToString();
        SqlDataSource2.DeleteParameters["ProductName"].DefaultValue = productName;
        SqlDataSource2.Delete();
        GridView2.DataBind();
    }

    protected void printBill_Click(object sender, EventArgs e)
    {
        GeneratePdf();
    }

    private void GeneratePdf()
    {
        Document pdfDoc = new Document(PageSize.A4, 25, 25, 30, 30);
        MemoryStream ms = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, ms);
        pdfDoc.Open();

        pdfDoc.Add(new Paragraph(lblBillNumber.Text, FontFactory.GetFont("Arial", 14, Font.BOLD)));
        pdfDoc.Add(new Paragraph(lblBillDate.Text, FontFactory.GetFont("Arial", 12, Font.BOLD)));
        pdfDoc.Add(new Paragraph("\n"));

        PdfPTable pdfTable = new PdfPTable(GridView2.Columns.Count);

        foreach (DataControlField column in GridView2.Columns)
        {
            if (column is BoundField)
            {
                pdfTable.AddCell(new Phrase(column.HeaderText));
            }
        }

        foreach (GridViewRow row in GridView2.Rows)
        {
            foreach (TableCell cell in row.Cells)
            {
                pdfTable.AddCell(new Phrase(cell.Text));
            }
        }

        pdfDoc.Add(pdfTable);
        pdfDoc.Close();
        writer.Close();

        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=BillDetails.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.BinaryWrite(ms.ToArray());
        Response.End();
    }
}
