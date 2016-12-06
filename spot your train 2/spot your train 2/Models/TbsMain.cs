using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spot_your_train_2.Models
{
   public class To
{
    public string name { get; set; }
    public string code { get; set; }
}

public class Day
{
    public string __invalid_name__day_code { get; set; }
    public string runs { get; set; }
}

public class From
{
    public string name { get; set; }
    public string code { get; set; }
}

public class Train
{
    public string name { get; set; }
    public string dest_arrival_time { get; set; }
    public string src_departure_time { get; set; }
    public To to { get; set; }
    public List<Day> days { get; set; }
    public string number { get; set; }
    public int no { get; set; }
    public From from { get; set; }
}

public class TbsMain
{
    public int total { get; set; }
    public List<Train> train { get; set; }
    public int response_code { get; set; }
}
    
}
