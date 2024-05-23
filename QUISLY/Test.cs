using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUISLY
{
    internal class Test
    {
        public string name = "";
        public ArrayList questions = new ArrayList();
    
        public Test(string n) { 
            name = n;
            questions.Add(new Question(0, "sdads", "s"));
            questions.Add(new Question(1, "sdads", "s"));
        }
    }
}
