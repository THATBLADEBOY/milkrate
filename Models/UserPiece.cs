using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace milkrate.Models
{
    public class UserPiece
    {
        [Key]
        public int Id { get; set; }

        public int ConditionId { get; set; }

        public string UserId { get; set; }

        public int PieceId { get; set; }

        public decimal Value { get; set; }

        public Piece Piece { get; set; }

        public Condition Condition { get; set; }

        public ApplicationUser User { get; set; }

        public List<TrackedValue> TrackedValues { get; set; }
    }
}
