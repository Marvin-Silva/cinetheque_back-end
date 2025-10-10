using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cinetheque.Models.DTO
{
    public class ArticleDto
    {
        public int getId { get; set; }
        public double getPrice { get; set; }
        public string getName { get; set; }
        public string getDescription { get; set; }
        public int getTotalQty { get; set; }
        public int getDispoQty { get; set; }
        public Category getCatId { get; set; }
    }
}