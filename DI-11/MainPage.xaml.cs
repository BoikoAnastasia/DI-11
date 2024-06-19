using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Window
    {
        public const string ConnectionString = "Server=3218EC11\\SSQLSERVER;Database=ZelebobaCP_22.05.24;Trusted_Connection=Yes;";
        public MainPage()
        {
            InitializeComponent();

            SqlConnection connection = new SqlConnection("Server=3218EC11\\SSQLSERVER;Database=ZelebobaCP_22.05.24;Trusted_Connection=Yes;");

            connection.Open();

            SqlCommand command = new SqlCommand("SELECT Name, Id, Surname, PhoneNumber, OrderNumber, Email FROM Clients", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            DataClients.ItemsSource = dataTable.DefaultView;


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var userPage = new UserPage();
            userPage.Show();
            this.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT Name, Id, Surname, PhoneNumber, OrderNumber, Email FROM Clients", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                DataClients.ItemsSource = dataTable.DefaultView;
            }
        }
    }
}
