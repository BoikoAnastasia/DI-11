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
        private string _connectionString = "Your Connection String Here"; // Замените на свою строку подключения
        private List<Question> _questions;
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
            _questions = new List<Question>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Запрос для получения вопросов из таблицы "Questions"
                string query = "SELECT Id, Text, Answer FROM Questions";
                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    _questions.Add(new Question
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Text = reader["Text"].ToString(),
                        Answer = reader["Answer"].ToString()
                    });
                }

                reader.Close();
            }
        }

        private void DisplayQuestion()
        {
            if (_currentQuestionIndex < _questions.Count)
            {
                Question currentQuestion = _questions[_currentQuestionIndex];
                questionTextBlock.Text = currentQuestion.Text;

                // Очистка ListBox
                answersListBox.Items.Clear();

                // Заполнение ListBox вариантами ответа (замените эти примеры на получение данных из базы данных)
                answersListBox.Items.Add("Вариант 1");
                answersListBox.Items.Add("Вариант 2");
                answersListBox.Items.Add("Вариант 3");
                answersListBox.Items.Add("Вариант 4");
            }
            else
            {
                ShowResult();
            }
        }

        private void CheckAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            // Получение выбранного ответа из ListBox
            string userAnswer = answersListBox.SelectedItem?.ToString() ?? string.Empty; //  Проверка на null

            Question currentQuestion = _questions[_currentQuestionIndex];

            if (userAnswer.ToLower() == currentQuestion.Answer.ToLower())
            {
                MessageBox.Show("Правильно!");
                _score++;
            }
            else
            {
                MessageBox.Show("Неправильно. Правильный ответ: " + currentQuestion.Answer);
            }

            _currentQuestionIndex++;
            //answersListBox.Clear(); // Очистка ListBox после ответа
            DisplayQuestion();
        }

        private void ShowResult()
        {
            MessageBox.Show($"Тест завершен. Ваш результат: {_score} из {_questions.Count}");
        }
            // Вы можете добавить здесь дополнительную логику,

    public class Question
        {
            public int Id { get; set; }
            public string Text { get; set; }
            public string Answer { get; set; }
        }
    }
}
