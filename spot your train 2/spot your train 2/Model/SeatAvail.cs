using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spot_your_train_2.Model.pnr
{
 
        public class Availability
        {
            public string status { get; set; }
            public string date { get; set; }
        }

        public class Quota
        {
            public string quota_code { get; set; }
            public string quota_name { get; set; }
        }

        public class To
        {
            public string code { get; set; }
            public double lng { get; set; }
            public string name { get; set; }
            public double lat { get; set; }
        }

        public class Class
        {
            public string class_name { get; set; }
            public string class_code { get; set; }
        }

        public class From
        {
            public string code { get; set; }
            public double lng { get; set; }
            public string name { get; set; }
            public double lat { get; set; }
        }

        public class SeatAvail
        {
            public List<Availability> availability { get; set; }
            public string train_name { get; set; }
            public Quota quota { get; set; }
            public bool error { get; set; }
            public string train_number { get; set; }
            public To to { get; set; }
            public Class @class { get; set; }
            public From from { get; set; }
            public int response_code { get; set; }
        }
    
}
