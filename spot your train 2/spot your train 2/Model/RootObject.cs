using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spot_your_train_2.Model
{
   
        public class RootObject
        {
            public int respnse_code { get; set; }
            public int total { get; set; }
            public List<string> train { get; set; }
        }
    
}
