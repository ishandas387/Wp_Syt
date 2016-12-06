using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spot_your_train_2.Models
{
    public class TrainStartDate
    {
    }

    public class ToStation
    {
        public string code { get; set; }
        public string name { get; set; }
    }

    public class BoardingPoint
    {
        public string code { get; set; }
        public string name { get; set; }
    }

    public class ReservationUpto
    {
        public string code { get; set; }
        public string name { get; set; }
    }

    public class FromStation
    {
        public string code { get; set; }
        public string name { get; set; }
    }

    public class PnrMain
    {
        public TrainStartDate train_start_date { get; set; }
        public List<object> passengers { get; set; }
        public bool error { get; set; }
        public string chart_prepared { get; set; }
        public ToStation to_station { get; set; }
        public string train_name { get; set; }
        public BoardingPoint boarding_point { get; set; }
        public int response_code { get; set; }
        public string doj { get; set; }
        public string pnr { get; set; }
        public string train_num { get; set; }
        public string @class { get; set; }
        public ReservationUpto reservation_upto { get; set; }
        public int total_passengers { get; set; }
        public FromStation from_station { get; set; }
    }
}
