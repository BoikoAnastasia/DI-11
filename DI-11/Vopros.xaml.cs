using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DI_11
{
    public partial class Vopros : Window
    {
        private string _connectionString = "Data Source=LAPTOP-V0AGQKUF\\SLAUUUIK;Initial Catalog=Test;Integrated Security=True";
        private List<Questions> _questions;
        private int _currentQuestionIndex = 0;
        private int _score = 0;

        public Vopros()
        {
            InitializeComponent();
            LoadQuestions();
            DisplayQuestion();
        }

        private void LoadQuestions()
{
    _questions = new List<Questions>();

    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        connection.Open();

        // Запрос для получения вопросов из таблицы "Questions"
        string query = "SELECT q.Id, q.Text, q.CorrectAnswer, a.Id AS AnswerId, a.Text AS AnswerText " +
                      "FROM Questions q " +
                      "JOIN Answer a ON q.Id = a.QuestionsId";
        SqlCommand command = new SqlCommand(query, connection);

        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            var questionId = Convert.ToInt32(reader["Id"]);
            var question = _questions.FirstOrDefault(q => q.Id == questionId);

            if (question == null)
            {
                question = new Questions
                {
                    Id = questionId,
                    Text = reader["Text"].ToString(),
                    CorrectAnswer = reader["CorrectAnswer"].ToString()
                };
                _questions.Add(question);
            }

            question.Answers.Add(new Answer
            {
                Id = Convert.ToInt32(reader["AnswerId"]),
                Text = reader["AnswerText"].ToString()
            });
        }

        reader.Close();
    }
}
        private void DisplayQuestion()
        {
            if (_currentQuestionIndex < _questions.Count)
            {
                Questions currentQuestion = _questions[_currentQuestionIndex];
                questionTextBlock.Text = currentQuestion.Text;

                // Очистка ListBox
                answersListBox.Items.Clear();

                // Заполнение ListBox вариантами ответа
                foreach (var answer in currentQuestion.Answers)
                {
                    answersListBox.Items.Add(answer.Text);
                }
            }
            else
            {
                ShowResult();
            }
        }

        private void CheckAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            // Получение выбранного ответа из ListBox
            string userAnswer = answersListBox.SelectedItem?.ToString() ?? string.Empty;

            var question = _questions[_currentQuestionIndex];

            // Находим правильный ответ в списке Answers
            Answer correctAnswer = question.Answers.FirstOrDefault(a => a.Text.ToLower() == question.CorrectAnswer.ToLower());

            if (userAnswer.ToLower() == correctAnswer.Text.ToLower())
            {
                MessageBox.Show("Правильно!");
                _score++;
                HomePage homePage = new HomePage();
                homePage.Show();
                Close();
            }
            else
            {
                MessageBox.Show(" Правильный ответ: " + correctAnswer.Text);
                _score++;
                HomePage homePage = new HomePage();
                homePage.Show();
                Close();
            }

            _currentQuestionIndex++;
            answersListBox.Items.Clear();
            DisplayQuestion();
        }

        private void ShowResult()
        {
            MessageBox.Show($"Тест завершен. Ваш результат: {_score} из {_questions.Count}");
            SaveResultToDatabase(_score);
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

        public class Questions
        {
            public int Id { get; set; }
            public string Text { get; set; }
            public string CorrectAnswer { get; set; }
            public List<Answer> Answers { get; set; } = new List<Answer>();
        }

        public class Answer
        {
            public int Id { get; set; }

            public string Text { get; set; }
        }
        public class TestResult 
        {
            public int Id { get; set; }
            public string UserName { get; set; }
            public int Score { get; set; }
            public DateTime DateTaken { get; set; }
        }

    }
}