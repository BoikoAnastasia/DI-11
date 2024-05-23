using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
namespace QUISLY
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Test newTest = new Test("Test1");
            string jsonString = JsonConvert.SerializeObject( newTest );
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            //File.WriteAllText(docPath, jsonString);
            using (StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine("C:\\Users\\admin\\source\\repos\\QUISLY", $"{newTest.name}.json")))
            {
                    outputFile.WriteLine(jsonString);
            }

            Test tt = JsonConvert.DeserializeObject<Test>(jsonString);
            ArrayList newArray = tt.questions;
            Question[] qu = new Question[tt.questions.Count];
            int i = 0;
            foreach (object q in tt.questions)
            {
                i++;
                
            }
            i = 0;
        }
    }
}
