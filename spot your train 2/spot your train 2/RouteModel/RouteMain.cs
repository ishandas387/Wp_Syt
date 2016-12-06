using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spot_your_train_2.Model.RouteModel
{
    public class Route
{
    public string fullname { get; set; }
    public string state { get; set; }
    public int halt { get; set; }
    public int route { get; set; }
    public int day { get; set; }
    public string schdep { get; set; }
    public double lat { get; set; }
    public int distance { get; set; }
    public string code { get; set; }
    public double lng { get; set; }
    public int no { get; set; }
    public string scharr { get; set; }
}

public class Class
{
    public string available { get; set; }
    public string __invalid_name__class_code { get; set; }
}

public class Day
{
    public string __invalid_name__day_code { get; set; }
    public string runs { get; set; }
}

public class Train
{
    public string name { get; set; }
    public List<Class> classes { get; set; }
    public string number { get; set; }
    public List<Day> days { get; set; }
}

public class RouteMain
{
    public List<Route> route { get; set; }
    public Train train { get; set; }
    public int response_code { get; set; }
}
}
