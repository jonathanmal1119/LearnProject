using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Classes.Indexing
{
    internal class MyIndexer
    {
        private HtmlDocument _document;
        public void LoadFile(string file)
        {
            _document = new HtmlDocument();
            _document.Load(file);
            
        }

        public IEnumerable<HtmlNode> getItems(string symbol)
        {
            return _document.DocumentNode.Descendants("ul");
        }

        private List<string> added = new List<string>();
        public void selectBody()
        {
            var htmlBody = _document.DocumentNode.SelectNodes("//li");
            var nodes = htmlBody.Elements("ul");

            foreach (var node in nodes)
            {
                //string strWithTabs = node.InnerText.Trim().Replace("\n", string.Empty);

                //string line = strWithTabs.Replace("\t", " ");
                //while (line.IndexOf("  ") >= 0)
                //{
                //    line = line.Replace("  ", " ");
                //}
                //Console.WriteLine(line);
                Console.WriteLine(node.OuterHtml);
                if (node.NodeType == HtmlNodeType.Element)
                {
                    
                }
            }
        }
    }
}
