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
        private List<string> rezultati = new List<string>();
        private string[] words;
        private string resuletters = "";

        public ActionResult Index()
        {
            return View(new GenViewModel());
        }

        [HttpPost]
        public ActionResult Index(GenViewModel model)
        {
            fillTrie();
            fillResultsNew(model);
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
                    words = line.Split();
                    string word = words[0].ToLower();
                    trie.Put(word, word);
                }
            }
        }
    
        // NOVI ALGORITAM KOJI SE KORISTI
        // ** Od svake riječi uzimamo po 1 slovo
        private void fillResultsNew(GenViewModel model)
        {
            trie.Matcher.ResetMatcher();
            model.results = new List<string>();
            words = model.request.ToLower().Split(null);
            fillResultsRecursion(0, words.Length - 1, model);
        }

        private void fillResultsRecursion(int current, int last, GenViewModel model)
        {

            if (current == last)
            {
                foreach (char d in words[current])
                {
                    if (trie.Matcher.StepForward(d))
                    {
                        if (trie.Matcher.isMatchWord())
                        {
                            if (!model.results.Contains(trie.Matcher.GetCurrentMatch()))
                            {
                                resuletters += trie.Matcher.GetCurrentMatch() + "\t-\t-\t-\t";
                                findLettersinResult();
                                model.results.Add(resuletters);
                                resuletters = "";
                            }  
                        }
                        trie.Matcher.StepBack();
                    }
                }
            }
            else
            {
                foreach (char c in words[current])
                {
                    if (trie.Matcher.StepForward(c))
                    {
                        fillResultsRecursion(current + 1, last, model);

                        trie.Matcher.StepBack();
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }

        private void findLettersinResult()
        {
            int x = 0;
            bool isitUp = false;
            foreach (char c in trie.Matcher.GetCurrentMatch())
            {
                foreach (char d in words[x])
                {
                    if (c.Equals(d) && isitUp == false)
                    {
                        resuletters += Char.ToUpper(d);
                        isitUp = true;
                    }
                    else
                    {
                        resuletters += d;
                    }
                }
                x++;
                isitUp = false;
                resuletters += " ";
            }
        }

        // STARI POKUŠAJ ALGORITMA ČIJA IDEJA JE ODBAČENA
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
                        // 1 permutacije
                        case 1:
                            if (trie.Matcher.isMatchWord())
                            {
                                model.results.Add(trie.Matcher.GetCurrentMatch());
                            }
                            diSamStao[brojRijeci - 1]++;
                            for (int i = diSamStao[brojRijeci - 1]; i < words[brojRijeci - 1].Length; i++)
                            {
                                if (diSamStao[brojRijeci - 1] == words[brojRijeci - 1].Length)
                                {
                                    state = 2;
                                }
                                trie.Matcher.StepBack();
                                char c = Char.Parse(words[brojRijeci - 1].Substring(diSamStao[brojRijeci - 1], 1));
                                trie.Matcher.StepForward(c);
                                state = 2;
                            }
                            break;
                        case 2:
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