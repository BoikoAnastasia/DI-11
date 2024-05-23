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
using static System.Net.Mime.MediaTypeNames;
using Newtonsoft.Json;
using System.Collections;
using System.IO;
using System.Xml.Linq;

namespace QUISLY
{
    /// <summary>
    /// Логика взаимодействия для SelectActionQuiz.xaml
    /// </summary>
    public partial class SelectActionQuiz : Window
    {
        Test test;
        public SelectActionQuiz()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".json";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                using (StreamReader r = new StreamReader(filename))
                {
                    string json = r.ReadToEnd();
                    Test test = JsonConvert.DeserializeObject<Test>(json);
                    this.test = test;
                }
                OpenQuiz oq = new OpenQuiz(test);
                oq.Show();
                this.Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CreateQuiz cq = new CreateQuiz();
            cq.Show();
            this.Close();
        }
    }
}
