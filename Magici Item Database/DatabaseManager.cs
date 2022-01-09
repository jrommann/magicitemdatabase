using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magici_Item_Database
{
    class DatabaseManager
    {
        static DatabaseManager _instance = null;
        static SQLiteConnection _db;

        DatabaseManager(string filepath)
        {
            _db = new SQLiteConnection(filepath);
            _db.CreateTable<MagicItem>();
        }

        static public DatabaseManager Open(string filepath)
        {
            _instance = new DatabaseManager(filepath);
            return _instance;
        }

        static public void Close(string filepath)
        {
            if (_db == null)
                throw new Exception("Database NOT opened");

            _db.Close();
            _db = null;
        }        

        static public List<MagicItem> GetAll()
        {
            if (_db == null)
                throw new Exception("Database NOT opened");
            
            return _db.Query<MagicItem>("SELECT * FROM MagicItems");
        }

        static public bool Add(MagicItem item)
        {
            if (_db == null)
                throw new Exception("Database NOT opened");

            try { _db.Insert(item); }
            catch { return false; }

            return true;
        }
    }
}
