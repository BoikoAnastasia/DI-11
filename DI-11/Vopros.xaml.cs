using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;

namespace DI_11
{
    public class TestContext : DbContext
    {
        public DbSet<Question> Questions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=LAPTOP-V0AGQKUF\\SLAUUUIK;Initial Catalog=Test;Integrated Security=True;");
        }
    }

    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<Answer> Answers { get; set; } = new List<Answer>(); 
        public string CorrectAnswer { get; set; }
    }

    public class Answer
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }

    public partial class Vopros : Window
    {
        private TestContext _context;
        private int _score = 0;
        private int _questionIndex = 0;
        private List<Question> _questions;

        public Vopros(TestContext context)
        {
            _context = context;
            InitializeComponent();

            try
            {
                _questions = _context.Questions
                    .Include(q => q.Answers) 
                    .ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке вопросов: {ex.Message}");
            }

            DisplayQuestion();
        }

        private void DisplayQuestion()
        {
            if (_questionIndex < _questions.Count)
            {
                var question = _questions[_questionIndex];
                questionTextBlock.Text = question.Text;

                answersListBox.Items.Clear();

                foreach (var answer in question.Answers)
                {
                    answersListBox.Items.Add(answer.Text);
                }
            }
            else
            {
                ShowResult();
            }
        }

        private void ShowResult()
        {
            MessageBox.Show($"Тест завершен. Ваш итоговый балл: {_score} из {_questions.Count}");
            Close();
        }

        private void AnswerButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранный ответ из ListBox
            string userAnswer = answersListBox.SelectedItem?.ToString() ?? string.Empty;

            var question = _questions[_questionIndex];

            if (userAnswer.ToLower() == question.CorrectAnswer.ToLower())
            {
                MessageBox.Show("Правильно!");
                _score++;
            }
            else
            {
                MessageBox.Show("Неправильно. Правильный ответ: " + question.CorrectAnswer);
            }

            _questionIndex++;
            answersListBox.Items.Clear();
            DisplayQuestion();
        }
    }
}