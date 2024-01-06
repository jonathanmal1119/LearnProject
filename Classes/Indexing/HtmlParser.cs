using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Classes.Indexing
{
    internal class HtmlParser
    {
        private HtmlDocument _document;

        /// <summary>
        /// Loads a new html file.
        /// </summary>
        /// <param name="file">The file path to load.</param>
        public void LoadFile(string file)
        {
            _document = new HtmlDocument();
            _document.Load(file);
           
        }

        /// <summary>
        /// Loads a new string of html symbols.
        /// </summary>
        /// <param name="html">The string of html symbols to load.</param>
        public void LoadHtml(string html)
        {
            _document = new HtmlDocument();
            _document.LoadHtml(html);
        }

        /// <summary>
        /// Loads html from a link.
        /// </summary>
        /// <param name="url">The url to load.</param>
        public void LoadUrl(string url)
        {
            HtmlWeb web = new HtmlWeb();
            _document = web.Load(url);
        }

        /// <summary>
        /// Finds a list of html symbols based off a element.
        /// </summary>
        /// <param name="symbol">The symbol name to find.</param>
        /// <returns>A <see cref="IEnumerable{HtmlNode}"/> collection of elements. </returns>
        public IEnumerable<HtmlNode> FindElements(string symbol)
        {
            if (_document is null) return new HtmlNode[0];
            return _document.DocumentNode.SelectNodes(symbol).AsEnumerable();
        }
    }
}
