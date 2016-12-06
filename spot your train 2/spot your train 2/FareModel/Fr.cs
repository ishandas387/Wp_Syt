using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spot_your_train_2.FareModel
{
    
        public class Fare
        {
            public string code { get; set; }
            public string fare { get; set; }
            public string name { get; set; }
        }

        public class Quota
        {
            public string code { get; set; }
            public string name { get; set; }
        }

        public class From
        {
            public string code { get; set; }
            public string name { get; set; }
        }

        public class Train
        {
            public string number { get; set; }
            public string name { get; set; }
        }

        public class To
        {
            public string code { get; set; }
            public string name { get; set; }
        }

        public class Fr
        {
            public List<Fare> fare { get; set; }
            public Quota quota { get; set; }
            public From from { get; set; }
            public Train train { get; set; }
            public To to { get; set; }
            public int response_code { get; set; }
        }
    
}
