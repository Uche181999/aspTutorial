using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotation.Schema;
using System.Threading.Tasks;

namespace api.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = "";
        public string CompanyName { get; set; } = string.Empty;
        [Column(TypeName = "decimal(20,2)")]
        public decimal Purchase { get; set; }
        [Column(TypeName = "decimal(20,2)")]
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }
        
        //conection to comment table
        public list<Comment> Comments { get; set; }= new list<Comment>();

    }
}