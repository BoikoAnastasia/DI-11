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
        public partial class Test : Window
        {
            private MySqlConnection connection;
            private string connectionString = "server=your_server;user id=your_username;password=your_password;database=your_database";

            public Test()
            {
                InitializeComponent();
                connection = new MySqlConnection(connectionString);
            }

            private void AddQuestion_Click(object sender, RoutedEventArgs e)
            {
                string questionText = name_Копировать1.Text;
                if (!string.IsNullOrEmpty(questionText))
                {
                    try
                    {
                        connection.Open();
                        string query = "INSERT INTO Questions (Text, TestId) VALUES (@Text, @TestId)";
                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Text", questionText);
                        command.Parameters.AddWithValue("@TestId", 1); 
                        command.ExecuteNonQuery();
                        MessageBox.Show("Вопрос успешно добавлен в базу данных.");
                        name_Копировать1.Text = string.Empty; 
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
                    MessageBox.Show("Пожалуйста, введите текст вопроса.");
                }
            }
        }
    }


