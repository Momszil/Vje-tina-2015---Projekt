using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Generator_Kratica.Models
{
    public class Words
    {
        public int WordID { get; set; }
        public string Letters { get; set; }
        public bool isWord { get; set; }
        public bool isBasicWord { get; set; }
    }
}