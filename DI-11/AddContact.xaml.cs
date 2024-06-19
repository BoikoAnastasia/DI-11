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

namespace DI_11
{
    /// <summary>
    /// Логика взаимодействия для AddContact.xaml
    /// </summary>
    public partial class AddContact : Window
    {
        private DatabaseManager dbManager;

        public AddContact()
        {
            InitializeComponent();
            dbManager = new DatabaseManager(); 
        }

        private void AddContact_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text;
            string phoneNumber = PhoneNumberTextBox.Text;
            int userId = DatabaseManager.UserId;

            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(phoneNumber))
            {
                if (dbManager.AddContact(userId, name, phoneNumber))
                {
                    MessageBox.Show("Контакт успешно добавлен.");
                    Contacts secondWindow = new Contacts();
                    secondWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ошибка при добавлении контакта.");
                }
            }
            else
            {
                MessageBox.Show("Имя и номер телефона не должны быть пустыми.");
            }
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Contacts secondWindow = new Contacts();
            secondWindow.Show();
            this.Close();
        }
    }
}
