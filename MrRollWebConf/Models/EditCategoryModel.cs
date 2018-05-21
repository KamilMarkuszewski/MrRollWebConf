using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrRollWebConf.Models
{
    public class EditCategoryModel
    {
        public Category Category;

        public EditCategoryModel(Category category)
        {
            Category = category;
        }

        public EditCategoryModel()
        {
            Category = new Category();
        }
    }
}
