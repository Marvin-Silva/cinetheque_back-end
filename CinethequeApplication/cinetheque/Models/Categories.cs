using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cinetheque.Models
{
    public class Categories
    {
        [JsonProperty("id")]
        public int getId { get; set; }

        [JsonProperty("categorie")]
        public string getNom { get; set; }
    }
}