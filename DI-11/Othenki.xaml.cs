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
    /// <summary>
    /// Логика взаимодействия для Othenki.xaml
    /// </summary>
    public partial class Othenki : Window
    {
        private string _connectionString = "Data Source=LAPTOP-V0AGQKUF\\SLAUUUIK;Initial Catalog=Test;Integrated Security=True";

        public Othenki()
        {
            InitializeComponent();
        }

        private void ShowScoresButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<TestResults> scores = GetScoresFromDatabase();

                ScoresDataGrid.ItemsSource = scores;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private List<TestResults> GetScoresFromDatabase()
        {
            List<TestResults> scores = new List<TestResults>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT Id, Score FROM TestResults";
                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    TestResults score = new TestResults
                    {
                        Id = Convert.ToInt32(reader["Id"]), 
                        Score = (int)reader["Score"]
                    };

                    scores.Add(score);
                }

                reader.Close();
            }

            return scores;
        }
    }

    public class TestResults
    {
        public int Id { get; set; }
        public int Score { get; set; }
    }
}