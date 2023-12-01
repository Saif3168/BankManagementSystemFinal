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
    public partial class Deposite : Form
    {
        private Connection connection = new Connection();
        public Deposite()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            int accountID = int.Parse(IDBox.Text);
            decimal depositeAmount = decimal.Parse(DepositeBox.Text);
            deposite(accountID, depositeAmount);

        }

        private void deposite(int accountID, decimal depositeAmount)
        {


            using (SqlConnection sqlConnection = connection.GetSqlConnection())

            {

                SqlCommand selectCommand = new SqlCommand("SELECT initialBalance FROM userTable WHERE accountID = @accountID", sqlConnection);
                selectCommand.Parameters.AddWithValue("@accountID", accountID);
                decimal currentBalance = (decimal)selectCommand.ExecuteScalar();

                decimal newBalance = currentBalance + depositeAmount;
                SqlCommand updateCommand = new SqlCommand("UPDATE userTable SET initialBalance=@newBalance WHERE accountID = @accountID", sqlConnection);
                updateCommand.Parameters.AddWithValue("@newBalance", newBalance);
                updateCommand.Parameters.AddWithValue("@accountId", accountID);
                updateCommand.ExecuteNonQuery();


                if (depositeAmount > 0)
                {
                    MessageBox.Show("Deposite successful. New balance: " + newBalance, "Success");
                    IDBox.Clear();
                    IDBox.Focus();
                    DepositeBox.Clear();

                }
                else
                {
                    MessageBox.Show("Deposite failed.", "Error");
                }
            }
        }


        private void Exit_Click(object sender, EventArgs e)
        {
            MyAccount form = new MyAccount();
            form.Show();
            this.Hide();
        }
    }
}
