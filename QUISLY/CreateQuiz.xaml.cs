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
using Newtonsoft.Json;
using System.IO;

namespace QUISLY
{
    /// <summary>
    /// Логика взаимодействия для CreateQuiz.xaml
    /// </summary>
    
    public partial class CreateQuiz : Window
    {
        int i = 0;
        List<Question> questions = new List<Question>();
        public CreateQuiz()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!(answersVariant.Text.Equals("") || answerText.Text.Equals("") || questionName.Text.Equals("")))
            {
                i++;
                Question newq = new Question(0, questionName.Text, answersVariant.Text, answerText.Text);
                questions.Add(newq);
                Frame addfr = new Frame();
                addfr.Content = new QuestionCard(newq.question, newq.questionName);
                DataGridy.Children.Add(addfr);
                questionName.Text = "";
                answersVariant.Text = "";
                answerText.Text = "";
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Test newTest = new Test(nameBox.Text, questions);
            string jsonString = JsonConvert.SerializeObject(newTest);
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            //File.WriteAllText(docPath, jsonString);
            using (StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine("C:\\Users\\admin\\source\\repos\\QUISLY\\Tests", $"{newTest.name}.json")))
            {
                outputFile.WriteLine(jsonString);
            }
            SelectActionQuiz selectAction = new SelectActionQuiz();
            selectAction.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SelectActionQuiz selectAction = new SelectActionQuiz();
            selectAction.Show();
            this.Close();
        }
    }
}
