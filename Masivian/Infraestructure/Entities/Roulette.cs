﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Infraestructure.Entities
{
    public partial class Roulette
    {
        public Roulette()
        {
            Bets = new HashSet<Bet>();
        }

        [Key]
        [Column("ID")]
        public Guid Id { get; set; }
        public Guid StateId { get; set; }

        [ForeignKey(nameof(StateId))]
        [InverseProperty(nameof(Status.Roulettes))]
        public virtual Status State { get; set; }
        [InverseProperty(nameof(Bet.Roulette))]
        public virtual ICollection<Bet> Bets { get; set; }
    }
}
