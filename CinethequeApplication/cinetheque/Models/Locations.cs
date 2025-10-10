using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cinetheque.Models
{
    public class Locations
    {
        [JsonProperty("id")]
        public int getId { get; set; }

        [JsonProperty("prix_total")]
        public double getTotalPrice { get; set; }

        [JsonProperty("qte_articles")]
        public int getQuantity { get; set; }

        [JsonProperty("date_debut")]
        public DateTime getStartDate { get; set; }

        [JsonProperty("date_fin")]
        public DateTime getEndDate { get; set; }

        [JsonProperty("article_id")]
        public int getArticleId{ get; set; }

        [JsonProperty("utilisateur_id")]
        public int getUserId { get; set; }
    }
}