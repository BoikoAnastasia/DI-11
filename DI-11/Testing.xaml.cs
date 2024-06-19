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
    public partial class Testing : Window
    {
        private readonly SqlConnection connection;
        private const string ConnectionString = "Data Source=LAPTOP-V0AGQKUF\\SLAUUUIK;Initial Catalog=Test;Integrated Security=True";

        public Testing()
        {
            InitializeComponent();
            connection = new SqlConnection(ConnectionString);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string testName = name.Text;
            string questionText = name_one.Text;
            string correctAnswerText = correctAnswerTextBox.Text;
            List<string> incorrectAnswers = new List<string> { correctAnswerTextBox.Text, incorrectAnswer1TextBox.Text, incorrectAnswer2TextBox.Text, incorrectAnswer3TextBox.Text };

            if (!string.IsNullOrEmpty(questionText) && !string.IsNullOrEmpty(correctAnswerText) && incorrectAnswers.All(answer => !string.IsNullOrEmpty(answer)))
            {
                try
                {
                    connection.Open();


                    string nameTest = name.Text;

                    string questionQuery = "INSERT INTO Questions (Text, Name, CorrectAnswer) VALUES (@Text, @Name, @CorrectAnswer); SELECT SCOPE_IDENTITY();";
                    SqlCommand questionCommand = new SqlCommand(questionQuery, connection);
                    questionCommand.Parameters.AddWithValue("@Text", questionText);
                    questionCommand.Parameters.AddWithValue("@Name", testName);
                    questionCommand.Parameters.AddWithValue("@CorrectAnswer", correctAnswerText);

                    int questionId = Convert.ToInt32(questionCommand.ExecuteScalar());

                    string correctAnswerQuery = "INSERT INTO Test (QuestionsId, Text) VALUES (@QuestionsId, @Text)";
                    SqlCommand correctAnswerCommand = new SqlCommand(correctAnswerQuery, connection);
                    correctAnswerCommand.Parameters.AddWithValue("@QuestionsId", questionId);
                    correctAnswerCommand.Parameters.AddWithValue("@Text", correctAnswerText);
                    correctAnswerCommand.ExecuteNonQuery();

                    foreach (var incorrectAnswer in incorrectAnswers)
                    {
                        string incorrectAnswerQuery = "INSERT INTO Answer (QuestionsId, Text) VALUES (@QuestionsId, @Text)";
                        SqlCommand incorrectAnswerCommand = new SqlCommand(incorrectAnswerQuery, connection);
                        incorrectAnswerCommand.Parameters.AddWithValue("@QuestionsId", questionId);
                        incorrectAnswerCommand.Parameters.AddWithValue("@Text", incorrectAnswer);
                        incorrectAnswerCommand.ExecuteNonQuery();
                    }

                    MessageBox.Show("Вопрос успешно добавлен в базу данных.");
                    name_one.Text = string.Empty;
                    correctAnswerTextBox.Text = string.Empty;
                    incorrectAnswer1TextBox.Text = string.Empty;
                    incorrectAnswer2TextBox.Text = string.Empty;
                    incorrectAnswer3TextBox.Text = string.Empty;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при добавлении вопроса: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите текст вопроса, правильный ответ и все варианты неправильных ответов.");
            }
        }
        
        

      

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            HomePage homePage = new HomePage();
            homePage.Show();
            Close();

        }
    }
}