using System.ComponentModel.DataAnnotations;

namespace LatvijasPastsCV.Models
{
    public class Izglitiba
    {
        public int ID { get; set; }

        [MaxLength(255)]
        public string? Nosaukums { get; set; }

        [MaxLength(255)]
        public string? Fakultate { get; set; }

        [MaxLength(255)]
        public string? StudijuVirziens { get; set; }

        [MaxLength(255)]
        public string? IzglitibasLimenis { get; set; }

        [MaxLength(255)]
        public string? Statuss { get; set; }

        [MaxLength(255)]
        public string? Iestade { get; set; }
        public DateTime MacibasLaiks { get; set; }

        public int CVID { get; set; }
    }
}