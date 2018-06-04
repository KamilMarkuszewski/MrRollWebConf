using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MrRollWebConf.Models
{
    public class EditTopicViewModel
    {
        #region Ctor
        
        public EditTopicViewModel() { }

        public EditTopicViewModel(int topicId, string topicName, int categoryId, string categoryName)
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
            Id = topicId;
            Name = topicName;
        }

        public EditTopicViewModel(int categoryId, string categoryName)
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
        }

        #endregion

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Please specify name")]
        public string Name { get; set; }

        public int Id { get; set; }




    }
}
