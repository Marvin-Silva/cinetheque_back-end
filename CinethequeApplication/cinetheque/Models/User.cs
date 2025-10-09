using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cinetheque.Models
{
    public class User
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("login")]
        public string getLogin{ get; set; }
        [JsonProperty("mdp")]
        public string getPwd { get; set; }
        [JsonProperty("role")]
        public string getRole { get; set; }

        public User() { }
        public User(int id, string login, string password, string role)
        {
            Id = id;
            getLogin = login;
            getPwd = password;
            getRole = role;
        }
    }
}