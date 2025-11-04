using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JOIEnergy.Domain
{
    [Table("Plans")]
    public class PlanDetailsDB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlanId { get; set; }

        [Required]
        [StringLength(100)]
        public string PlanName { get; set; }

        [Required]
        [StringLength(100)]
        public string EnergySupplier { get; set; }

        [Required]
        public decimal UnitRate { get; set; }

        // One Plan → Many SmartMeters
        public ICollection<SmartMeterIDDetailsDB> SmartMeters { get; set; } = new List<SmartMeterIDDetailsDB>();
    }
}