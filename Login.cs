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
    public partial class Login : Form
    {
        private Connection connection = new Connection();
        public Login()
        {
            InitializeComponent();
        }

        private void Log_in_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlConnection = connection.GetSqlConnection())
            {
               
                int accountID;
                accountID = int.Parse(IDBox.Text);
                string password;
                password = PasswordBox.Text;

                string query = "SELECT * FROM userTable WHERE accountID = @accountID AND password = @password";


                using  (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                   
                    command.Parameters.AddWithValue("@accountID", accountID);
                    command.Parameters.AddWithValue("@password", password);
                    SqlDataReader reader = command.ExecuteReader();


                    if (reader.Read())
                    {
                        accountID = int.Parse(IDBox.Text);
                        password = PasswordBox.Text;
                        MyAccount form = new MyAccount();
                        form.Show();
                        this.Hide();

                    }
                    else
                    {
                        MessageBox.Show("Incorrect AccountID or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        IDBox.Clear();
                        PasswordBox.Clear();
                        IDBox.Focus();
                    }
                }


            }
        }
    }
}
