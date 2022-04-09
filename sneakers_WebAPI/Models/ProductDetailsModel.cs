using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace sneakers_WebAPI.Models
{
    public class ProductDetailsModel
    {
        #region Product Properties
        public int ProductId { get; set; }

        public string pName { get; set; }

        public string pCategory { get; set; }

        public int pQty { get; set; }

        public int pPrice { get; set; }

        #endregion

        SqlConnection conn = new SqlConnection("server = DESKTOP-AL42T46\\TRAINEEINSTANCE; database = sneakerDB ; integrated security = true");

        #region Search For Specific Product
        public ProductDetailsModel GetProduct(int Id)
        {
            ProductDetailsModel product = new ProductDetailsModel();
            SqlCommand cmd_search = new SqlCommand("SELECT * FROM Product WHERE ProductId = @ProductId", conn);
            cmd_search.Parameters.AddWithValue("@productId", Id);
            SqlDataReader _read = null;

         
            try
            {
                conn.Open();
                _read = cmd_search.ExecuteReader();

                    if(_read.Read())
                    {
                        product.ProductId = Convert.ToInt32(_read[0]);
                        product.pName = _read[1].ToString();
                        product.pCategory = _read[2].ToString();
                        product.pQty = Convert.ToInt32(_read[3]);
                        product.pPrice = Convert.ToInt32(_read[4]);
                        

                    return product;

                    }
                    else
                    {
                        throw new Exception("Product Not Found!");
                    }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                _read.Close();
                conn.Close();
            }
        }
        #endregion

        #region List All Products
        public List<ProductDetailsModel> GetProductList()
        {
            SqlCommand cmd_allProducts = new SqlCommand("SELECT * FROM Product", conn);
            List<ProductDetailsModel> pList = new List<ProductDetailsModel>();
            SqlDataReader _read = null;

            try
            {
                conn.Open();
                _read = cmd_allProducts.ExecuteReader();

                while(_read.Read())
                {
                    pList.Add(new ProductDetailsModel()
                    {
                        ProductId = Convert.ToInt32(_read[0]),
                        pName = _read[1].ToString(),
                        pCategory = _read[2].ToString(),
                        pQty = Convert.ToInt32(_read[3]),
                        pPrice = Convert.ToInt32(_read[4]),
                        
                    });
                }
            }
            catch(SqlException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                _read.Close();
                conn.Close();
            }
            return pList;
        }
        #endregion

    }
}
