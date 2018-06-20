using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherAPIProject.Models
{
    public class WeatherModel
    {
        public string apiResponse { get; set; }
        public Dictionary<string,string>Cities  { get; set; }
    }
}