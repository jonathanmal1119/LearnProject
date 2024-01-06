using Learn.Classes.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Learn.Classes
{
    internal static class ConnectionManager
    {
        public static IDatabase EstablishConnection()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Learn.db");
            if (!File.Exists(filePath))
            {
                System.Data.SQLite.SQLiteConnection.CreateFile(filePath);
                File.SetAttributes(filePath,File.GetAttributes(filePath)|FileAttributes.Hidden);
                IDatabase db = new SQLiteConnection(filePath);
                db.RunQuery("CREATE TABLE`TermCards` (`id` integer not null primary key autoincrement,`term` TEXT not null,`definition` TEXT not null,`favorite` BOOLEAN not null)");
            }
            return new SQLiteConnection(filePath);
        }
    }
}
