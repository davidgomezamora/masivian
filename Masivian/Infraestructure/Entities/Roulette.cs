using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Infraestructure.Entities
{
    [Table("Roulette")]
    public partial class Roulette
    {
        [Key]
        public Guid Id { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
        public Guid StateId { get; set; }

        [ForeignKey(nameof(Id))]
        [InverseProperty(nameof(State.Roulette))]
        public virtual State IdNavigation { get; set; }
    }
}
