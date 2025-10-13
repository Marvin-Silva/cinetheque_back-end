using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cinetheque.Models
{
    public class Category
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("nom")]
        public string CategoryName { get; set; }

        public Category() { }
        public Category(int id, string category)
        {
            this.Id = id;
            this.CategoryName = category;
        }
    }
}