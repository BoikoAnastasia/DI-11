using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUISLY
{
    internal class Question
    {
        public int id;
        public string question;
        public string answer;
        public Question(int numberQ, string question, string answer) {
            id =numberQ;
            this.question = question;
           this.answer = answer;
        }
    }
}
