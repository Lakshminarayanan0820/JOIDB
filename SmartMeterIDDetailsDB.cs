using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JOIEnergy.Domain
{
    [Table("SmartMeters")]
    public class SmartMeterIDDetailsDB
    {
        [Key]
        [StringLength(50)]
        public string SmartMeterId { get; set; }

        [Required]
        public int PlanId { get; set; }

        [ForeignKey(nameof(PlanId))]
        public PlanDetailsDB PlanDetails { get; set; }

        // One SmartMeter → Many Readings
        public ICollection<Reading> Readings { get; set; } = new List<Reading>();
    }
}