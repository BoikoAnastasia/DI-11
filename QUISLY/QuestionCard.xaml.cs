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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QUISLY
{
    /// <summary>
    /// Логика взаимодействия для QuestionCard.xaml
    /// </summary>
    public partial class QuestionCard : Page
    {
        public QuestionCard(string question, string questionNames)
        {
            InitializeComponent();
            questionText.Text = question;
            questionName.Text = questionNames;
        }
    }
}
