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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        private const string ConnectionString = "Data Source=3218EC08;Initial Catalog=Test;Integrated Security=True";
        public Registration()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = name.Text;
            string groupName = group.Text;
            string password = PasswordBox.Password;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string userQuery = "INSERT INTO Users (Username, Password, GroupName) VALUES (@Username, @Password, @GroupName)";
                SqlCommand userCommand = new SqlCommand(userQuery, connection);
                userCommand.Parameters.AddWithValue("@Username", username);
                userCommand.Parameters.AddWithValue("@Password", password);
                userCommand.Parameters.AddWithValue("@GroupName", groupName);
                userCommand.ExecuteNonQuery();
            }
            MessageBox.Show("Пользователь зарегистрирован: " + username + " в группе: " + groupName);
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}

