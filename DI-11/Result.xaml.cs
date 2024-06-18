using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;

namespace DI_11
{
    public partial class Result : Window
    {
        public Result()
        {
            InitializeComponent();

            int result = 85;
            resultLabel.Content = $"Вы набрали {result} баллов из 100. Ваша оценка: {CalculateGrade(result)}";

            string connectionString = "Data Source=YourServer;Initial Catalog=YourDatabase;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO TestResults (Score) VALUES (@Score)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Score", result);
                command.ExecuteNonQuery();
            }
        }

        private string CalculateGrade(int result)
        {
            if (result >= 90)
                return "A";
            else if (result >= 80)
                return "B";
            else if (result >= 70)
                return "C";
            else if (result >= 60)
                return "D";
            else
                return "F";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HomePage homePage = new HomePage();
            homePage.Show();
            this.Close();
        }
    }
}

