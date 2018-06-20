using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WeatherAPIProject.Models;

namespace WeatherAPIProject.Controllers
{
    public class WeatherAppController : Controller
    {
        // GET: WeatherApp
        public ActionResult Index()
        {
            WeatherModel weatherModel = FillCity();
            return View(weatherModel);
        }
        [HttpPost]
        public ActionResult Index(string cities)
        {
            WeatherModel weatherModel = FillCity();
            try
            {
                if (cities != null)
                {
                    string apiKey = "f1adaad561307ef65a1266096fd908b2";
                    HttpWebRequest apiRequest = WebRequest.Create("http://api.openweathermap.org/data/2.5/weather?id=" + cities + "&appid=" + apiKey + "&units=metric") as HttpWebRequest;
                    string apiResponse = "";
                    using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
                    {
                        StreamReader reader = new StreamReader(response.GetResponseStream());
                        apiResponse = reader.ReadToEnd();
                    }
                    ResponseWeather rootObject = JsonConvert.DeserializeObject<ResponseWeather>(apiResponse);
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<table><tr><th>Weather Description</th></tr>");
                    sb.Append("<tr><td>City:</td><td>" + rootObject.name + "</td></tr>");
                    sb.Append("<tr><td>Country:</td><td>" + rootObject.sys.country + "</td></tr>");
                    sb.Append("<tr><td>Wind:</td><td>" + rootObject.wind.speed + " Km/h</td></tr>");
                    sb.Append("<tr><td>Current Temperature:</td><td>" + rootObject.main.temp + " °C</td></tr>");
                    sb.Append("<tr><td>Humidity:</td><td>" + rootObject.main.humidity + "</td></tr>");
                    sb.Append("<tr><td>Weather:</td><td>" + rootObject.weather[0].description + "</td></tr>");
                    sb.Append("</table>");
                    weatherModel.apiResponse = sb.ToString();

                }
                else
                {
                    if (Request.Form["submit"] != null)
                    {
                        weatherModel.apiResponse = "► Select City";
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return View(weatherModel);
        }
        public WeatherModel FillCity()
        {
            WeatherModel weatherModel = new WeatherModel();
            weatherModel.Cities = new Dictionary<string, string>();
            weatherModel.Cities.Add("Melbourne", "7839805");
            weatherModel.Cities.Add("Auckland", "2193734");
            weatherModel.Cities.Add("New Delhi", "1261481");
            weatherModel.Cities.Add("Abu Dhabi", "292968");
            weatherModel.Cities.Add("Lahore", "1172451");
            return weatherModel;
        }
    }
}