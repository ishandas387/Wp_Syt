using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spot_your_train_2.Models
{

    public class RootObject
    {
        public string position { get; set; }
        public string error { get; set; }
        public string train_number { get; set; }
        public int response_code { get; set; }
        public List<Route> route { get; set; }
        public int total { get; set; }
    }
    public class Route
    {
        public string actdep { get; set; }
        public string station { get; set; }
        public string actarr { get; set; }
        public string status { get; set; }
        public string schdep { get; set; }
        public int no { get; set; }
        public string scharr { get; set; }
    }

   
}
