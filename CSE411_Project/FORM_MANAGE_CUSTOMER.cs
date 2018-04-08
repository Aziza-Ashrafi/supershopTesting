using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSE411_Project
{
    public partial class FORM_MANAGE_CUSTOMER : Form
    {
        Customer customer = new Customer();
        Businesslogic bl = new Businesslogic();
        //Order order = new Order();
        int position = 0;
        public FORM_MANAGE_CUSTOMER()
        {
            InitializeComponent();
            DGV_CUSTOMERS.DataSource = customer.getCustomers();

        }

        private void FORM_MANAGE_CUSTOMER_Load(object sender, EventArgs e)
        {
            navigation(position);
          
       
        }

        // add customer
        private void BTN_INSERT_CUSTOMER_Click(object sender, EventArgs e)
        {
            bool supress = false;
            bool check = customer.insertCustomer(TB_FNAME.Text, TB_LNAME.Text, TB_TEL.Text, TB_EMAIL.Text);
            showSmsForInsertion(check,supress);
        }

        // get selected customer info in textboxes
        private void DGV_CUSTOMERS_Click(object sender, EventArgs e)
        {
            string id= DGV_CUSTOMERS.CurrentRow.Cells[0].Value.ToString();
            string fname = DGV_CUSTOMERS.CurrentRow.Cells[1].Value.ToString();
            string lname = DGV_CUSTOMERS.CurrentRow.Cells[2].Value.ToString();
            string tel = DGV_CUSTOMERS.CurrentRow.Cells[3].Value.ToString();
            string email = DGV_CUSTOMERS.CurrentRow.Cells[4].Value.ToString();

            getCustomerFromDataGridView(id, fname, lname, tel, email);


        }

        // update customer
        private void BTN_UPDATE_CUSTOMER_Click(object sender, EventArgs e)
        {
            bool supress = false;
            updateCustomer(supress, TB_ID_CUSTOMER.Text,TB_FNAME.Text,TB_LNAME.Text,TB_TEL.Text,TB_EMAIL.Text);


        }

        // delete customer
        private void BTN_DELETE_CUSTOMER_Click(object sender, EventArgs e)
        {
            bool supress = false;
            deleteCustomer(supress,TB_ID_CUSTOMER.Text);

        }

        public int navigation(int pos)      //1st row load in text box 
        {
            DataTable table = new DataTable();
            table = customer.getCustomers();
            TB_ID_CUSTOMER.Text = table.Rows[pos][0].ToString();
            TB_FNAME.Text = table.Rows[pos][1].ToString();
            TB_LNAME.Text = table.Rows[pos][2].ToString();
            TB_TEL.Text = table.Rows[pos][3].ToString();
            TB_EMAIL.Text = table.Rows[pos][4].ToString();

            // int test= Convert.ToInt32(TB_ID_CUSTOMER.Text);
            int test = bl.convertIntoint(TB_ID_CUSTOMER.Text); //return 1st id
           return test;
        }

        private void BTN_PREVIOUS_Click(object sender, EventArgs e)
        {
            if (position == 0)
                 return;
             position--;
             navigation(position);
           // previouseBtn(position);
            
        }

        private void BTN_NEXT_Click(object sender, EventArgs e)
        {
            if (position == customer.getCustomers().Rows.Count - 1)
                 return;
             position++;
             navigation(position);
           // nextBtn(position);
        }

        private void PANEL_MIN_Click(object sender, EventArgs e)
        {
            check_minimize("Normal");
        }

        private void PANEL_CLOSE_Click(object sender, EventArgs e)
        {
            checkClose();
        }

        private void BTN_RESET_Click(object sender, EventArgs e)
        {
            resetBtn();
        }


        //modified for testing purpose

       public bool check_minimize(string n)        
         {
             WindowState = FormWindowState.Minimized;

             if(WindowState.Equals(n))
             {
                 return true;

             }
             return false;

         }

        public string resetBtn()
        {
            TB_ID_CUSTOMER.Text = "";
            TB_FNAME.Text = "";
            TB_LNAME.Text = "";
            TB_TEL.Text = "";
            TB_EMAIL.Text = "";

            return TB_ID_CUSTOMER.Text;

        }

        public int checkClose()
        {
            Close();
            return 2;
        }

       public int previouseBtn(int pos) //only for testing
        {
            if (pos == 0)
                return 1;
            pos--;
            navigation(pos);
            return pos;
        }

        public int nextBtn(int pos)
        {
            if (pos == customer.getCustomers().Rows.Count - 1)
                return 1;
            pos++;
            navigation(pos);
            return pos;
        }
        
       /* public int convertIntoint(string str)
        {
           int n= Convert.ToInt32(str);
            return n;
        }
        */
        public string showSmsForInsertion(bool check,bool supress)
        {
            if (check)
            {
                string sms = "New Customer Inserted Successfully";
                if (!supress)
                {
                    MessageBox.Show(sms, "New Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                DGV_CUSTOMERS.DataSource = customer.getCustomers();
                return sms;
            }
            else
            {
                string sms = "Fail To Insert";
                if (!supress)
                {
                    MessageBox.Show(sms, "New Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return sms;
            }
        }

        public string deleteCustomer(bool supress, string customer_id)
        {
            string sms="00";

          //  if (TB_ID_CUSTOMER.Text == string.Empty)
          if(customer_id==string.Empty)
            {
               
                sms = "Select The Costumer To Delete";
                if(!supress)
                MessageBox.Show(sms, "Delete A Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return sms;
            }
            else
            {
              
                if (MessageBox.Show("do you really want to delete this Customer", "Remove Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sms = "Customer Deleted Successfully";

                    // customer.deleteCustomer(Convert.ToInt32(customer_id));
                    customer.deleteCustomer(bl.convertIntoint(customer_id));
                    DGV_CUSTOMERS.DataSource = customer.getCustomers();
                    if (!supress)
                        MessageBox.Show(sms, "Remove Customer");
                  
                    TB_ID_CUSTOMER.Text = "";
                    TB_FNAME.Text = "";
                    TB_LNAME.Text = "";
                    TB_TEL.Text = "";
                    TB_EMAIL.Text = "";
                   
                }
                return sms;
            }
        }

        public string updateCustomer(bool supress, string customer_id,string fname,string lname,string tel,string email)
        {

            string sms = "00";
            if (customer_id == string.Empty)
            {
                sms = "Select The Costumer To Update";
                if (!supress)
                    MessageBox.Show(sms, "Select A Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return sms;
            }
            else
            {
                int a = bl.convertIntoint(customer_id);
                bool check = customer.updateCustomer(a, fname, lname, tel, email);

                if (check)
                {
                    sms = "Customer Updated Successfully";
                    if (!supress)
                        MessageBox.Show(sms, "Update Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DGV_CUSTOMERS.DataSource = customer.getCustomers();
                }
                else
                {
                    sms = "Fail to update";
                    if (!supress)
                        MessageBox.Show(sms, "Update Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);


                }
                return sms;
            }
        }

       public string getCustomerFromDataGridView(string id,string fname,string lname,string tel,string email)
        {
            TB_ID_CUSTOMER.Text = id;
            TB_FNAME.Text = fname;
            TB_LNAME.Text = lname;
            TB_TEL.Text = tel;
            TB_EMAIL.Text =email;

            return TB_ID_CUSTOMER.Text;
        }
        

    }
}
