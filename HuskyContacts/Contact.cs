using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuskyContacts
{
    class Contact
    {
        public int Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipCode { get; set; }
     
        public string phone { get; set; }
        public string email { get; set; }

        // create a connection string allow connect with database server
        static string myconnstr = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;

        //select data from database put into temporary table in the application
        public DataTable Select()
        {
            // connect with database
            SqlConnection conn = new SqlConnection(myconnstr);
            // create a temprary table
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM tbl_contacts";
                //create a command using sql query "sql" and connection "conn" above
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                //open connection
                conn.Open();
                // fill the teporary table with data reveiced from database
                adapter.Fill(dt);

            }
            catch(Exception ex){ }
            finally
            { 
                // close the connection with database server
                conn.Close();
            }
            return dt;
        }

        // insert data got from the form into database
        public bool Insert (Contact c) 
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstr);
            try
            {
                //create an insert sql query with parameter
                string sql = "INSERT INTO tbl_contacts (First_Name, Last_Name, Street, City, State, Zipcode, Phone, Email) VALUES (@First_Name, @Last_Name, @Street, @City, @State, @Zipcode, @Phone, @Email)";
                // command sql string and connection with database server
                SqlCommand cmd = new SqlCommand(sql, conn);
                //put value into parameter to sent it into insert query
                cmd.Parameters.AddWithValue("@First_Name", c.firstName);
                cmd.Parameters.AddWithValue("@Last_Name", c.lastName);
                cmd.Parameters.AddWithValue("@Street", c.street);
                cmd.Parameters.AddWithValue("@City", c.city);
                cmd.Parameters.AddWithValue("@State", c.state);
                cmd.Parameters.AddWithValue("@Zipcode", c.zipCode);
                cmd.Parameters.AddWithValue("@Phone", c.phone);
                cmd.Parameters.AddWithValue("@Email", c.email);
                // open connection with database
                conn.Open();
                // excute the query and return the number of row has been insert into database
                int rows = cmd.ExecuteNonQuery();
                if(rows > 0) // if the row is not 0, it mean insert value into table successful 
                {
                    // change the boolean variable isSuccess to true when insert success
                    isSuccess = true; 
                } 
            }
            catch(Exception ex) { }
            finally 
            {
                conn.Close ();
            }
            return isSuccess; 
        }

        //update a contact information
        public bool Update(Contact c) 
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstr);
            try 
            {
                // create sql query to update a record in table tbl_contacts with the ID is equal with ID of contact choosen in datagridview
                string sql = "UPDATE tbl_contacts SET First_Name=@First_Name, Last_Name=@Last_Name, Street=@Street, City=@City, State=@State, Zipcode=@Zipcode, Phone=@Phone, Email=@Email WHERE Id = @contactID ";

                //create sql command
                SqlCommand cmd = new SqlCommand (sql, conn);
                cmd.Parameters.AddWithValue("@First_Name", c.firstName);
                cmd.Parameters.AddWithValue("@Last_Name", c.lastName);
                cmd.Parameters.AddWithValue ("@Street", c.street);
                cmd.Parameters.AddWithValue("@City", c.city);
                cmd.Parameters.AddWithValue("@State", c.state);
                cmd.Parameters.AddWithValue("@Zipcode", c.zipCode);
                cmd.Parameters.AddWithValue("@Phone", c.phone);
                cmd.Parameters.AddWithValue("@Email", c.email);
                cmd.Parameters.AddWithValue("@contactID", c.Id);
                // open connection with database
                conn.Open();
                int rows = cmd.ExecuteNonQuery (); // excute the query and return number of row effected
                if(rows > 0) 
                { 
                    isSuccess = true; // change it into true when update successfull 
                } 
               
            }
            catch (Exception ex) { }
            finally 
            { 
                conn.Close (); 
            }
            return isSuccess;
        }

        //delete a record in the table tbl_contacts with the ID equal with ID of contact choosen in datagridview
        public bool Delete(Contact c) 
        { 
            //create a default return value and set it false
            bool isSuccess = false;
            //create a connection to database
            SqlConnection conn = new SqlConnection(myconnstr);
            try 
            {
                string sql = "DELETE FROM tbl_contacts WHERE ID = @ContactID";
                //create sql command
                SqlCommand cmd = new SqlCommand (sql, conn);
                cmd.Parameters.AddWithValue("@ContactID", c.Id);
                //open connection with database
                conn.Open ();
                int rows = cmd.ExecuteNonQuery ();
                if(rows > 0) 
                {
                    isSuccess= true; // if delete successfull
                }               
            }
            catch (Exception ex) { }
            finally 
            { 
                conn.Close (); 
            }
            return isSuccess;
        }
    }
}
