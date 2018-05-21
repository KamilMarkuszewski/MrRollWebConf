using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MrRollWebConf.Exceptions;
using MrRollWebConf.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MrRollWebConf.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            var categories = FakeRepo.AllGetCategories();
            return View("Index", categories);
        }


        public IActionResult AddCategory()
        {
            return View("EditCategory", new EditCategoryModel());
        }

        public IActionResult EditCategory(Category cat)
        {
            return View(new EditCategoryModel(cat));
        }

        public IActionResult DeleteCategory(Category category)
        {
            try
            {
                FakeRepo.DeleteCategory(category);
                ViewData["success"] = "Kategoria została usunięta.";
            }
            catch (MrRollForeignKeyException)
            {
                ViewData["error"] = "Nie można usunąć.";
            }
            catch (MrRollConcurrencyException)
            {
                ViewData["error"] = "Obiekt został edytowany przez innego użytkownika.";
            }
            return Index();
        }


        public IActionResult AddTopic()
        {
            IEnumerable<CategoryRow> allCategories = FakeRepo.AllGetCategoryRows();
            return View("EditTopic", new EditTopicModel(allCategories));
        }

        public IActionResult EditTopic()
        {
            Topic topic = null;
            IEnumerable<CategoryRow> allCategories = FakeRepo.AllGetCategoryRows();
            return View(new EditTopicModel(topic, allCategories));
        }

        public IActionResult DeleteTopic(Topic topic)
        {
            try
            {
                FakeRepo.DeleteTopic(topic);
                ViewData["success"] = "Temat został usunięty.";
            }
            catch (MrRollForeignKeyException)
            {
                ViewData["error"] = "Nie można usunąć.";
            }
            catch (MrRollConcurrencyException)
            {
                ViewData["error"] = "Obiekt został edytowany przez innego użytkownika.";
            }
            return Index();
        }



    }
}
