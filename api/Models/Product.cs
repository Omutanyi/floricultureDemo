using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public int? GenderId { get; set; }
        public Gender? Gender { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}