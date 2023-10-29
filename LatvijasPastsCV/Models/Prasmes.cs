using System.ComponentModel.DataAnnotations;

namespace LatvijasPastsCV.Models
{
    public class Prasmes
    {
        public int ID { get; set; }

        [MaxLength(255)]
        public string? Apraksts { get; set; }

        [MaxLength(255)]
        public string? Veids { get; set; }

        public int CVID { get; set; }
    }
}