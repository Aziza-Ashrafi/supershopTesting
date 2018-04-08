﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace CSE411_Project
{
    public class DB
    {
       public SqlConnection connection;

        public DB()
        {
            connection = new SqlConnection(@"Server= DESKTOP-UU9H0E8\SQLEXPRESS; Database = MY_DB; Integrated Security = true");
        }

        public void openConnection()
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
        }

        public void closeConnection()
        {
            if (connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
        }

        public DataTable getData(string procedureName, SqlParameter[] procedureParams)
        {
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = procedureName;
            command.Connection = connection;
            if (procedureParams != null)
            {
                for (int i = 0; i < procedureParams.Length; i++)
                {
                    command.Parameters.Add(procedureParams[i]);
                }
            }

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        public bool setData(string procedureName, SqlParameter[] procedureParams)
        {
            try { 
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = procedureName;
            command.Connection = connection;
            if (procedureParams != null)
            {
                command.Parameters.AddRange(procedureParams);
            }

            int rows =command.ExecuteNonQuery();
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
