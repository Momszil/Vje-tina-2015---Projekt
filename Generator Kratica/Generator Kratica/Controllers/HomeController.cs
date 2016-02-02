using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Generator_Kratica.Models.Trie;
using Generator_Kratica.Models;

namespace Generator_Kratica.Controllers
{
    public class HomeController : Controller
    {
        private Trie<string> trie = new Trie<string>();

        public ActionResult Index()
        {
            
            trie.Put("Marko", "Marko");
            trie.Put("Ivan", "Ivan");
            // TODO NATRPAVANJE U TRIE METODU
            return View(new GenViewModel());
        }
        
        [HttpPost]
        public ActionResult Index(GenViewModel model)
        {
            model.results = new List<string>();
            model.results.Add("PERO");
            model.results.Add("vedran");
            //TODO NATRPAVANJE REZULTATA
            return PartialView("Generate", model);
        }

        [HttpPost]
        public ActionResult GoBack()
        {
            return Redirect("/");
        }

        /*
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        */
    }
}