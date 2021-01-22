using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Infraestructure.Entities
{
    public partial class Bet
    {
        [Key]
        [Column("ID")]
        public Guid Id { get; set; }
        public int Money { get; set; }
        public Guid UserId { get; set; }
        public int? Prize { get; set; }
        public Guid RouletteId { get; set; }
        public Guid StateId { get; set; }

        [ForeignKey(nameof(RouletteId))]
        [InverseProperty("Bets")]
        public virtual Roulette Roulette { get; set; }
        [ForeignKey(nameof(StateId))]
        [InverseProperty(nameof(Status.Bets))]
        public virtual Status State { get; set; }
    }
}
