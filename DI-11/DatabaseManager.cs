using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DI_11
{
    public class Contact
    {
        public string ContactId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class DatabaseManager
    {
        private SQLiteConnection connection;

        public static int UserId { get; set; }
        public static string UserName { get; set; }

        public DatabaseManager()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            connection = new SQLiteConnection(connectionString);
        }

        public bool CreateUser(string username, string password)
        {
            connection.Open();
            string hashedPassword = HashPassword(password);
            string query = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password)";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", hashedPassword);

            int result = command.ExecuteNonQuery();
            connection.Close();

            return result > 0;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public bool AddContact(int userId, string name, string phoneNumber)
        {
            try
            {
                connection.Open();
                string query = "INSERT INTO Contacts (UserId, Name, PhoneNumber) VALUES (@UserId, @Name, @PhoneNumber)";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);

                int result = command.ExecuteNonQuery();
                connection.Close();
                return result > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении контакта: " + ex.Message);
                return false;
            }
        }

        public List<Contact> GetContacts()
        {
            List<Contact> contacts = new List<Contact>();
            connection.Open();
            string query = "SELECT Name, PhoneNumber, ContactId FROM Contacts";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                contacts.Add(new Contact { Name = reader["Name"].ToString(), PhoneNumber = reader["PhoneNumber"].ToString(), ContactId = reader["ContactId"].ToString() });
            }
            connection.Close();
            return contacts;
        }

        public int ValidateUser(string username, string password)
        {
            using (var connection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                connection.Open();
                string query = "SELECT UserId, Password FROM Users WHERE Username = @Username";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedPassword = reader["Password"].ToString();
                            string hashedPassword = HashPassword(password);
                            if (hashedPassword == storedPassword)
                            {
                                int userId = Convert.ToInt32(reader["UserId"]);
                                return userId;
                            }
                        }
                    }
                }
            }
            return -1;
        }
        public bool DeleteContact(int contactId)
        {
            try
            {
                connection.Open();
                string query = "DELETE FROM Contacts WHERE ContactId = @ContactId";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ContactId", contactId);
                    int result = command.ExecuteNonQuery();
                    return result > 0; 
                }
            }
            catch (Exception ex)
            {
           
                MessageBox.Show("Ошибка при удалении контакта: " + ex.Message);
                Console.WriteLine($"Ошибка при удалении контакта с ID {contactId}: {ex.Message}");
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public bool UpdateContact(int contactId, string newName, string newPhoneNumber)
        {
            try
            {
                connection.Open();
                string query = "UPDATE Contacts SET Name = @Name, PhoneNumber = @PhoneNumber WHERE ContactId = @ContactId";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", newName);
                    command.Parameters.AddWithValue("@PhoneNumber", newPhoneNumber);
                    command.Parameters.AddWithValue("@ContactId", contactId);
                    int result = command.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при обновлении контакта: " + ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }


    }

}