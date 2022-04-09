using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace sneakers_WebAPI.Models
{
    public class SneakerDetailsModel
    {
        #region Properties
        public int CustomerId { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string address { get; set; }

        public string city { get; set; }

        public string state { get; set; }

        public int zip { get; set; }      
        #endregion

        SqlConnection conn = new SqlConnection("server = DESKTOP-AL42T46\\TRAINEEINSTANCE; database = sneakerDB ; integrated security = true");
        
        #region List Customers
        public List<SneakerDetailsModel> GetCustomersList()
        {
            SqlCommand cmd_allSneakers = new SqlCommand("SELECT * FROM Customers", conn);
            List<SneakerDetailsModel> sList = new List<SneakerDetailsModel>();
            SqlDataReader readAllSneakers = null;

            try
            {
                conn.Open();
                readAllSneakers = cmd_allSneakers.ExecuteReader();

                while(readAllSneakers.Read())
                {
                    sList.Add(new SneakerDetailsModel()
                    {
                        CustomerId = Convert.ToInt32(readAllSneakers[0]),
                        firstName = readAllSneakers[1].ToString(),
                        lastName = readAllSneakers[2].ToString(),
                        address = readAllSneakers[3].ToString(),
                        city = readAllSneakers[4].ToString(),
                        state = readAllSneakers[5].ToString(),
                        zip = Convert.ToInt32(readAllSneakers[6]),
                        

                    });           
                }
            }
            catch(SqlException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conn.Close();
                readAllSneakers.Close();
            }

            return sList;
        }
        #endregion

        #region GetCustomer
        public SneakerDetailsModel GetCustomers(int Id)
        {
            SneakerDetailsModel air = new SneakerDetailsModel();
            SqlCommand cmd_searchProduct = new SqlCommand("SELECT * FROM Customers WHERE CustomerId = @CustomerId", conn);
            cmd_searchProduct.Parameters.AddWithValue("@CustomerId", Id);
            SqlDataReader _read = null;
            

            try
            {
                conn.Open();
                _read = cmd_searchProduct.ExecuteReader();

                    if(_read.Read())
                    {
                        air.CustomerId = Convert.ToInt32(_read[0]);
                        air.firstName = _read[1].ToString();
                        air.lastName = _read[2].ToString();
                        air.address = _read[3].ToString();
                        air.city = _read[4].ToString();
                        air.state = _read[5].ToString();
                        air.zip = Convert.ToInt32(_read[6]);

                    return air;
                }
                    else
                    {
                        throw new Exception("Customer Not Found");
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

        #region Add Customer
        public string AddCustomer(SneakerDetailsModel newCustomer)
        {
            SqlCommand cmd_add = new SqlCommand("INSERT INTO Customers VALUES (@firstname, @lastname, @address, @city, @state, @zip)", conn);
            cmd_add.Parameters.AddWithValue("@firstname", newCustomer.firstName);
            cmd_add.Parameters.AddWithValue("@lastname", newCustomer.lastName);
            cmd_add.Parameters.AddWithValue("@address", newCustomer.address);
            cmd_add.Parameters.AddWithValue("@city", newCustomer.city);
            cmd_add.Parameters.AddWithValue("@state", newCustomer.state);
            cmd_add.Parameters.AddWithValue("@zip", newCustomer.zip);

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
            return "Customer Added Successfully!";
        }
        #endregion

        #region Delete Customer
        public string DeleteCustomer(int CustomerId)
        {
            SqlCommand cmd_delete = new SqlCommand("DELETE FROM Customers WHERE CustomerId = @CustomerId", conn);
            cmd_delete.Parameters.AddWithValue("@CustomerId", CustomerId);

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
            return "Customer Delete Successfully";
        }
        #endregion

        #region Update Customers
        public string UpdateCustomer(SneakerDetailsModel updates)
        {
            SqlCommand cmd_update = new SqlCommand("UPDATE Customers SET firstname = @firstname, lastname = @lastname, address = @address, city = @city, state = @state, zip = @zip WHERE CustomerId = @CustomerId", conn);
            cmd_update.Parameters.AddWithValue("@firstname", updates.firstName);
            cmd_update.Parameters.AddWithValue("@lastname", updates.lastName);
            cmd_update.Parameters.AddWithValue("@address", updates.address);
            cmd_update.Parameters.AddWithValue("@city", updates.city);
            cmd_update.Parameters.AddWithValue("@state", updates.state);
            cmd_update.Parameters.AddWithValue("@zip", updates.zip);
            cmd_update.Parameters.AddWithValue("@CustomerId", updates.CustomerId);

            try
            {
                conn.Open();
                cmd_update.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return "Customer Updated Successfully!";
        }
        #endregion



    }
}
