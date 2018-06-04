using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrRollWebConf.Models
{
    public class CategoryRow
    {
        public int Id;
        public string Name;

        public CategoryRow(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
