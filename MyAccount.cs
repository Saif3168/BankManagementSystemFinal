using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankManagementSystemFinal
{
    public partial class MyAccount : Form
    {
        private Connection connection = new Connection();
        public MyAccount()
        {
            InitializeComponent();
        }

        private void View_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection sqlConnection = connection.GetSqlConnection())
                {
                    string readCommand = "select * from userTable where accountID=@accountID";
                    SqlCommand sqlCommand = new SqlCommand(readCommand, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@accountID", IDBox.Text);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something Error in Data Search");
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection sqlConnection = connection.GetSqlConnection())
                {
                    string deleteCommand = "delete from userTable where AccountID = @AccountID";
                    SqlCommand sqlCommand = new SqlCommand(deleteCommand, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@accountID", IDBox.Text);
                    sqlCommand.ExecuteNonQuery();
                }

                MessageBox.Show("Data Deleted Successfully!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something Error in Data Delete");
            }
        }

        private void Deposite_Click(object sender, EventArgs e)
        {
            Deposite form = new Deposite();
            form.Show();
            this.Hide();
        }

        private void Withdraw_Click(object sender, EventArgs e)
        {
            Withdraw form = new Withdraw();
            form.Show();
            this.Hide();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Registration form = new Registration();
            form.Show();
            this.Hide();
        }
    }
}
