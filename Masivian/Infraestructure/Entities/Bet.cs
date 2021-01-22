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
        public int Number { get; set; }
        public bool IsColor { get; set; }
        public double Money { get; set; }
        public Guid UserId { get; set; }
        public double? Prize { get; set; }
        public int? RouletteNumber { get; set; }
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
