using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace milkrate.Models
{
    public class Piece
    {
        [Key]
        public int ID { get; set; }

        public string Title { get; set; }

        public string PieceType { get; set; }

        public int RetailPrice { get; set; }

        public int AveragePrice { get; set; }

        public decimal Premium { get; set; }

        public int LastSale { get; set; }

        public string PageLink { get; set; }

        public string ImageLink { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}
