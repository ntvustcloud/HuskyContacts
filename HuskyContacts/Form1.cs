using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace HuskyContacts
{
    public partial class frmDashBoard : Form
    {
        Contact c = new Contact();
        public frmDashBoard()
        {
            InitializeComponent();
        }
        // regular expresstion string to check phone number is in correct form
        public const string phoneMotif = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";

        // regular expresstion string to check email is in correct form
        public const string emailMotif = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

        // check phone number is valid or not
        public static bool IsPhoneNbr(string number)
        {
            if (number != null) return Regex.IsMatch(number, phoneMotif);
            else return false;
        }
        //check email is valid or not
        public static bool isEmail(string emailStr) 
        {
            if(emailStr != null) return Regex.IsMatch(emailStr, emailMotif);
            else return false;
        }

        private void frmDashBoard_Load(object sender, EventArgs e)
        {
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            // check textbox first name is empty or not
            if (string.IsNullOrEmpty(txtFirstName.Text.Trim()))
            {
                errorProviderFirstName.SetError(txtFirstName, "Frist Name is required"); 
                return;
            }
            else 
            {
                errorProviderFirstName.SetError(txtFirstName, string.Empty);
            }
            //check textbox lastname is empty or not
            if (string.IsNullOrEmpty(txtLastName.Text.Trim()))
            {               
                errorProviderLastName.SetError(txtLastName, "Last Name is required");                
                return;
            }
            else
            {
                errorProviderLastName.SetError(txtLastName, string.Empty);
            }
            //check the textbox phone is empty or not
            if (string.IsNullOrEmpty(txtPhone.Text.Trim()))
            {
                errorProviderPhone.SetError(txtPhone, "Phone number is required");
                return;
            }
            else
            {
                errorProviderPhone.SetError(txtPhone, string.Empty);
                // valiadate phone number is in correct form
                if (IsPhoneNbr(txtPhone.Text) == false)
                {
                    MessageBox.Show("Invalid Phone number!");
                    return;
                };
            }
            // check textbox email is empty or not
            if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
            {
                errorProviderEmail.SetError(txtEmail, "Email is required");
                return;
            }
            else
            {
                errorProviderEmail.SetError(txtEmail, string.Empty);
                if (isEmail(txtEmail.Text) == false) 
                {
                    MessageBox.Show("Invalid email form!");
                    return;
                }
            }
            // get value from textbox 
            c.firstName = txtFirstName.Text;
            c.lastName = txtLastName.Text;
            c.street = txtStreet.Text;
            c.city = txtCity.Text;
            c.state = txtState.Text;
            c.zipCode = txtZipCode.Text;
            c.email = txtEmail.Text;
            c.phone = txtPhone.Text;

            // insert these value into table tbl_contacts in database pomodoro
            bool success = c.Insert(c);
            if (success == true)
            {
                MessageBox.Show("Add contact successfully!");
                DataTable dt = c.Select();
                // update datagridview with new data from database
                dgvContactList.DataSource = dt;

                // clear textbox after insert and reload the data
                txtID.Clear();
                txtFirstName.Clear();
                txtLastName.Clear();
                txtStreet.Clear();
                txtCity.Clear();
                txtState.Clear();
                txtZipCode.Clear();
                txtEmail.Clear();
                txtPhone.Clear();
            }
            else 
            {
                MessageBox.Show("Something wrong happen, cannot add contact to the contact list!");
            }

            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtID.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            txtStreet.Clear();
            txtCity.Clear();
            txtState.Clear();
            txtZipCode.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // check textbox first name is empty or not
            if (string.IsNullOrEmpty(txtFirstName.Text.Trim()))
            {
                errorProviderFirstName.SetError(txtFirstName, "Frist Name is required");
                return;
            }
            else
            {
                errorProviderFirstName.SetError(txtFirstName, string.Empty);
            }
            //check textbox lastname is empty or not
            if (string.IsNullOrEmpty(txtLastName.Text.Trim()))
            {
                errorProviderLastName.SetError(txtLastName, "Last Name is required");
                return;
            }
            else
            {
                errorProviderLastName.SetError(txtLastName, string.Empty);
            }
            //check the textbox phone is empty or not
            if (string.IsNullOrEmpty(txtPhone.Text.Trim()))
            {
                errorProviderPhone.SetError(txtPhone, "Phone number is required");
                return;
            }
            else
            {
                errorProviderPhone.SetError(txtPhone, string.Empty);
                // valiadate phone number is in correct form
                if (IsPhoneNbr(txtPhone.Text) == false) 
                {
                    MessageBox.Show("Invalid Phone number!");
                    return;
                };
            }
            // check textbox email is empty or not
            if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
            {
                errorProviderEmail.SetError(txtEmail, "Email is required");
                return;
            }
            else
            {
                errorProviderEmail.SetError(txtEmail, string.Empty);
                // valiadate email is in correct form
                if (isEmail(txtEmail.Text) == false)
                {
                    MessageBox.Show("Invalid email form!");
                    return;
                }
            }



            //get data from textbox into a temporary contact c
            c.Id = int.Parse(txtID.Text);
            c.firstName = txtFirstName.Text;
            c.lastName = txtLastName.Text;
            c.street = txtStreet.Text;
            c.city = txtCity.Text;
            c.state = txtState.Text;
            c.zipCode = txtZipCode.Text;
            c.email = txtEmail.Text;
            c.phone = txtPhone.Text;
            // update database with information contain in variable c
            bool success = c.Update(c);
            if (success == true) 
            {
                MessageBox.Show("Update contact successfully!");
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;

                // clear all text box after update successfull
                txtID.Clear();
                txtFirstName.Clear();
                txtLastName.Clear();
                txtStreet.Clear();
                txtCity.Clear();
                txtState.Clear();
                txtZipCode.Clear();
                txtEmail.Clear();
                txtPhone.Clear();
            }
            else 
            {
                MessageBox.Show("Something wrong happen, cannot update contact!");
            }
        }

        private void dgvContactList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // load data in the record of datagridview into the textbox
            int rowIndex = e.RowIndex;
            txtID.Text = dgvContactList.Rows[rowIndex].Cells[0].Value.ToString();
            txtFirstName.Text = dgvContactList.Rows[rowIndex].Cells[1].Value.ToString();
            txtLastName.Text = dgvContactList.Rows[rowIndex].Cells[2].Value.ToString();
            txtStreet.Text = dgvContactList.Rows[rowIndex].Cells[3].Value.ToString();
            txtCity.Text = dgvContactList.Rows[rowIndex].Cells[4].Value.ToString();
            txtState.Text = dgvContactList.Rows[rowIndex].Cells[5].Value.ToString();
            txtZipCode.Text = dgvContactList.Rows[rowIndex].Cells[6].Value.ToString();
            txtPhone.Text = dgvContactList.Rows[rowIndex].Cells[7].Value.ToString();
            txtEmail.Text = dgvContactList.Rows[rowIndex].Cells[8].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // get ID of contact which is need to deleted from datagridview
            c.Id = int.Parse(txtID.Text);
            bool success = c.Delete(c);
            if (success == true) 
            {
                MessageBox.Show("Deleted contact successfully!");
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;

                // clear all text box after deleted successfull
                txtID.Clear();
                txtFirstName.Clear();
                txtLastName.Clear();
                txtStreet.Clear();
                txtCity.Clear();
                txtState.Clear();
                txtZipCode.Clear();
                txtEmail.Clear();
                txtPhone.Clear();
            }
            else
            {
                MessageBox.Show("Something wrong happen, cannot Deleted this contact!");
            }
        }
        //create a connection string
        static string myConnStr = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString; 
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // got the keyword from search textbox
            string keyword = txtSearch.Text;

            // connect with database by create a connection with connection string just created above
            SqlConnection conn = new SqlConnection(myConnStr);

            //search query compare First Name, Last Name, Phone number, and Email with the keyword
            string sqlQuery = "SELECT * FROM tbl_contacts WHERE First_Name LIKE '%"+keyword+ "%' OR Last_Name LIKE '%"+keyword+ "%' OR Phone LIKE '%"+keyword+ "%' OR Email LIKE '%"+keyword+"%'";
            SqlDataAdapter sda = new SqlDataAdapter (sqlQuery, conn);


            // create a teporary table to contain the outcome
            DataTable dt = new DataTable();

            // fill the table with the result of searching
            sda.Fill(dt);

            // present the result in datagridview
            dgvContactList.DataSource = dt;

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
