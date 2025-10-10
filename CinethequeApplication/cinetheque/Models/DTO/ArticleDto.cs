using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cinetheque.Models.DTO
{
    public class ArticleDto
    {
        private int id;
        private double price;
        private string name;
        private string description;
        private int totalQty;
        private int dispoQty;
        private Category category;

        ArticleDto(int id, double price, string name, string description, int totalQty, int dispoQty, Category category)
        {
            Id = id;
            Price = price;
            Name = name;
            Description = description;
            TotalQty = totalQty;
            DispoQty = dispoQty;
            Category = category;
        }

        public ArticleDto() { }

        public int Id { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TotalQty { get; set; }
        public int DispoQty { get; set; }
        public Category Category { get; set; }
    }
}