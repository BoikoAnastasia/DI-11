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

namespace QUISLY
{
    
    public partial class OpenQuiz : Window
    {
        Test currentTest;
        int currentIndex = 0;
        bool isAnsver = false;
        public OpenQuiz(Test currentTest)
        {
            InitializeComponent();
            this.currentTest = currentTest;
            testName.Text = currentTest.name;
            questionText.Text = $"{currentTest.questions[currentIndex].questionName}\n{currentTest.questions[currentIndex].question}";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!(currentIndex <= 0))
            {
                currentIndex--;
                questionText.Text = $"{currentTest.questions[currentIndex].questionName}\n{currentTest.questions[currentIndex].question}";
                isAnsver = false;
            }
            else if (!(currentIndex == 0)) {
                questionText.Text = $"{currentTest.questions[currentIndex - 1].questionName}\n{currentTest.questions[currentIndex - 1].question}";
                isAnsver = false;
                currentIndex = currentTest.questions.Count - 1;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!isAnsver)
            {
                questionText.Text = $"Ответ: {currentTest.questions[currentIndex].answer}";
                isAnsver = true;
            }
            else {
                questionText.Text = $"{currentTest.questions[currentIndex].questionName}\n{currentTest.questions[currentIndex].question}";
                isAnsver = false;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if(!(currentIndex >= currentTest.questions.Count - 1 ))
            {
                currentIndex++;
                questionText.Text = $"{currentTest.questions[currentIndex].questionName}\n{currentTest.questions[currentIndex].question}";
                isAnsver = false;
            } else {
                currentIndex = 0;
                questionText.Text = $"{currentTest.questions[0].questionName}\n{currentTest.questions[0].question}";
                isAnsver = false;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            SelectActionQuiz selectActionQuiz = new SelectActionQuiz();
            selectActionQuiz.Show();
            this.Close();
        }
    }

}
