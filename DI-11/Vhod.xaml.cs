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
    using System.Data.SqlClient;

    public partial class Vhod : Window
    {
        private const string ConnectionString = "Data Source=3218EC08;Initial Catalog=Test;Integrated Security=True";
        public Vhod()
        {
            InitializeComponent();
        }

        private bool ValidateUser(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);
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

