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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DI_11
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DatabaseManager dbManager;

        public MainWindow()
        {
            InitializeComponent();
            dbManager = new DatabaseManager();
        }

        private void Input_Changed(object sender, RoutedEventArgs e)
        {
            LogInButton.IsEnabled = !string.IsNullOrWhiteSpace(LoginBox.Text) && !string.IsNullOrWhiteSpace(PasswordBox.Password);
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            string username = LoginBox.Text;
            string password = PasswordBox.Password;

            int userId = dbManager.ValidateUser(username, password);
            if (userId != -1) 
            {
                OpenContactsWindow();
            }
            else
            {
                bool userCreated = dbManager.CreateUser(username, password);
                if (userCreated)
                {
                    int test = dbManager.ValidateUser(username, password);
                    ExeptionBlock.Text = "Аккаунт создан, вход выполнен.";
                    OpenContactsWindow();
                }
                else
                {
                    ExeptionBlock.Text = "Ошибка создания аккаунта.";
                }
            }
        }


        private void OpenContactsWindow()
        {
            Contacts secondWindow = new Contacts();
            secondWindow.Show();
            this.Close();
        }
    }
}
