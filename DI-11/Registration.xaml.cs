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
        private const string ConnectionString = "YourConnectionStringHere";
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
                string userQuery = "INSERT INTO Users (Username, Password, GroupId) VALUES (@Username, @Password, (SELECT GroupId FROM Groups WHERE GroupName = @GroupName))";
                SqlCommand userCommand = new SqlCommand(userQuery, connection);
                userCommand.Parameters.AddWithValue("@Username", username);
                userCommand.Parameters.AddWithValue("@Password", password);
                userCommand.Parameters.AddWithValue("@GroupName", groupName);
                userCommand.ExecuteNonQuery();

                string groupQuery = "IF NOT EXISTS (SELECT 1 FROM Groups WHERE GroupName = @GroupName) INSERT INTO Groups (GroupName) VALUES (@GroupName)";
                SqlCommand groupCommand = new SqlCommand(groupQuery, connection);
                groupCommand.Parameters.AddWithValue("@GroupName", groupName);
                groupCommand.ExecuteNonQuery();
            }

            MessageBox.Show("Пользователь зарегистрирован: " + username + " в группе: " + groupName);
        }
    }
}
