using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cinetheque.Models
{
    public class UserInfo
    {
        [JsonProperty("id")]
         public int getId{get; set;}
        [JsonProperty("nom")]
        public string getUserName{ get; set; }
        [JsonProperty("prenom")]
        public string getUserFirstName{ get; set; }
        [JsonProperty("adresse")]
        public string getUserAdress{ get; set; }
        [JsonProperty("utilisateur_id")]
        public int getUserId{ get; set; }


        UserInfo() { }
        UserInfo(string name, string firstName, string adress, int user_id)
        {
            getUserName = name;
            getUserFirstName = firstName;
            getUserAdress = adress;
            getUserId = user_id;
        }
    }
}