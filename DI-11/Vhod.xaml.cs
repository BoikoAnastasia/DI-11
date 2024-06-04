using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DI_11
{
    /// <summary>
    /// Логика взаимодействия для Vhod.xaml
    /// </summary>
    public partial class Vhod : Window
    {
        private const string ConnectionString = "YourConnectionStringHere";

        public Vhod()
        {
            InitializeComponent();
        }

      

        private bool ValidateUser(string username, string password)
        {
            
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string passwordd = PasswordBox.Password;
                string usernname = name.Text;
                connection.Open();
                string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", usernname);
                command.Parameters.AddWithValue("@Password", passwordd);
                int count = (int)command.ExecuteScalar();
                return count > 0;



            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = name.Text;
            string password = PasswordBox.Password;

            if (ValidateUser(username, password))
            {
                MessageBox.Show("Вход выполнен успешно для пользователя: " + username);
                HomePage homePage = new HomePage();
                homePage.Show();
                Close();
                
            }
            else
            {
                MessageBox.Show("Неверное имя пользователя или пароль");
            }
        }





    }
}
