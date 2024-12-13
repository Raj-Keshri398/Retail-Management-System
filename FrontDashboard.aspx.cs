using System;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Data;

public partial class shipment : System.Web.UI.Page
{
    private static readonly string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\Retail.mdf;Integrated Security=True";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UpdateSalesStats();
            UpdatePurchaseStats();
            UpdateTodaySalesStats();
            UpdateTodayPurchaseStats();
            CalculateProfitAndLoss();
            CalculateTodayProfitAndLoss();
        }
    }

    private void UpdateSalesStats()
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = @"
            SELECT 
                ISNULL(SUM(TotalAmount), 0) AS TotalSales,
                ISNULL(SUM(BalanceDue), 0) AS TotalBalanceDue
            FROM Sales";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Label1.Text = "₹" + Convert.ToDecimal(reader["TotalSales"]).ToString("N2");
                        Label3.Text = "₹" + Convert.ToDecimal(reader["TotalBalanceDue"]).ToString("N2");
                    }
                }
            }
        }
    }

    private void UpdatePurchaseStats()
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = @"
            SELECT 
                ISNULL(SUM(TotalAmount), 0) AS TotalPurchase
            FROM Purchase";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Label7.Text = "₹" + Convert.ToDecimal(reader["TotalPurchase"]).ToString("N2");
                        
                    }
                }
            }
        }
    }

    private void UpdateTodaySalesStats()
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string todayDate = DateTime.Now.ToString("yyyy-MM-dd");

            string query = @"
            SELECT 
                ISNULL(SUM(TotalAmount), 0) AS TodaySales,
                ISNULL(SUM(BalanceDue), 0) AS TodayBalanceDue
            FROM Sales
            WHERE BillDate = @TodayDate";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TodayDate", todayDate);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Label4.Text = "₹" + Convert.ToDecimal(reader["TodaySales"]).ToString("N2");
                        Label6.Text = "₹" + Convert.ToDecimal(reader["TodayBalanceDue"]).ToString("N2");
                        Label11.Text = todayDate;
                    }
                }
            }
        }
    }

    private void UpdateTodayPurchaseStats()
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string todayDate = DateTime.Now.ToString("yyyy-MM-dd");

            string query = @"
            SELECT 
                ISNULL(SUM(TotalAmount), 0) AS TodayPurchase
            FROM Purchase
            WHERE BillDate = @TodayDate";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TodayDate", todayDate);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Label9.Text = "₹" + Convert.ToDecimal(reader["TodayPurchase"]).ToString("N2");
         
                    }
                }
            }
        }
    }

    private void CalculateProfitAndLoss()
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();

            string query = @"
            SELECT 
                ISNULL(SUM(Items.Total - (Product.Purchase_price * Items.Quantity)), 0) AS TotalProfit,
                ISNULL(SUM(CASE 
                            WHEN Product.Purchase_price > (Items.Total / Items.Quantity) 
                            THEN (Product.Purchase_price - (Items.Total / Items.Quantity)) * Items.Quantity
                            ELSE 0 
                          END), 0) AS TotalLoss
            FROM Product
            JOIN Items ON Product.ProductName = Items.ProductName";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Label2.Text = "₹" + Convert.ToDecimal(reader["TotalProfit"]).ToString("N2");
                        Label8.Text = "₹" + Convert.ToDecimal(reader["TotalLoss"]).ToString("N2");
                    }
                }
            }
        }
    }

    private void CalculateTodayProfitAndLoss()
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string todayDate = DateTime.Now.ToString("yyyy-MM-dd");

            string query = @"
            SELECT 
                ISNULL(SUM(Items.Total - (Product.Purchase_price * Items.Quantity)), 0) AS TodayProfit,
                ISNULL(SUM(CASE 
                            WHEN Product.Purchase_price > (Items.Total / Items.Quantity) 
                            THEN (Product.Purchase_price - (Items.Total / Items.Quantity)) * Items.Quantity
                            ELSE 0 
                          END), 0) AS TodayLoss
            FROM Items
            JOIN Product ON Items.ProductName = Product.ProductName
            JOIN Sales ON Items.BillNumber = Sales.BillNumber
            WHERE Sales.BillDate = @TodayDate";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TodayDate", todayDate);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Label5.Text = "₹" + Convert.ToDecimal(reader["TodayProfit"]).ToString("N2");
                        Label10.Text = "₹" + Convert.ToDecimal(reader["TodayLoss"]).ToString("N2");
                    }
                }
            }
        }
    }

    
}
