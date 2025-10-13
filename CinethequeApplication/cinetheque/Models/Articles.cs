using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cinetheque.Models
{
    public class Articles
    {
        [JsonProperty("id")]
        public int getId { get; set; }
        [JsonProperty("prix")]
        public double getPrice { get; set; }
        [JsonProperty("nom")]
        public string getName { get; set; }
        [JsonProperty("categorie_id")]
        public int getCatId { get; set; }
        [JsonProperty("description")]
        public string getDescription { get; set; }
        [JsonProperty("qte_totale")]
        public int getTotalQty { get; set; }
        [JsonProperty("qte_dispo")]
        public int getDispoQty { get; set; }
    }
}