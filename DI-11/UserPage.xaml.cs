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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DI_11
{
    /// <summary>
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Window
    {
        public UserPage(object id)
        {

            InitializeComponent();

            SqlConnection connection = new SqlConnection("Server=3218EC11\\SSQLSERVER;Database=ZelebobaCP_22.05.24;Trusted_Connection=Yes;");
            connection.Open();
            SqlCommand cmd = new SqlCommand($"SELECT * FROM Users where id='{id}'", connection);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                textName.Text = (reader.GetValue(0).ToString());
                textPhone.Text = (reader.GetValue(2).ToString());
                textSurname.Text = (reader.GetValue(1).ToString());
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
