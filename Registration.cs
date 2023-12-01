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
    public partial class Registration : Form
    {
        private Connection connection = new Connection();
        public Registration()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            try
            {
                string accountName = NameBox.Text.Trim();
                string accountType = TypeBox.Text.Trim();
                string gender = GBox.Text.Trim();
                string branch = BBox.Text.Trim();
                string mobileNumber = MobileBox.Text;
                string password = PasswordBox.Text;
                if (string.IsNullOrWhiteSpace(accountName) ||string.IsNullOrWhiteSpace(accountType) || 
                    string.IsNullOrWhiteSpace(gender)      ||string.IsNullOrWhiteSpace(branch)      || 
                    string.IsNullOrWhiteSpace(mobileNumber)||string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Please fill in all the required fields.");
                    return;
                }
                if (!IsAllLetters(accountName) || !IsAllLetters(branch) || !IsAllLetters(gender))
                {
                    MessageBox.Show("Name, Branch, and Gender should contain only characters.");
                    return;
                }
                double initialBalance;
                if (!double.TryParse(BalanceBox.Text, out initialBalance))
                {
                    MessageBox.Show("Invalid initial balance. Please enter a valid number.");
                    return;
                }
                if (initialBalance < 5000)
                {
                    MessageBox.Show("Initial balance should be at least 5000.");
                    return;
                }


                using (SqlConnection sqlConnection = connection.GetSqlConnection())
                {
                    var insertQuery = "insert into userTable values(@accountName, @accountID, @accountType, @gender, @branch, @initialBalance, @mobileNumber, @password)";

                    SqlCommand sqlCommand = new SqlCommand(insertQuery, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@accountName", NameBox.Text);
                    sqlCommand.Parameters.AddWithValue("@accountID", IDBox.Text);
                    sqlCommand.Parameters.AddWithValue("@accountType", TypeBox.Text);
                    sqlCommand.Parameters.AddWithValue("@gender", GBox.Text);
                    sqlCommand.Parameters.AddWithValue("@branch", BBox.Text);
                    sqlCommand.Parameters.AddWithValue("@initialBalance", double.Parse(BalanceBox.Text));
                    sqlCommand.Parameters.AddWithValue("@mobileNumber", MobileBox.Text);
                    sqlCommand.Parameters.AddWithValue("@password", PasswordBox.Text);

                    sqlCommand.ExecuteNonQuery();
                }

                MessageBox.Show("Data Inserted Successfully!!");

                NameBox.Clear();
                NameBox.Focus();
                IDBox.Clear();
                TypeBox.Clear();
                GBox.Clear();
                BBox.Clear();
                BalanceBox.Clear();
                MobileBox.Clear();
                PasswordBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong");
            }
        }
        private bool IsAllLetters(string input)
        {
            return input.All(char.IsLetter);
        }

        private void Login_Click(object sender, EventArgs e)
        {
            Login form = new Login();
            form.Show();
            this.Hide();
        }
    }
}
