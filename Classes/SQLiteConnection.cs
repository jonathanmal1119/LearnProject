using Learn.Classes.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Classes
{
    internal class SQLiteConnection : IDatabase
    {
        private string filePath;
        public SQLiteConnection(string filePath)
        {
            this.filePath = filePath;
        }
        public DataTable GetQuery(string Query)
        {
            DataTable dt = new DataTable();
            using (System.Data.SQLite.SQLiteConnection sqlite = new System.Data.SQLite.SQLiteConnection("Data Source=" + filePath))
            using (System.Data.SQLite.SQLiteCommand sqlitecommand = new System.Data.SQLite.SQLiteCommand(Query, sqlite))
            {
                sqlite.Open();
                using (System.Data.SQLite.SQLiteDataReader sQLiteDataReader = sqlitecommand.ExecuteReader())
                {
                    try
                    {
                        dt.Load(sQLiteDataReader);
                    }
                    catch (ConstraintException e)
                    {
                        //Console.WriteLine(e);
                    }
                }
                sqlite.Close();
                sqlite.Dispose();
            }
            return dt;
        }

        public void RunQuery(string Query)
        {
            using (System.Data.SQLite.SQLiteConnection sqlite = new System.Data.SQLite.SQLiteConnection("Data Source=" + filePath))
            using (System.Data.SQLite.SQLiteCommand sqlitecommand = sqlite.CreateCommand())
            {
                sqlite.Open();
                sqlitecommand.CommandText = Query;
                sqlitecommand.CommandType = CommandType.Text;
                sqlitecommand.ExecuteNonQuery();
                sqlite.Close();
                sqlite.Dispose();
            }
        }
    }
}
