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
    /// Логика взаимодействия для Vopros.xaml
    /// </summary>
    public partial class Vopros : Window
    {
        public Vopros()
        {
            InitializeComponent();
        }

        private TestContext _context = new TestContext();
        private Test _currentTest;
        private int _currentQuestionIndex;

        private void LoadTest(int testId)
        {
            _currentTest = _context.Test.Include(t => t.Questions).ThenInclude(q => q.Answers).FirstOrDefault(t => t.Id == testId);
            _currentQuestionIndex = 0;
            DisplayCurrentQuestion();
        }

        private void DisplayCurrentQuestion()
        {
            if (_currentTest != null && _currentQuestionIndex < _currentTest.Questions.Count)
            {
                var currentQuestion = _currentTest.Questions.ElementAt(_currentQuestionIndex);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _currentQuestionIndex++;
            DisplayCurrentQuestion();
        }

    }
}
