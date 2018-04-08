using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

using System.Text.RegularExpressions;

namespace CSE411_Project
{
   public class Customer
    {
        public string fname, lname, tel, email;
        public bool insertCustomer(string fname, string lname, string tel, string email)
        {
            if(fname==null || lname==null || tel==null || email==null)
           // if (string.IsNullOrEmpty(fname) || string.IsNullOrEmpty(lname) || string.IsNullOrEmpty(tel) || string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException("Can not be null");
            }
           else if(!checkStringOnlyLetter(fname) || !checkStringOnlyLetter(lname))
            {
                // throw new ArgumentException("Not contain only letters");
                return false;

            }

           else if(!checkStringOnlyNumeric(tel))
            {
                return false;
            }

           else if(!IsValidEmail(email))
            {
                return false;
            }
          
            else
            {
                DB db = new DB();
                db.openConnection();
                this.fname = fname;
                this.lname = lname;
                this.tel = tel;
                this.email = email;
                SqlParameter[] parameters = new SqlParameter[4];

                parameters[0] = new SqlParameter("@fname", SqlDbType.VarChar);
                parameters[0].Value = fname;

                parameters[1] = new SqlParameter("@lname", SqlDbType.VarChar, 50);
                parameters[1].Value = lname;

                parameters[2] = new SqlParameter("@tel", SqlDbType.NChar, 20);
                parameters[2].Value = tel;

                parameters[3] = new SqlParameter("@mail", SqlDbType.VarChar, 50);
                parameters[3].Value = email;

                bool res = db.setData("spr_insert_customer", parameters);
                db.closeConnection();

                return res;
            }
            
            }

        public DataTable getCustomers()
        {
            DB db = new DB();
            DataTable tab = new DataTable();
            tab = db.getData("spr_get_customers", null);
            db.closeConnection();
            return tab;
        }


        public DataTable searchProducts(string valueToSearch)
        {

            DB db = new DB();
            DataTable table = new DataTable();
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@val", SqlDbType.VarChar, 100);
            parameters[0].Value = valueToSearch;
            table = db.getData("spr_search_Products", parameters);
            db.closeConnection();
            return table;
        }

        public bool deleteCustomer(int id)
        {

            DB db = new DB();
            DataTable table = new DataTable();
            SqlParameter[] parameters = new SqlParameter[1];

            parameters[0] = new SqlParameter("@id", SqlDbType.Int);
            parameters[0].Value = id;

            db.openConnection();
            bool res=db.setData("spr_delete_customer", parameters);
            db.closeConnection();
            return res;
           

        }

        public bool updateCustomer(int id, string fname, string lname, string tel, string email)
        {
           if (fname == null || lname == null || tel == null || email == null)
            // if (string.IsNullOrEmpty(fname) || string.IsNullOrEmpty(lname) || string.IsNullOrEmpty(tel) || string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException("Can not be null");
            }
          else if (!checkStringOnlyLetter(fname) || !checkStringOnlyLetter(lname))
            {
                // throw new ArgumentException("Not contain only letters");
                return false;

            }

           else if (!checkStringOnlyNumeric(tel))
            {
                return false;
            }

           else if (!IsValidEmail(email))
            {
                return false;
            }

            else
            {

                DB db = new DB();
                db.openConnection();
                SqlParameter[] parameters = new SqlParameter[5];

                parameters[0] = new SqlParameter("@id", SqlDbType.Int);
                parameters[0].Value = id;

                parameters[1] = new SqlParameter("@fname", SqlDbType.VarChar, 50);
                parameters[1].Value = fname;

                parameters[2] = new SqlParameter("@lname", SqlDbType.VarChar, 50);
                parameters[2].Value = lname;

                parameters[3] = new SqlParameter("@tel", SqlDbType.NChar, 20);
                parameters[3].Value = tel;

                parameters[4] = new SqlParameter("@mail", SqlDbType.VarChar, 50);
                parameters[4].Value = email;

                bool res = db.setData("spr_update_customer", parameters);
                db.closeConnection();
                return res;
            }

        }

        public bool checkStringOnlyLetter(string test)
        {
            if (!Regex.IsMatch(test, @"^[a-zA-Z]+$"))
            {
                return false;
            }
            return true;
        }

        public bool checkStringOnlyNumeric(string test)
        {
            if (!Regex.IsMatch(test, @"^[0-9 ]+$"))
            {
                return false;
            }
            return true;
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

       


    }
}
