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
        private int _result; 

        public Result(int result)
        {
            InitializeComponent();
            _result = result;

            
            resultLabel.Content = $"Вы набрали {_result} баллов из 100. Ваша оценка: {CalculateGrade(_result)}";

            SaveResultToDatabase(_result);
        }

       

        private void SaveResultToDatabase(int result)
        {
            string connectionString = "Data Source=LAPTOP-V0AGQKUF\\SLAUUUIK;Initial Catalog=Test;Integrated Security=True";

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
            if (result >= 5)
                return "A";
            else if (result >= 4)
                return "B";
            else if (result >= 3)
                return "C";
            else if (result >= 2)
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

