using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace sneakers_WebAPI.Models
{
    public class OrderDetailsModel
    {
        #region Properties
        public int orderId { get; set; }

        public DateTime orderDate { get; set; }

        public int customerId { get; set; }

        public int productId { get; set; }

        #endregion

        SqlConnection conn = new SqlConnection("server = DESKTOP-AL42T46\\TRAINEEINSTANCE; database = sneakerDB ; integrated security = true");

        #region Place an Order
        public string AddOrder(OrderDetailsModel newOrder)
        {
            SqlCommand cmd_add = new SqlCommand("INSERT INTO Orders VALUES(@orderDate, @customerId, @productId)", conn);
            
            cmd_add.Parameters.AddWithValue("@orderDate", newOrder.orderDate);
            cmd_add.Parameters.AddWithValue("@customerId", newOrder.customerId);
            cmd_add.Parameters.AddWithValue("@productId", newOrder.productId);
            

            try
            {
                conn.Open();
                cmd_add.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return "Order Placed Successfully!";
        }
        #endregion

        #region Cancel an Order
        public string CancelOrder(int customerId)
        {
            SqlCommand cmd_delete = new SqlCommand("DELETE FROM Orders WHERE customerId = @customerId", conn);
            cmd_delete.Parameters.AddWithValue("@customerId", customerId);
            try
            {
                conn.Open();
                cmd_delete.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return "Order Has Been Cancelled!";
        }
        #endregion

        #region All Customer History
        public List<OrderDetailsModel> GetOrderHistory()
        {
            SqlCommand cmd_allOrders = new SqlCommand("SELECT * FROM Orders", conn);
            List<OrderDetailsModel> oList = new List<OrderDetailsModel>();

            SqlDataReader _read = null;
            try
            {
                conn.Open();
                _read = cmd_allOrders.ExecuteReader();
                while(_read.Read())
                {
                    oList.Add(new OrderDetailsModel()
                    {
                        orderId = Convert.ToInt32(_read[0]),
                        orderDate = Convert.ToDateTime(_read[1]),
                        customerId = Convert.ToInt32(_read[2]),
                        productId = Convert.ToInt32(_read[3])
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
            return oList;
        }
        #endregion

        #region Order History by CustomerId
        public OrderDetailsModel GetOrder(int Id)
        {
            OrderDetailsModel order = new OrderDetailsModel();
            SqlCommand cmd_search = new SqlCommand("SELECT * FROM Orders WHERE orderId=@orderId", conn);
            cmd_search.Parameters.AddWithValue("@orderId", Id);
           
            SqlDataReader _read = null;

            try
            {
                conn.Open();
                _read = cmd_search.ExecuteReader();

                if(_read.Read())
                {
                    order.orderId = Convert.ToInt32(_read[0]);
                    order.orderDate = Convert.ToDateTime(_read[1]);
                    order.customerId = Convert.ToInt32(_read[2]);
                    order.productId = Convert.ToInt32(_read[3]);

                    return order;
                }
                else
                {
                    throw new Exception("Order Not Found!");
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
    }
}
