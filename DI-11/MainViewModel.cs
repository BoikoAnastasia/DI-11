//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.ComponentModel;
//using System.Linq;
//using System.Runtime.Remoting.Contexts;

//namespace DI_11
//{
//    public class MainViewModel : INotifyPropertyChanged
//    {
//        private ObservableCollection<TestResult> _testResults;

//        public event PropertyChangedEventHandler PropertyChanged;

//        public ObservableCollection<TestResult> TestResults
//        {
//            get { return _testResults; }
//            set
//            {
//                _testResults = value;
//                OnPropertyChanged("TestResults");
//            }
//        }

//        public MainViewModel()
//        {
//            using (var dbContext = new TestDbContext())
//            {
//                TestResults = new ObservableCollection<TestResult>(dbContext.TestResults.ToList());
//            }
//        }

//        protected void OnPropertyChanged(string propertyName)
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        }
//    }

//    public class TestDbContext : DbContext
//    {
//        public DbSet<TestResult> TestResults { get; set; }
//    }

//    public class TestResult
//    {
//        public int Id { get; set; }
//        public string StudentName { get; set; }
//        public string TestName { get; set; }
//        public int Score { get; set; }
//    }
//}
