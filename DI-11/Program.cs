//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.EntityFrameworkCore;

//namespace DI_11
//{
//    class TestContext : DbContext
//    {
//        public DbSet<Question> Questions { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            optionsBuilder.UseSqlServer("Data Source=LAPTOP-V0AGQKUF\\SLAUUUIK;Initial Catalog=Test;Integrated Security=True;");
//        }
//    }

//    class Question
//    {
//        public int Id { get; set; }
//        public string Text { get; set; }
//        public string Answer { get; set; }
//    }

//    class Program
//    {
//        static void Main()
//        {
//            using (var context = new TestContext())
//            {
//                context.Database.EnsureCreated();
//                if (!context.Questions.Any())
//                {
//                    context.Questions.Add(new Question { Text = "Что такое 2 + 2?", Answer = "4" });
//                    context.Questions.Add(new Question { Text = "Сколько месяцев в году?", Answer = "12" });
//                    context.SaveChanges();
//                }
//                int score = 0;
//                var questions = context.Questions.ToList();
//                foreach (var question in questions)
//                {
//                    Console.WriteLine(question.Text);
//                    string userAnswer = Console.ReadLine();
//                    if (userAnswer.ToLower() == question.Answer.ToLower())
//                    {
//                        Console.WriteLine("Правильно!");
//                        score++;
//                    }
//                    else
//                    {
//                        Console.WriteLine("Неправильно. Правильный ответ: " + question.Answer);
//                    }
//                }
//                Console.WriteLine("Тест завершен. Ваш итоговый балл: " + score + " из " + questions.Count);
//            }
//        }
//    }
//}
