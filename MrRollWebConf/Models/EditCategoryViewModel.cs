using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MrRollWebConf.Models
{
    public class EditCategoryViewModel
    {
        #region Ctor

        public EditCategoryViewModel() { }

        public EditCategoryViewModel(Category category)
        {
            Id = category.Id;
            Name = category.Name;
        }

        #endregion

        public int Id { get; set; }

        [Required(ErrorMessage = "Please specify name")]
        public string Name { get; set; }
    }
}
