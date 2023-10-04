using System.ComponentModel.DataAnnotations;

namespace LatvijasPastsCV.Models
{
    public class Adrese
    {
        public int ID { get; set; }

        [MaxLength(255)]
        public string? Valsts { get; set; }
        public string? Indekss { get; set; }

        [MaxLength(255)]
        public string? Pilseta { get; set; }

        [MaxLength(255)]
        public string? Iela { get; set; }
        public int Numurs { get; set; }
    }
}