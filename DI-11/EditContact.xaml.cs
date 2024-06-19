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
    /// Логика взаимодействия для EditContact.xaml
    /// </summary>
    public partial class EditContact : Window
    {

        private DatabaseManager dbManager;
        private Contact contact;
        public EditContact(Contact contact)
        {
            InitializeComponent();
            dbManager = new DatabaseManager();
            this.contact = contact;
        }

        private void EditContact_Click(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text == string.Empty || PhoneNumberTextBox.Text == string.Empty)
            {
                MessageBox.Show("Имя и номер телефона не должны быть пустыми.");
                return;
            }

            int contactId = Convert.ToInt16(contact.ContactId);
            bool result = dbManager.UpdateContact(contactId, NameTextBox.Text, PhoneNumberTextBox.Text);
            if (result)
            {
                MessageBox.Show("Контакт успешно обновлен.");
                Contacts secondWindow = new Contacts();
                secondWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Не удалось обновить контакт.");
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
