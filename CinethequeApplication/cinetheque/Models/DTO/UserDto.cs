using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cinetheque.Models
{
    public class UserDto
    {
        [JsonProperty("login")]
        public string getLogin { get; set; }
        [JsonProperty("mdp")]
        public string getPwd { get; set; }
        [JsonProperty("role")]
        public string getRole { get; set; }
        [JsonProperty("id")]
        public int getId { get; set; }
        [JsonProperty("nom")]
        public string getUserName { get; set; }
        [JsonProperty("prenom")]
        public string getUserFirstName { get; set; }
        [JsonProperty("adresse")]
        public string getUserAdress { get; set; }
        [JsonProperty("utilisateur_id")]
        public int getUserId { get; set; }
    }
}