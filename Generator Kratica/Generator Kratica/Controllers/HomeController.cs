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
            fillResults(model);
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

        // ** Ako je u igri više od 3 riječi,
        //    Onda ćemo maksimalno uzeti po 2 slova od svake riječi
        // ** Ako je u igri manje od 4 riječi,
        //    Onda ćemo moći uzeti po maksimalno 3 slova od svake riječi
        // ** Riječi koje imaju 3 ili manje slova bi mogli i preskočiti
        // ** Svaka ostala riječ ćemo uzet da mora bar 1 slovo uzet
        private void fillResults(GenViewModel model)
        {
            trie.Matcher.ResetMatcher();
            model.results = new List<string>();
            string[] words = model.request.ToLower().Split(null);
            if (true) // words.Length > 3
            {
                int state = 0;
                int brojRijeci = words.Length;
                int[] diSamStao = new int[brojRijeci];
                for (int i = 0; i < brojRijeci; i++)
                {
                    diSamStao[i] = 0;
                }
                bool prolaz = true;
                while (prolaz)
                {
                    switch (state)
                    {
                        // 0 nismo došli do zadnje riječi, od svake riječi uzimamo po prvo slovo
                        case 0:

                            for (int i = 0; i < brojRijeci; i++)
                            {
                                if (i == brojRijeci - 1)
                                {
                                    state = 1;
                                }
                                else
                                {
                                    state = 0;
                                }
                                char c = Char.Parse(words[i].Substring(0, 1));
                                trie.Matcher.StepForward(c);
                            }
                            break;
                        // 1 počinjemo sa zadnjom riječi
                        case 1:
                            if (trie.Matcher.isMatchWord())
                            {
                                model.results.Add(trie.Matcher.GetCurrentMatch());
                            }
                            prolaz = false;
                            break;
                    }
                }
                
            }
            /*
            trie.Matcher.ResetMatcher();

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
            */
        }
        // KOJU KARIRANU KOŠULJU DANAS OBUĆI (kakao)
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