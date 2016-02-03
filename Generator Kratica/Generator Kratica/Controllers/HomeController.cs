using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Generator_Kratica.Models.Trie;
using Generator_Kratica.Models;
using System.IO;

namespace Generator_Kratica.Controllers
{
    public class HomeController : Controller
    {
        private Trie<string> trie = new Trie<string>();

        public ActionResult Index()
        {
            return View(new GenViewModel());
        }

        [HttpPost]
        public ActionResult Index(GenViewModel model)
        {
            fillTrie();
            model.results = new List<string>();
            model.results.Add("PERO".ToLower());
            model.results.Add("vedran");
            trie.Matcher.ResetMatcher();
            trie.Matcher.StepForward('v');
            trie.Matcher.StepForward('e');
            trie.Matcher.StepForward('d');
            trie.Matcher.StepForward('r');
            trie.Matcher.StepForward('a');
            trie.Matcher.StepForward('n');
            model.results.Add(trie.Matcher.isMatchWord().ToString());
            model.results.Add(trie.Matcher.GetCurrentMatch());
            //TODO NATRPAVANJE REZULTATA
            return PartialView("Generate", model);
        }

        [HttpPost]
        public ActionResult GoBack()
        {
            return Redirect("/");
        }

        private void fillTrie()
        {
            using (StreamReader sr = new StreamReader(Server.MapPath(@"/App_Data/HR_Txt-601.txt")))
            {
                while (sr.Peek() >= 0)
                {
                    string line = sr.ReadLine();
                    string[] words = line.Split();
                    string word = words[0].ToLower();
                    trie.Put(word, word);
                }
            }
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