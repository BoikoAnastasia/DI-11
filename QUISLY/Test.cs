using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUISLY
{
   public class Test
    {
        public string name = "";
        public List<Question> questions = new List<Question>();
    
        public Test(string n, List<Question> questions) { 
            name = n;
            this.questions = questions;
        }
    }
}
