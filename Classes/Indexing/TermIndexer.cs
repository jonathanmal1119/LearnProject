using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Classes.Indexing
{
    internal class TermIndexer
    {
        private readonly Dictionary<string, IIndexer> indexList;

        public TermIndexer()
        {
            indexList = new Dictionary<string, IIndexer>
            {
                { ".txt", new TextIndexer() },
                { ".html", new HtmlIndexer() }
            };
        }

        public IIndexer CreateIndexerByExtension(string extension)
        {
            return indexList.FirstOrDefault(x => x.Key == extension).Value;
        }

        public IIndexer CreateIndexerByFile(string file)
        {
            FileInfo fi = new FileInfo(file);
            return indexList.FirstOrDefault(x => x.Key == fi.Extension).Value;
        }
    }
}
