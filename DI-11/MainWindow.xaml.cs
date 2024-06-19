using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
        private SqlDataReader reader;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=3218EC15;Initial Catalog=Ilya;Trusted_Connection=True;");
            conn.Open();
            SqlCommand createCommand = new SqlCommand($"SELECT * from Users where FIO = '{login.Text}' and password='{password.Password}'", conn);
            reader = createCommand.ExecuteReader();
            int i = 0;
            while (reader.Read())
            {
                i++;
            }
            if (i == 1)
            {
                BlackWin blackWin = new BlackWin();
                blackWin.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("INCORRECT PASSWORD OR USER ID", "Authentication Failed");
            }
          
        }
        
    }
}
