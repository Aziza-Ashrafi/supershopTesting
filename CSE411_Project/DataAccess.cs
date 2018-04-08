using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace CSE411_Project
{
   public class DataAccess
    {

        //  public string ConnectionString { get; set; }
        public SqlConnection conn;
        public SqlCommand comm;


        public DataAccess()
        {
            conn = new SqlConnection();
            comm = new SqlCommand();
            conn.ConnectionString = @"Server=DESKTOP-UU9H0E8\SQLEXPRESS;Database=MY_DB;Integrated Security=true";
            comm.Connection = conn;
            // public string ConnectionString = ConfigurationManager.ConnectionStrings["supershop"].ConnectionString;
        }

        public bool Connect()
        {
           
        conn.Open();
        return conn.State == ConnectionState.Open;
        }

        public bool Disconnect()
        {
            
            conn.Close();

            return conn.State == ConnectionState.Closed;
        }

        public SqlCommand GetSqlCommand(string storedProcedure)
        {
            SqlCommand sqlCmd = new SqlCommand(storedProcedure, conn);
            sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
            return sqlCmd;
        }


      public bool setData(string procedureName, SqlParameter[] procedureParams)
                {
                    try
                    { 
                   
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.CommandText = procedureName;
                    comm.Connection = conn;
                    if (procedureParams != null)
                    {
                        comm.Parameters.AddRange(procedureParams);
                    }

                    int rows = comm.ExecuteNonQuery();
                
                  if (rows > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException)
            {
                return false;
            }
        }

      

       

    }
}
