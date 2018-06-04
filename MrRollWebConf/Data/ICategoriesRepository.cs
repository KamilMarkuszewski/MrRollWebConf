using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MrRollWebConf.Models;

namespace MrRollWebConf.Data
{
    public interface ICategoriesRepository
    {
        List<Category> GetCategories();

        int SaveCategory(EditCategoryViewModel editCategoryViewModel);
        void DeleteCategory(int id);

        int SaveTopic(EditTopicViewModel editTopicViewModel);
        void DeleteTopic(int id, int categoryId);
    }
}
