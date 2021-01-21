using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Infraestructure.Entities
{
    [Table("State")]
    public partial class State
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(10)]
        public string Name { get; set; }

        [InverseProperty("IdNavigation")]
        public virtual Bet Bet { get; set; }
        [InverseProperty("IdNavigation")]
        public virtual Roulette Roulette { get; set; }
    }
}
