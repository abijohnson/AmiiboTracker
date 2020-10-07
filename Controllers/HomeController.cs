using AmiiboTracker.DAL;
using AmiiboTracker.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using unirest_net.http;
using System.Xml;
using System.Data;
using System.Net.Http;

namespace AmiiboTracker.Controllers
{
    public class HomeController : Controller
    {
        private Amiibo db = new Amiibo();

        private AmiiboContext context = new AmiiboContext();

        [Authorize]
        public ActionResult Index()
        {
            //Checks to make sure we have data in the database 
            var check = context.Amiiboes.Where(s => s.Name.Contains("Mario")).ToList();

            if (check.Count() == 0)
            {
                getAmiiboData();
            }


            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public void getAmiiboData()
        {

            string url = "https://n3evin-amiiboapi-v1.p.rapidapi.com/amiibo/";
            string h1 = System.Web.Configuration.WebConfigurationManager.AppSettings["key1"];
            string h2 = System.Web.Configuration.WebConfigurationManager.AppSettings["key2"];
            string request = SendRequest(url, h1, h2);

            //string temp ;

            int i = 0; 

            JObject parsedData = JObject.Parse(request);

            foreach (var info in parsedData["amiibo"])
            {
                db.AmiiboSeries = parsedData["amiibo"][i]["amiiboSeries"].Value<string>() ;
                db.Character = parsedData["amiibo"][i]["character"].Value<string>(); 
                db.GameSeries = parsedData["amiibo"][i]["gameSeries"].Value<string>();
                db.Head = parsedData["amiibo"][i]["head"].Value<string>();
                db.Image = parsedData["amiibo"][i]["image"].Value<string>();
                db.Name = parsedData["amiibo"][i]["name"].Value<string>();

                if(parsedData["amiibo"][i]["release"]["au"] != null )
                {
                    db.ReleaseAU = parsedData["amiibo"][i]["release"]["au"].Value<string>(); 
                }

                if (parsedData["amiibo"][i]["release"]["au"] == null)
                {
                    db.ReleaseAU = "This was not released in AU."; 
                }

                if (parsedData["amiibo"][i]["release"]["eu"] != null)
                {
                    db.ReleaseEU = parsedData["amiibo"][i]["release"]["eu"].Value<string>();
                }

                if (parsedData["amiibo"][i]["release"]["eu"] == null)
                {
                    db.ReleaseEU = "This was not released in EU.";
                }

                if (parsedData["amiibo"][i]["release"]["jp"] != null)
                {
                    db.ReleaseJP = parsedData["amiibo"][i]["release"]["jp"].Value<string>();
                }

                if (parsedData["amiibo"][i]["release"]["jp"] == null)
                {
                    db.ReleaseJP = "This was not released in JP.";
                }

                if (parsedData["amiibo"][i]["release"]["na"] != null)
                {
                    db.ReleaseNA = parsedData["amiibo"][i]["release"]["na"].Value<string>();
                }

                if (parsedData["amiibo"][i]["release"]["na"] == null)
                {
                    db.ReleaseNA = "This was not released in NA." ;
                }



                db.Tail = parsedData["amiibo"][i]["tail"].Value<string>();
                db.Type = parsedData["amiibo"][i]["type"].Value<string>();

                i++;

                context.Amiiboes.Add(db);
                context.SaveChanges();

            }

            //Console.WriteLine(parsedData);

            //for( int i = 0; i < items.Count(); i++ )
            //{
            //    temp = parsedData["avatar_url"].Value<string>(); ;
            //}

        }

        private string SendRequest(string uri, string h11, string h22)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Headers.Add("X-RapidAPI-Host", h11);
            request.Headers.Add("X-RapidAPI-Key", h22);
            request.Accept = "application/json";

            string jsonString = null;

            using (WebResponse response = request.GetResponse())
            {
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                jsonString = reader.ReadToEnd();
                reader.Close();
                stream.Close();
            }
            return jsonString;

        }



    }



}
