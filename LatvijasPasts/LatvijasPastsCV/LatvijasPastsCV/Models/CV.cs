namespace LatvijasPastsCV.Models
{
    public class CV
    {
        public int ID { get; set; }

        public int PamatdatiID { get; set; }
        public Pamatdati? Pamatdati { get; set; }

        public List<Izglitiba> Izglitiba { get; set; } = new List<Izglitiba>();

        public List<DarbaPieredze> DarbaPieredzes { get; set; } = new List<DarbaPieredze>();

        public List<Prasmes> Prasmes { get; set; } = new List<Prasmes>();

        public int AdreseID { get; set; }
        public Adrese? Adrese { get; set; }
    }
}