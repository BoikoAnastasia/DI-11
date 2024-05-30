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
    /// Логика взаимодействия для Vhod.xaml
    /// </summary>
    public partial class Vhod : Window
    {
       
        public Vhod()
        {
            InitializeComponent();
        }

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(name.Text.Length > 0)
            {
                if (password.Password != null)
                {


                    if (name.Text == "admin")
                    {
                        if (password.Password == "111")
                        {
                            HomePage homePage = new HomePage();
                            homePage.Show();
                            Close();
                        }


                    }
                    else
                    {
                        MessageBox.Show("Нет такого пользователя");
                    }
                }


            }
            else
            {
                MessageBox.Show("Введите данные");
            }

        }
    }
}
