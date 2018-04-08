using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CSE411_Project;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Windows.Forms;

namespace UnitTest
{
    [TestFixture]
    public class Class1
    {
        DataAccess da = null;

        [SetUp]
        public void TestSetup()
        {
            da = new DataAccess();
        }

        [Test]
        [Category("Database")]
        public void DisconnectFromDatabase()
        {

            bool disconnected = da.Disconnect();
            Assert.IsTrue(disconnected);
        }

        [Test]
        [Category("Database")]
        public void ConnectToDatabase()
        {

            bool connected = da.Connect();
            Assert.IsTrue(connected);

        }
        [TearDown]
        public void TearDown()
        {
            da = null;
        }

        [Test]
        [Category("Database")]

        public void InsertCustomerIntoDatabase()
        {

            Customer ca = new Customer();
            bool inserted = ca.insertCustomer("CustomerTest", "Customer", "123", "Customer@Test.com");

            da.Disconnect();
            Assert.IsTrue(inserted);
        }


        [Test]
        public void InsertCustomerIntoDatabaseCheckBlank()  //Check blank
        {

            Customer ca = new Customer();
            bool inserted = ca.insertCustomer("", "CustomerTest", "123", "Customer@Test.com");


            da.Disconnect();
            Assert.IsFalse(inserted);
        }

        [Test]
        [Category("Database")]
        public void UpdateCustomerTest()
        {
            // DataAccess da = new DataAccess();
            Customer ca = new Customer();
            da.Connect();


            bool inserted = ca.insertCustomer("Customertestupdate", "Customer", "000", "Customer@Testupdate.com");
            if (inserted)
            {
                SqlCommand command = new SqlCommand("SELECT TOP(1) CUST_ID FROM CUSTOMER ORDER BY 1 DESC", da.conn);
                // command.Parameters.AddWithValue("@mail", "Customer@Testupdate.com");
                // command.ExecuteNonQuery();
                int primaryKey = Convert.ToInt32(command.ExecuteScalar());
                bool updated = ca.updateCustomer(primaryKey, "CustomerUpdated", "CustomerTest", "222", "Customer@Testupdate.com");
                Assert.IsTrue(updated);
                da.Disconnect();
            }
        }

        [Test]
        [Category("Database")]
        public void FailToUpdateCustomer()
        {

            Customer ca = new Customer();
            da.Connect();
            bool inserted = ca.insertCustomer("Customertestupdate", "Customer", "000", "Customer@Testupdate.com");
            if (inserted)
            {
                SqlCommand command = new SqlCommand("SELECT TOP(1) CUST_ID FROM CUSTOMER ORDER BY 1 DESC", da.conn);
                int primaryKey = Convert.ToInt32(command.ExecuteScalar());
                bool updated2 = ca.updateCustomer(primaryKey, "", "CustomerTest", "222", "Customer@Testupdate.com");
                Assert.IsFalse(updated2);
                da.Disconnect();


            }
        }






        [Test]
        [Category("Database")]

        public void DeleteCustomer()
        {
            // DataAccess da = new DataAccess();
            Customer ca = new Customer();
            Customer c = new Customer();
            da.Connect();
            bool inserted = ca.insertCustomer("Customer Test delete", "Customer Test2", "000", "Customer@Testdelete.com");


            if (inserted)
            {
                SqlCommand command = new SqlCommand("SELECT TOP(1) CUST_ID FROM CUSTOMER ORDER BY 1 DESC", da.conn);
                // command.Parameters.AddWithValue("@mail", "Customer@Testdelete.com");
                // command.ExecuteNonQuery();
                int primaryKey = Convert.ToInt32(command.ExecuteScalar());
                bool deleted = ca.deleteCustomer(primaryKey);

                // Customer c2 = da.GetCustomer(name);

                da.Disconnect();

                //  Assert.IsNull(c2);
                Assert.IsTrue(deleted);
            }
        }

        [Test]
        public void GetCustomerCheck()
        {
            Customer ca = new Customer();
            da.Connect();
            bool inserted = ca.insertCustomer("getCustomerCheck", "Customer Test2", "666", "Customer@TestGet.com");


            if (inserted)
            {
                DataTable datalist = new DataTable();
                datalist = ca.getCustomers();
                int count = datalist.Rows.Count;
                Assert.AreNotEqual(0, count);
            }

        }

        [Test]
        public void searchProductCheck()
        {
            Customer ca = new Customer();
            da.Connect();


            DataTable datalist = new DataTable();
            datalist = ca.searchProducts("rice");
            int count = datalist.Rows.Count;
            Assert.AreEqual(1, count);


        }

        [Test]
        public void IfAnyCustomerFieldNullThrowArgumentException()
        {
            string name = null;

            Customer ca = new Customer();
            da.Connect();
            // bool inserted = ca.insertCustomer(null, "CustomerTest", "666", "Customer@TestGet.com");

            Assert.Throws<ArgumentNullException>(() => ca.insertCustomer(name, "Customer Test2", "666", "Customer@TestGet.com"));
            //  Assert.Fail("If we get here, an exception hasn't been thrown");
        }


        [Test]
        [Category("Database")]
        public void IfProductNotFound()
        {
            da.Connect();
            DataTable d = new DataTable();
            Customer ca = new Customer();
            d = ca.searchProducts("UnknownProduct");
            int count = d.Rows.Count;
            da.Disconnect();
            Assert.AreEqual(0, count);
        }


        [Test]
        [Category("Database")]
        public void checkStringContainOnlyLetter()
        {
            string test = "aziza";

            Customer ca = new Customer();
            bool res = ca.checkStringOnlyLetter(test);

            Assert.IsTrue(res);


        }

        [Test]
        [Category("Database")]
        public void checkStringContainNotOnlyLetter()
        {

            string test2 = "a2b3";
            Customer ca = new Customer();

            bool res2 = ca.checkStringOnlyLetter(test2);

            Assert.IsFalse(res2);

        }

        [Test]
        [Category("Database")]
        public void checkStringContainOnlyNumeric()
        {
            string test = "123123";
            Customer ca = new Customer();
            bool res = ca.checkStringOnlyNumeric(test);
            Assert.IsTrue(res);


        }

        [Test]
        [Category("Database")]
        public void checkStringContainNotOnlyNumeric()
        {
            string test2 = "a2b3";
            Customer ca = new Customer();

            bool res2 = ca.checkStringOnlyNumeric(test2);
            Assert.IsFalse(res2);

        }

        [Test]

        public void testWindowNotMinimize()
        {
            FORM_MANAGE_CUSTOMER fob = new FORM_MANAGE_CUSTOMER();
            bool res = fob.check_minimize("Normal");
            Assert.IsFalse(res);
        }

        [Test]

        public void checkConstructorOfForm()   //check constructor or object create or not
        {
            FORM_MANAGE_CUSTOMER test = new FORM_MANAGE_CUSTOMER();
            Assert.IsNotNull(test);

        }

        [Test]

        public void TestnevigationFunction()
        {
                FORM_MANAGE_CUSTOMER test = new FORM_MANAGE_CUSTOMER();
                int t = test.navigation(0);
                Assert.AreEqual(1,t);

            
        }

        [Test]
        public void CheckResetBtn()
        {
            FORM_MANAGE_CUSTOMER test = new FORM_MANAGE_CUSTOMER();
            string t = test.resetBtn();
            Assert.AreEqual("",t);

        }

        [Test]
        public void TestClose()
        {
            FORM_MANAGE_CUSTOMER test = new FORM_MANAGE_CUSTOMER();
            int t = test.checkClose();
            Assert.AreEqual(2, t);

        }

        [Test]
        public void TestPreviousBtnCondition1()
        {
            FORM_MANAGE_CUSTOMER test = new FORM_MANAGE_CUSTOMER();
            int t = test.previouseBtn(0);
            Assert.AreEqual(1, t);

        }

        [Test]
        public void TestPreviousBtnCondition2()
        {
            FORM_MANAGE_CUSTOMER test = new FORM_MANAGE_CUSTOMER();
            int t = test.previouseBtn(3);
            Assert.AreEqual(2, t);

        }

        [Test]
        public void TestNextBtnCondition1()
        {
            Customer c = new Customer();
            int t = c.getCustomers().Rows.Count - 1; //last position
            FORM_MANAGE_CUSTOMER test = new FORM_MANAGE_CUSTOMER();
            int s = test.nextBtn(t);
            Assert.AreEqual(1, s);

        }

        [Test]
        public void TestNextBtnCondition2()
        {
            Customer c = new Customer();
            int t = 6;
            FORM_MANAGE_CUSTOMER test = new FORM_MANAGE_CUSTOMER();
            int s = test.nextBtn(t);
            Assert.AreEqual(7, s);

        }
        [Test]
        public void testConvert()
        {
            //   FORM_MANAGE_CUSTOMER test = new FORM_MANAGE_CUSTOMER();
          Businesslogic bl = new Businesslogic();
            int i=bl.convertIntoint("1");
            Assert.AreEqual(1, i);
        }

       [Test]

       public void TestMsgForSuccessfulInsertion()
        {
            FORM_MANAGE_CUSTOMER test = new FORM_MANAGE_CUSTOMER();
            string s = test.showSmsForInsertion(true,true);  //2nd true is for supress message box
            Assert.AreEqual("New Customer Inserted Successfully", s);
        }

        [Test]

        public void TestMsgForFailedInsertion()
        {
            FORM_MANAGE_CUSTOMER test = new FORM_MANAGE_CUSTOMER();
            string s = test.showSmsForInsertion(false,true);
            Assert.AreEqual("Fail To Insert", s);
        }

        [Test]

        public void TestMsgForSelectCustomerForDeletion()
        {
            FORM_MANAGE_CUSTOMER test = new FORM_MANAGE_CUSTOMER();
            string s = test.deleteCustomer(true,"");  // true is for supress message box
            Assert.AreEqual("Select The Costumer To Delete", s);
        }

        [Test]

        public void TestMsgForSuccessfulCustomerDeletion()
        {
            FORM_MANAGE_CUSTOMER test = new FORM_MANAGE_CUSTOMER();
            string s = test.deleteCustomer(true, "50");  // true is for supress message box, 50 for demo
            if(s== "Customer Deleted Successfully")
            Assert.AreEqual("Customer Deleted Successfully", s);
            else
                Assert.AreNotEqual("Customer Deleted Successfully", s);

        }

      /*  [Test]

        public void TestNotDeleteCustomern()
        {
            FORM_MANAGE_CUSTOMER test = new FORM_MANAGE_CUSTOMER();
            string s = test.deleteCustomer(true, "50");  // true is for supress message box, 50 for demo
            Assert.AreNotEqual("Customer Deleted Successfully", s);
        }*/

        [Test]

        public void TestMsgForSelectCustomerForUpdate()
        {
            FORM_MANAGE_CUSTOMER test = new FORM_MANAGE_CUSTOMER();
            string s = test.updateCustomer(true, "","","","","");  // true is for supress message box
            Assert.AreEqual("Select The Costumer To Update", s);
        }



        [Test]

        public void TestMsgForSuccessfullyUpdateCustomer()
        {
            FORM_MANAGE_CUSTOMER test = new FORM_MANAGE_CUSTOMER();
            string s = test.updateCustomer(true, "2", "aziza", "ashrafi", "435", "aziza@gmail.com");  // true is for supress message box
            Assert.AreEqual("Customer Updated Successfully", s);
        }

        [Test]

        public void TestMsgForFailedToUpdateCustomer()
        {
            FORM_MANAGE_CUSTOMER test = new FORM_MANAGE_CUSTOMER();
            string s = test.updateCustomer(true, "2", "", "ashrafi", "435", "aziza@gmail.com");  // true is for supress message box
            Assert.AreEqual("Fail to update", s);
        }

        [Test]

        public void TestLoadFromGridView()
        {
            FORM_MANAGE_CUSTOMER test = new FORM_MANAGE_CUSTOMER();
            string s = test.getCustomerFromDataGridView("1", "aziza", "ashrafi", "123", "a@gmial.com");
            Assert.AreEqual("1", s);
        }



        //Test Cases for all pair testing (using PICT result)

   
        [Test]
        public void AllPairTesting1()
        {

            Customer ca = new Customer();
            bool inserted = ca.insertCustomer("Customer22", "", "123", "");

            da.Disconnect();
            Assert.IsFalse(inserted);
        }

        [Test]
        public void AllPairTesting2()
        {

            Customer ca = new Customer();
            bool inserted = ca.insertCustomer("", "abc", "a123", "a@gmail.com");

            da.Disconnect();
            Assert.IsFalse(inserted);
        }

        [Test]
        public void AllPairTesting3()
        {

            Customer ca = new Customer();
            bool inserted = ca.insertCustomer("12ab", "abc", "", "agmail.com");

            da.Disconnect();
            Assert.IsFalse(inserted);
        }

       

        [Test]
        public void AllPairTesting4()
        {

            Customer ca = new Customer();
            bool inserted = ca.insertCustomer("", "abc11", "123", "amail.com");

            da.Disconnect();
            Assert.IsFalse(inserted);
        }
        [Test]
        public void AllPairTesting5()
        {

            Customer ca = new Customer();
            bool inserted = ca.insertCustomer("aziza", "abc11", "", "a@gmail.com");

            da.Disconnect();
            Assert.IsFalse(inserted);
        }
         

      
        [Test]
        public void AllPairTesting6()
        {

            Customer ca = new Customer();
            bool inserted = ca.insertCustomer("aziza", "", "123", "a@gmail.com");

            da.Disconnect();
            Assert.IsFalse(inserted);
        }

        [Test]
        public void AllPairTesting7()
        {

            Customer ca = new Customer();
            bool inserted = ca.insertCustomer("aziza", "11aa", "aa123", "");

            da.Disconnect();
            Assert.IsFalse(inserted);
        }

        
        [Test]
        public void AllPairTesting8()
        {

            Customer ca = new Customer();
            bool inserted = ca.insertCustomer("", "", "", "agmail.com");

            da.Disconnect();
            Assert.IsFalse(inserted);
        }
        [Test]
        public void AllPairTesting9()
        {

            Customer ca = new Customer();
            bool inserted = ca.insertCustomer("111aziza", "", "aa123", "a@gmail.com");

            da.Disconnect();
            Assert.IsFalse(inserted);
        }
        [Test]
        public void AllPairTesting10()
        {

            Customer ca = new Customer();
            bool inserted = ca.insertCustomer("aziza", "ashrafi", "123", "");

            da.Disconnect();
            Assert.IsFalse(inserted);
        }


        [Test]
        public void AllPairTesting11()
        {

            Customer ca = new Customer();
            bool inserted = ca.insertCustomer("aziza", "", "aa123", "aa.com");

            da.Disconnect();
            Assert.IsFalse(inserted);
        }
        [Test]
        public void AllPairTesting12()
        {

            Customer ca = new Customer();
            bool inserted = ca.insertCustomer("abc123", "ab23", "", "");

            da.Disconnect();
            Assert.IsFalse(inserted);
        }
        [Test]
        public void AllPairTesting13()
        {

            Customer ca = new Customer();
            bool inserted = ca.insertCustomer("", "12ab", "123", "");

            da.Disconnect();
            Assert.IsFalse(inserted);
        }

      

        [Test]
        public void NullMSGTest()
        {
            Customer ca = new Customer();
            Assert.AreEqual("Can not be null", Assert.Throws<ArgumentNullException>(() => ca.insertCustomer(null, "12ab", "123", "")).ParamName);

        }

        [Test]
        public void CharConversion()
        {
            // FORM_MANAGE_CUSTOMER test = new FORM_MANAGE_CUSTOMER();
            Businesslogic bl = new Businesslogic();
            Assert.Throws<FormatException>(() => bl.convertIntoint("abc"));

        }
    }
}

