using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace JOIEnergy.Domain
{
    [Table("Readings")]
    public class Reading
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReadingId { get; set; }

        [Required]
        [StringLength(50)]
        public string SmartMeterId { get; set; }

        [ForeignKey(nameof(SmartMeterId))]
        public SmartMeterIDDetailsDB SmartMeter { get; set; }

        [Required]
        public double Value { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}