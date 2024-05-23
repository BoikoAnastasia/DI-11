using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUISLY
{
    public class Question
    {
        public int id;
        public string question;
        public string answer;
        public string questionName;
        public Question(int numberQ,string questionName, string question, string answer) {
            id =numberQ;
            this.questionName = questionName;
            this.question = question;
            this.answer = answer;
        }
    }
}
