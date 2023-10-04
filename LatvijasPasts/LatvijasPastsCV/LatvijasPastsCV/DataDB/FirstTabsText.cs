using LatvijasPastsCV.DBData;
using LatvijasPastsCV.Models;

namespace LatvijasPastsCV.Services
{
    public class FirstTabsText
    {
        private readonly CVDbContext _context;

        public FirstTabsText(CVDbContext context)
        {
            _context = context;
        }

        public void SeedData()
        {
            if (!_context.Adreses.Any())
            {
                _context.Adreses.Add(new Adrese
                {
                    Valsts = "Latvija",
                    Indekss = "LV-1001",
                    Pilseta = "Rīga",
                    Iela = "Brīvības iela",
                    Numurs = 151
                });
                _context.SaveChanges();
            }

            if (!_context.Pamatdati.Any())
            {
                _context.Pamatdati.Add(new Pamatdati
                {
                    Vards = "Jānis",
                    Uzvards = "Bērziņš",
                    Talrunis = "+371 12345678",
                    EPasts = "janis@epasts.lv"
                });
                _context.SaveChanges();
            }

            if (!_context.CV.Any())
            {
                _context.CV.Add(new CV
                {
                    Pamatdati = _context.Pamatdati.FirstOrDefault(),
                    AdreseID = _context.Adreses.FirstOrDefault().ID
                });
                _context.SaveChanges();
            }

            if (!_context.DarbaPieredze.Any())
            {
                _context.DarbaPieredze.Add(new DarbaPieredze
                {
                    Vieta = "Uzņēmuma nosaukums",
                    Nosaukums = "Projekta nosaukums",
                    IenemamaisAmats = "Programmētājs",
                    SlodzesApmers = 40,
                    DarbaStazs = TimeSpan.FromDays(365 * 2),
                    Stazs = DateTime.Now.AddYears(-2),
                    Amats = "Izstrādātājs",
                    CVID = _context.CV.FirstOrDefault().ID
                });
                _context.SaveChanges();
            }

            if (!_context.Izglitiba.Any())
            {
                _context.Izglitiba.Add(new Izglitiba
                {
                    Nosaukums = "Universitātes nosaukums",
                    Fakultate = "Informācijas tehnoloģijas",
                    StudijuVirziens = "Datorzinātnes",
                    IzglitibasLimenis = "Bakalaura",
                    Statuss = "Absolvējis",
                    Iestade = "Latvijas Universitāte",
                    MacibasLaiks = new DateTime(2017, 9, 1),
                    CVID = _context.CV.FirstOrDefault().ID
                });
                _context.SaveChanges();
            }

            if (!_context.Prasmes.Any())
            {
                _context.Prasmes.Add(new Prasmes
                {
                    Veids = "Programmēšana",
                    CVID = _context.CV.FirstOrDefault().ID
                });
                _context.SaveChanges();
            }

            _context.SaveChanges();
        }
    }
}