using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using MrRollWebConf.Exceptions;
using MrRollWebConf.Models;
using Newtonsoft.Json;

namespace MrRollWebConf.Data
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private static string ApplicationJson
        {
            get { return "application/json"; }
        }


        public List<Category> GetCategories()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://jsswirus.nazwa.pl:8080/mrroll-1.0/");
                    var response = client.GetAsync($"rest/v1/categories").Result;
                    response.EnsureSuccessStatusCode();

                    var stringResult = response.Content.ReadAsStringAsync().Result;
                    var categories = JsonConvert.DeserializeObject<IEnumerable<Category>>(stringResult);

                    return categories.ToList();
                }
                catch (HttpRequestException)
                {
                    throw new MrRollException();
                }
            }
        }

        public int SaveCategory(EditCategoryViewModel editCategoryViewModel)
        {
            if (editCategoryViewModel.Id == 0)
            {
                return AddCategory(editCategoryViewModel);
            }
            else
            {
                return UpdateCategory(editCategoryViewModel);
            }
        }

        public void DeleteCategory(int id)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://jsswirus.nazwa.pl:8080/mrroll-1.0/");
                    var response = client.DeleteAsync($"rest/v1/categories/{id}").Result;
                    response.EnsureSuccessStatusCode();
                }
                catch (HttpRequestException)
                {
                    throw new MrRollException();
                }
            }
        }

        public int SaveTopic(EditTopicViewModel editTopicViewModel)
        {
            if (editTopicViewModel.Id == 0)
            {
                return AddTopic(editTopicViewModel);
            }
            else
            {
                return UpdateTopic(editTopicViewModel);
            }
        }

        private int UpdateTopic(EditTopicViewModel editTopicViewModel)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://jsswirus.nazwa.pl:8080/mrroll-1.0/");
                    var httpContent = ToJsonHttpContent(new { name = editTopicViewModel.Name });
                    var response = client.PutAsync($"rest/v1/categories/{editTopicViewModel.CategoryId}/topics/{editTopicViewModel.Id}", httpContent).Result;
                    response.EnsureSuccessStatusCode();
                    return editTopicViewModel.Id;
                }
                catch (HttpRequestException)
                {
                    throw new MrRollException();
                }
            }
        }

        private int AddTopic(EditTopicViewModel editTopicViewModel)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://jsswirus.nazwa.pl:8080/mrroll-1.0/");
                    var httpContent = ToJsonHttpContent(new { name = editTopicViewModel.Name });
                    var response = client.PostAsync($"rest/v1/categories/{editTopicViewModel.CategoryId}/topics/", httpContent).Result;
                    response.EnsureSuccessStatusCode();

                    var stringResult = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(stringResult);
                    return Int32.Parse(result["topicID"]);
                }
                catch (HttpRequestException)
                {
                    throw new MrRollException();
                }
            }
        }

        public void DeleteTopic(int id, int categoryId)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://jsswirus.nazwa.pl:8080/mrroll-1.0/");
                    var response = client.DeleteAsync($"rest/v1/categories/{categoryId}/topics/{id}").Result;
                    response.EnsureSuccessStatusCode();
                }
                catch (HttpRequestException)
                {
                    throw new MrRollException();
                }
            }
        }

        private int UpdateCategory(EditCategoryViewModel editCategoryViewModel)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://jsswirus.nazwa.pl:8080/mrroll-1.0/");
                    var httpContent = ToJsonHttpContent(new { name = editCategoryViewModel.Name });
                    var response = client.PutAsync($"rest/v1/categories/{editCategoryViewModel.Id}", httpContent).Result;
                    response.EnsureSuccessStatusCode();
                    return editCategoryViewModel.Id;
                }
                catch (HttpRequestException)
                {
                    throw new MrRollException();
                }
            }
        }

        private int AddCategory(EditCategoryViewModel editCategoryViewModel)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://jsswirus.nazwa.pl:8080/mrroll-1.0/");
                    var httpContent = ToJsonHttpContent(new { name = editCategoryViewModel.Name });
                    var response = client.PostAsync($"rest/v1/categories", httpContent).Result;
                    response.EnsureSuccessStatusCode();

                    var stringResult = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(stringResult);
                    return Int32.Parse(result["categoryID"]);
                }
                catch (HttpRequestException)
                {
                    throw new MrRollException();
                }
            }
        }

        private static StringContent ToJsonHttpContent(object value)
        {
            var categoryJson = JsonConvert.SerializeObject(value, Formatting.Indented);
            var httpContent = new StringContent(categoryJson, Encoding.UTF32, ApplicationJson);
            return httpContent;
        }


    }
}
