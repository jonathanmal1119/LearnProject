using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Classes.Indexing
{
    internal interface IIndexer
    {
        /// <summary>
        /// Loads a new file into the <see cref="IIndexer"/>.
        /// </summary>
        /// <param name="file">The file path to load.</param>
        /// <returns>True if successful.</returns>
        bool LoadFile(string file);

        /// <summary>
        /// Parses a file and adds the terms into the database.
        /// </summary>
        void IndexTerms();

        /// <summary>
        /// Starts indexing terms asynchronously.
        /// </summary>
        /// <returns>A <see cref="Task"/> that can be awaited.</returns>
        Task BeginIndexTermsAsync();
    }
}
