using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spot_your_train_2.Models
{
   
        public class Station
        {
            public string fullname { get; set; }
            public string code { get; set; }
        }

        public class Sname
        {
            public List<Station> station { get; set; }
            public int total { get; set; }
            public int response_code { get; set; }
        }
    
}
