using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Infraestructure.Entities
{
    [Table("Status")]
    public partial class Status
    {
        public Status()
        {
            Bets = new HashSet<Bet>();
            Roulettes = new HashSet<Roulette>();
        }

        [Key]
        [Column("ID")]
        public Guid Id { get; set; }
        [StringLength(10)]
        public string Name { get; set; }

        [InverseProperty(nameof(Bet.State))]
        public virtual ICollection<Bet> Bets { get; set; }
        [InverseProperty(nameof(Roulette.State))]
        public virtual ICollection<Roulette> Roulettes { get; set; }
    }
}
