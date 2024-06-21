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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       public object id = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs ex)
        {
            try
            {

                SqlConnection connection = new SqlConnection("Server=3218EC11\\SSQLSERVER;Database=ZelebobaCP_22.05.24;Trusted_Connection=Yes;");
                connection.Open();
                SqlCommand cmd = new SqlCommand($"SELECT * FROM Users where Name='{Login.Text}' and Password='{Password.Text}'", connection);
                SqlDataReader reader = cmd.ExecuteReader();

                int i = 0;

                while (reader.Read())
                {
                    i++;
                    id = reader[4];
                }

                if (i == 1)
                {
                    var MainPage = new MainPage(id);
                    MainPage.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("INCORRECT PASSWORD OR USER ID", "Authentication Failed");
                }
                connection.Close();
            }

            catch (Exception err)
            {
                Console.WriteLine("Catch Block = " + err);
            }

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

    }
}
