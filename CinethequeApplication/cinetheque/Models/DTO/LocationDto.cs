using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cinetheque.Models.DTO
{
    public class LocationDto
    {
        public int Id { get; set; }
        public double TotalPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ArticleDto Article { get; set; }
        public UserDto User { get; set; }
    }
}