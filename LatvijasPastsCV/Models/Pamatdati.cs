using System.ComponentModel.DataAnnotations;

namespace LatvijasPastsCV.Models
{
    public class Pamatdati
    {
        public int ID { get; set; }

        [MaxLength(50)]
        public string? Vards { get; set; }

        [MaxLength(50)]
        public string? Uzvards { get; set; }

        [MaxLength(50)]
        public string? Talrunis { get; set; }

        [MaxLength(50)]
        public string? EPasts { get; set; }
    }
}