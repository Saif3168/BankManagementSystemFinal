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
    public partial class Withdraw : Form
    {
        private Connection connection = new Connection();
        public Withdraw()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            MyAccount form = new MyAccount();
            form.Show();
            this.Hide();
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            int accountID = int.Parse(IDBox.Text);
            decimal withdrawalAmount = decimal.Parse(WithdrawBox.Text);
            withdraw(accountID, withdrawalAmount);
        }

        private void withdraw(int accountID, decimal withdrawalAmount)
        {
            using (SqlConnection sqlConnection = connection.GetSqlConnection())
            {
                SqlCommand selectCommand = new SqlCommand("SELECT initialBalance FROM userTable WHERE accountID = @accountID", sqlConnection);
                selectCommand.Parameters.AddWithValue("@accountID", accountID);
                decimal currentBalance = (decimal)selectCommand.ExecuteScalar();

                if (currentBalance >= withdrawalAmount)
                {
                    decimal newBalance = currentBalance - withdrawalAmount;
                    SqlCommand updateCommand = new SqlCommand("UPDATE userTable SET initialBalance = @newBalance WHERE accountID = @accountID", sqlConnection);
                    updateCommand.Parameters.AddWithValue("@newBalance", newBalance);
                    updateCommand.Parameters.AddWithValue("@accountID", accountID);

                    updateCommand.ExecuteNonQuery();

                    if (withdrawalAmount > 0)
                    {
                        MessageBox.Show("Withdrawa successful. New balance: " + newBalance, "Success");
                        IDBox.Clear();
                        IDBox.Focus();
                        WithdrawBox.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Withdrawa failed.", "Error");
                    }
                }
                else
                {
                    MessageBox.Show("Insufficient balance.", "Error");
                }
            }
        }
    }
}
