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
using System.Windows.Shapes;

namespace DI_11
{
    /// <summary>
    /// Логика взаимодействия для Stocks.xaml
    /// </summary>
    public partial class Stocks : Window
    {
        bool isSelected = false;
        List<DoElement> ListElements = new List<DoElement>();
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-H3UE754; Initial Catalog=ToDo; Integrated Security=True");
        public Stocks()
        {
            InitializeComponent();
            conn.Open();
            SqlCommand cmd = new SqlCommand("select nameTask, description, typeTask, id from Tasks", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                DoElement doEl = new DoElement(reader[0].ToString(), reader[1].ToString(), this, Int32.Parse(reader[2].ToString()), Int32.Parse(reader[3].ToString()));
                ListElements.Add(doEl);
            }
            reader.Close();
            updateList();
        }

        //Метод распределяет задачи по их типам
        public void updateList()
        {
            DoList.Items.Clear();
            ProcessList.Items.Clear();
            ReadyList.Items.Clear();
            for (int i = 0; i < ListElements.Count(); i++)
            {

                if (ListElements[i].typeTask == 0)
                {
                    DoList.Items.Add(setToFrame(ListElements[i]));
                }
                else if (ListElements[i].typeTask == 1)
                {
                    ProcessList.Items.Add(setToFrame(ListElements[i]));
                }
                else
                {
                    ReadyList.Items.Add((setToFrame(ListElements[i])));
                }
            }
        }

        //Метод который вставляет page в Frame чтобы вставить page в ListBox
        private Frame setToFrame(DoElement element)
        {
            Frame frame = new Frame();
            frame.Content = element;
            frame.Width = 333;
            return frame;
        }

        private void DoList_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void DoList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DoList_Selected(object sender, RoutedEventArgs e)
        {

        }

        //Метод обработки клика на кнопку добавить задачу
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddTask adWindow = new AddTask(this);
            adWindow.Show();
        }

        //Метод добавления задачи принимающий имя и описание задачи, так же метод добавляет задачу в бд
        public void addTask(string nameTask, string taskInfo)
        {
            SqlCommand cmd = new SqlCommand($"insert into Tasks (id, nameTask, description, typeTask ) values ({ListElements.Count}, '{nameTask}', '{taskInfo}', 0)", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Close();
            DoElement doEl = new DoElement(nameTask, taskInfo, this, 0, 0);
            ListElements.Add(doEl);
            updateList();
        }

        //Метод вызывается при перемещении задачи в разные столбцы тем самым обновляя её статус
        public void updateTaskType(int type, int id)
        {
            SqlCommand cmd = new SqlCommand($"UPDATE Tasks set typeTask = {type} where id = {id}", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Close();

        }
        //Метод удаления задачи
        public void deleteTask(int id)
        {
            SqlCommand cmd = new SqlCommand($"DELETE FROM Tasks where id = {id}", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Close();
            ListElements.Remove(ListElements[id]);
            updateList();
        }
    }
}
