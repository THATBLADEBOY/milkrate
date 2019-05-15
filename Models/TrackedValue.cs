using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace milkrate.Models
{
    public class TrackedValue
    {
        [Key]
        public int Id { get; set; }

        public int Value { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime TrackedDate { get; set; }

        public int PieceId { get; set; }
    }
}
