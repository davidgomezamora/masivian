using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Infraestructure.Entities
{
    [Table("Bet")]
    public partial class Bet
    {
        [Key]
        public Guid Id { get; set; }
        [Column(TypeName = "money")]
        public decimal Money { get; set; }
        public Guid UserId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
        public Guid StateId { get; set; }

        [ForeignKey(nameof(Id))]
        [InverseProperty(nameof(State.Bet))]
        public virtual State IdNavigation { get; set; }
    }
}
