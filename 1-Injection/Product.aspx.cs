using System;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace _1_Injection
{
    public partial class Product : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var productSubCategoryId = Request.QueryString["ProductSubCategoryId"];
            int id;
            //if (!int.TryParse(productSubCategoryId, out id)) { throw new ApplicationException("ID wasn't an integer."); }

            var connString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            // Unsafe string concat
            var sqlString = "SELECT * FROM Product WHERE ProductSubCategoryID = " + productSubCategoryId;
            using (var conn = new SqlConnection(connString))
            {
                using (var command = new SqlCommand(sqlString, conn))
                {
                    command.Connection.Open();
                    ProductGridView.DataSource = command.ExecuteReader();
                    ProductGridView.DataBind();
                }
            }

            /*//Paramatarised input
            var sqlString = "SELECT * FROM Product WHERE ProductSubCategoryID = @ProductSubCategoryId";
            using (var conn = new SqlConnection(connString))
            {
                using (var command = new SqlCommand(sqlString, conn))
                {
                    command.Parameters.Add("@ProductSubcategoryId", SqlDbType.VarChar).Value = productSubCategoryId;
                    command.Connection.Open();
                    ProductGridView.DataSource = command.ExecuteReader();
                    ProductGridView.DataBind();
                }
            }*/

            /*//Stored procedure input
            var sqlString = "GetProducts";
            using (var conn = new SqlConnection(connString))
            {
                using (var command = new SqlCommand(sqlString, conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@ProductSubcategoryId", SqlDbType.VarChar).Value = productSubCategoryId;
                    //command.Parameters.Add("@ProductSubcategoryId", SqlDbType.Int).Value = id;
                    command.Connection.Open();
                    ProductGridView.DataSource = command.ExecuteReader();
                    ProductGridView.DataBind();
                }
            }*/

            var dc = new InjectionEntities();
            /*ProductGridView.DataSource = dc.Products.Where(p => p.ProductSubcategoryID == id).ToList();
            //ProductGridView.DataSource = dc.Products.Where(p => p.ProductSubcategoryID == productSubCategoryId).ToList();
            ProductGridView.DataBind();*/

            ProductCount.Text = ProductGridView.Rows.Count.ToString("n0");
        }
    }
}