using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MrRollWebConf.Exceptions;

namespace MrRollWebConf.Models
{
    public static class FakeRepo
    {
        private static readonly List<Category> Categories = new List<Category>()
        {
            new Category
            {
                Id = 1, Name = "Cars", Topics = new List<Topic>()
                {
                    new Topic {Id = 1, Name = "Parking", CategoryId = 1},
                    new Topic {Id = 2, Name = "	Delegacje", CategoryId = 1},
                }
            },
            new Category
            {
                Id = 2, Name = "Food", Topics = new List<Topic>()
                {
                    new Topic {Id = 3, Name = "Pizza", CategoryId = 2},
                    new Topic {Id = 4, Name = "Kanapki", CategoryId = 2},
                }
            },
        };

        public static List<CategoryRow> AllGetCategoryRows()
        {
            return Categories.Select(c => new CategoryRow() { Id = c.Id, Name = c.Name }).ToList();
        }

        public static List<Category> AllGetCategories()
        {
            return Categories;
        }

        public static void DeleteCategory(Category category)
        {
            if (Categories.Any(c => c.Id == category.Id))
            {
                var cat = Categories.First(c => c.Id == category.Id);
                if (cat.Topics.Any())
                {
                    throw new MrRollForeignKeyException();
                }
                Categories.Remove(category);
            }
            else
            {
                throw new MrRollConcurrencyException();
            }
        }


        public static void DeleteTopic(Topic topic)
        {
            if (Categories.Any(c => c.Topics.Any(t => t.Id == topic.Id)))
            {
                Categories.ForEach(c => c.Topics.Remove(topic));
            }
            else
            {
                throw new MrRollConcurrencyException();
            }
        }

        public static void AddTopic(Topic topic)
        {
            List<Topic> topics = new List<Topic>();
            Categories.ForEach(c => topics.AddRange(c.Topics));
            Topic maxTopic = topics.Aggregate((i, j) => i.Id > j.Id ? i : j);
            topic.Id = maxTopic.Id + 1;
            var cat = Categories.FirstOrDefault(c => c.Id == topic.CategoryId);
            if (cat == null)
            {
                throw new MrRollConcurrencyException();
            }

            cat.Topics.Add(topic);
        }


    }
}
