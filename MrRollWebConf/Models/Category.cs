using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrRollWebConf.Models
{
    public class Category
    {
        public int Id;
        public string Name;
        public IEnumerable<Topic> Topics;
    }
}
