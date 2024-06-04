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
    /// Логика взаимодействия для HomePage.xaml
    /// </summary>
    public partial class HomePage : Window
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Vopros vopros = new Vopros();
            vopros.Show();
            Close();
        }
        private void Sozdat(object sender, RoutedEventArgs e)
        {
            Test test = new Test();
            test.Show();
            Close();
        }
        private void Othenki(object sender, RoutedEventArgs e)
        {
            Othenki othenki = new Othenki();
            othenki.Show();
            Close();
        }
    }
}
