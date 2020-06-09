using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meds_Server.Model
{
    public partial class Meds
    {
        [Key()]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; } 

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int Pieces { get; set; }

        [StringLength(30)]
        public string Type { get; set; }

        [Column("Best_Before")]
        public DateTime BestBefore { get; set; }

        public byte[] Picture { get; set; }

        [Column("Base_Substance")]
        [StringLength(50)]
        public string BaseSubstance { get; set; }

        [Column("Base_Substance_Quantity")]
        [StringLength(50)]
        public string BaseSubstanceQuantity { get; set; }

        [StringLength(200)]
        public string Description { get; set; }
    }
}
