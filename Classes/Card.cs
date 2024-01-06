using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Classes
{
    public class Card
    {
        public string term { get; set; }
        public string definition { get; set; }
        public string group { get; set; }
        bool favorite { get; set; }

        public Card()
        {
            term = "";
            definition = "";
            group = "";
            favorite = false;
        }

        public Card(string term, string def, string group, bool fav)
        {
            this.term = term;
            definition = def;
            this.group = group;
            favorite = fav;
        }

        public Card(string term, string def, string group)
        {
            this.term = term;
            definition = def;
            this.group = group;
        }

        public override string ToString()
        {
            return "Term: " + term + " | Definition: " + definition;
        }
    }
}
