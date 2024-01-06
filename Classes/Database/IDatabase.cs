using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Classes.Database
{
    internal interface IDatabase
    {
        void RunQuery(string Query);
        DataTable GetQuery(string Query);
    }
}
