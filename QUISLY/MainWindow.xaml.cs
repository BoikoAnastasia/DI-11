using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        string[] userNames;
        string[] userPasswords;
        bool userFind = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=3218EC05\SQLEXPRESS; Initial Catalog=QUIZLY; Integrated Security=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("select login, password from Users", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                userNames = reader[0].ToString().Split();
                userPasswords = reader[1].ToString().Split();
            }
            reader.Close();

            for (int i = 0; i < userNames.Length; i++) {
                if (!userNames[i].Equals(loginBox.Text)) {
                    userFind = false;
                } else userFind = true;
            }
            if (userFind)
            {
                SelectActionQuiz saq = new SelectActionQuiz();
                saq.Show();
                this.Close();
            }
            else {
                SqlCommand cmd2 = new SqlCommand($"INSERT INTO Users VALUES ({userNames.Length + 1},'{loginBox.Text}', '{passwordBox.Text}')", conn);
                cmd2.ExecuteNonQuery();
                //reader2.Close();
                SelectActionQuiz saq = new SelectActionQuiz();
                saq.Show();
                this.Close();
            }



        
            //Test newTest = new Test("Test1");
            //string jsonString = JsonConvert.SerializeObject(newTest);
            //string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            ////File.WriteAllText(docPath, jsonString);
            //using (StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine("C:\\Users\\admin\\source\\repos\\QUISLY\\Tests", $"{newTest.name}.json")))
            //{
            //    outputFile.WriteLine(jsonString);
            //}

            //Test tt = JsonConvert.DeserializeObject<Test>(jsonString);
            //ArrayList newArray = tt.questions;
            //Question[] qu = new Question[tt.questions.Count];
        }
    }
}
