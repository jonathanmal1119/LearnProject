using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Classes.Indexing
{
    internal class HtmlIndexer : IIndexer
    {
        private HtmlDocument _document;

        public Task BeginIndexTermsAsync()
        {
            return Task.Run(() => IndexTerms());
        }

        public void IndexTerms()
        {
           
            if (_document == null) return;
            
            HtmlParser h = new HtmlParser();
            Console.WriteLine(h.FindElements("<ul>"));
            //h.FindElements();


            //Hparse.FindElements("<ul>");
            //foreach (HtmlNode n in Hparse.FindElements(""))
            //{
            //    Console.WriteLine(n.ToString());
            //}
        }

        public bool LoadFile(string file)
        {
            if(!File.Exists(file)) return false;
            _document = new HtmlDocument();
            _document.Load(file);

            if(_document is null) return false;
            return true;
        }
    }
}
