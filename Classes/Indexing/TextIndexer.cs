using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Classes.Indexing
{
    internal class TextIndexer : IIndexer
    {
        public Task BeginIndexTermsAsync()
        {
            return Task.Run(() => IndexTerms());
        }

        public void IndexTerms()
        {
            throw new NotImplementedException();
        }

        public bool LoadFile(string file)
        {
            throw new NotImplementedException();
        }
    }
}
