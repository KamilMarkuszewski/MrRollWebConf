using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MrRollWebConf.Data;
using MrRollWebConf.Exceptions;
using MrRollWebConf.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MrRollWebConf.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoriesController(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var categories = _categoriesRepository.GetCategories().OrderBy(c => c.Id);
            return View("Index", categories);
        }

        public IActionResult AddCategory()
        {
            return View("EditCategory", new EditCategoryViewModel());
        }

        public IActionResult EditCategory(int categoryId)
        {
            var categories = _categoriesRepository.GetCategories();
            var cateogry = categories.FirstOrDefault(c => c.Id == categoryId);
            if (cateogry == null)
            {
                ViewData["error"] = "Obiekt został usunięty przez innego użytkownika.";
                return RedirectToAction("Index");
            }

            return View("EditCategory", new EditCategoryViewModel(cateogry));
        }

        [HttpPost]
        public IActionResult DeleteCategory(int categoryId)
        {
            try
            {
                _categoriesRepository.DeleteCategory(categoryId);
                ViewData["success"] = "Kategoria została usunięta.";
            }
            catch (MrRollException)
            {
                ViewData["error"] = "Operacja zakończona niepowodzeniem.";
            }
            return RedirectToAction("Index");
        }


        public IActionResult AddTopic(int categoryId)
        {
            var categories = _categoriesRepository.GetCategories();
            var category = categories.FirstOrDefault(c => c.Id == categoryId);
            if (category == null)
            {
                ViewData["error"] = "Obiekt został usunięty przez innego użytkownika.";
                return RedirectToAction("Index");
            }

            return View("EditTopic", new EditTopicViewModel(categoryId, category.Name));
        }

        public IActionResult EditTopic(int topicId, int categoryId)
        {
            var categories = _categoriesRepository.GetCategories();
            var category = categories.FirstOrDefault(c => c.Id == categoryId);
            if (category == null)
            {
                ViewData["error"] = "Obiekt został usunięty przez innego użytkownika.";
                return RedirectToAction("Index");
            }
            var topic = category.Topics.FirstOrDefault(t => t.Id == topicId);
            if (topic == null)
            {
                ViewData["error"] = "Obiekt został usunięty przez innego użytkownika.";
                return RedirectToAction("Index");
            }

            var editTopicViewModel = new EditTopicViewModel(topicId, topic.Name, categoryId, category.Name);
            return View(editTopicViewModel);
        }

        [HttpPost]
        public IActionResult DeleteTopic(int topicId, int categoryId)
        {
            try
            {
                _categoriesRepository.DeleteTopic(topicId, categoryId);
                ViewData["success"] = "Temat został usunięty.";
            }
            catch (MrRollException)
            {
                ViewData["error"] = "Operacja zakończona niepowodzeniem.";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SaveCategory(EditCategoryViewModel editCategoryViewModel)
        {
            try
            {
                _categoriesRepository.SaveCategory(editCategoryViewModel);
                ViewData["success"] = "Kategoria została zapisana.";
            }
            catch (MrRollException)
            {
                ViewData["error"] = "Operacja zakończona niepowodzeniem.";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SaveTopic(EditTopicViewModel editTopicViewModel)
        {
            try
            {
                _categoriesRepository.SaveTopic(editTopicViewModel);
                ViewData["success"] = "Temat został zapisany.";
            }
            catch (MrRollException)
            {
                ViewData["error"] = "Operacja zakończona niepowodzeniem.";
            }
            return RedirectToAction("Index");
        }
    }
}
