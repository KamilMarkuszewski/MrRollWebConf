using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrRollWebConf.Models
{
    public class EditTopicModel
    {
        public Topic Topic;

        public IEnumerable<CategoryRow> AllCategories;

        public EditTopicModel(IEnumerable<CategoryRow> allCategories)
        {
            Topic = new Topic();
            AllCategories = allCategories;
        }

        public EditTopicModel(Topic topic, IEnumerable<CategoryRow> allCategories)
        {
            Topic = topic;
            AllCategories = allCategories;
        }
    }
}
