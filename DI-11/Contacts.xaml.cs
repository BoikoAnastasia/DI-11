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
    /// Логика взаимодействия для Contacts.xaml
    /// </summary>
    public partial class Contacts : Window
    {
        private DatabaseManager dbManager;

        public Contacts()
        {
            InitializeComponent();
            dbManager = new DatabaseManager();
            LoadContacts();
        }

        private void LoadContacts()
        {
            MessagList.Children.Clear();
            List<Contact> contacts = dbManager.GetContacts();
            foreach (var contact in contacts)
            {
                StackPanel contactPanel = new StackPanel { Orientation = Orientation.Horizontal };

                TextBlock nameBlock = new TextBlock
                {
                    Text = contact.Name,
                    FontSize = 16,
                    Margin = new Thickness(5)
                };

                TextBlock phoneBlock = new TextBlock
                {
                    Text = contact.PhoneNumber,
                    FontSize = 16,
                    Margin = new Thickness(5)
                };

               
                Button deleteButton = new Button
                {
                    Content = "Удалить",
                    Margin = new Thickness(5),
                    Tag = contact  
                };
                deleteButton.Click += DeleteContact_Click;  

               
                Button editButton = new Button
                {
                    Content = "Редактировать",
                    Margin = new Thickness(5),
                    Tag = contact 
                };
                editButton.Click += EditContact_Click; 

                contactPanel.Children.Add(nameBlock);
                contactPanel.Children.Add(phoneBlock);
                contactPanel.Children.Add(deleteButton);
                contactPanel.Children.Add(editButton);
                MessagList.Children.Add(contactPanel);
            }
        }

        private void DeleteContact_Click(object sender, RoutedEventArgs e)
        {
            Button deleteButton = sender as Button;
            Contact contact = deleteButton.Tag as Contact;

            if (dbManager.DeleteContact(Convert.ToInt16(contact.ContactId)))
            {
                MessageBox.Show("Контакт удалён");
                LoadContacts(); 
            }
            else
            {
                MessageBox.Show("Ошибка при удалении контакта");
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string searchQuery = Search.Text;
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                LoadContacts();
                return;
            }

            var foundContacts = dbManager.SearchContactsByName(searchQuery);
            DisplayContacts(foundContacts);
        }

        private void DisplayContacts(List<Contact> contacts)
        {
            MessagList.Children.Clear();

            foreach (var contact in contacts)
            {
                StackPanel contactPanel = new StackPanel { Orientation = Orientation.Horizontal };
                TextBlock nameBlock = new TextBlock
                {
                    Text = contact.Name,
                    FontSize = 16,
                    Margin = new Thickness(5)
                };
                TextBlock phoneBlock = new TextBlock
                {
                    Text = contact.PhoneNumber,
                    FontSize = 16,
                    Margin = new Thickness(5)
                };

                contactPanel.Children.Add(nameBlock);
                contactPanel.Children.Add(phoneBlock);
                MessagList.Children.Add(contactPanel); // Добавьте новые элементы в UI
            }
        }



        private void EditContact_Click(object sender, RoutedEventArgs e)
        {
            Button editButton = sender as Button;
            Contact contact = editButton.Tag as Contact;

            EditContact editWindow = new EditContact(contact);
            editWindow.Show();
            this.Close(); 
        }

        private void Exit_From_User_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Add_Contact_Click(object sender, RoutedEventArgs e)
        {
            AddContact secondWindow = new AddContact();
            secondWindow.Show();
            this.Close();
        }
    }
}
