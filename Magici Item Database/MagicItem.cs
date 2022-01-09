using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magici_Item_Database
{
    [Table("MagicItems")]
    public class MagicItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [Indexed]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public int PageNumber { get; set; }
    }
}
