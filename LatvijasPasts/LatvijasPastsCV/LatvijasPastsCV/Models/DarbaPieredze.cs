using System.ComponentModel.DataAnnotations;

namespace LatvijasPastsCV.Models
{
    public class DarbaPieredze
    {
        public int ID { get; set; }

        [MaxLength(255)]
        public string? Vieta { get; set; }

        [MaxLength(255)]
        public string? Nosaukums { get; set; }

        [MaxLength(255)]
        public string? IenemamaisAmats { get; set; }
        public int SlodzesApmers { get; set; }
        public TimeSpan DarbaStazs { get; set; }
        public DateTime Stazs { get; set; }

        [MaxLength(255)]
        public string? Amats { get; set; }

        public int CVID { get; set; }
    }
}