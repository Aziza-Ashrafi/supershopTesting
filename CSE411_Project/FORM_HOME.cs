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
    public partial class FORM_HOME : Form
    {
        public string userType;
        public FORM_HOME()
        {
            InitializeComponent();
 
        }


        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FORM_MANAGE_CUSTOMER fmcm = new FORM_MANAGE_CUSTOMER();
            fmcm.ShowDialog();
        }

       

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FORM_MANAGE_PRODUCT fmp = new FORM_MANAGE_PRODUCT();
            fmp.ShowDialog();
        }
    }
}
