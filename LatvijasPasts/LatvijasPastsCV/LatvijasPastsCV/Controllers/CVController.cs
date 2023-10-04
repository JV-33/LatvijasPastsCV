using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LatvijasPastsCV.DBData;
using LatvijasPastsCV.Models;

namespace Pasts.Controllers
{
    public class CVController : Controller
    {
        private readonly CVDbContext _context;

        public CVController(CVDbContext context)
        {
            _context = context;
        }

        public IActionResult CVSaraksts()
        {
            var cvs = _context.CV.Include(c => c.Pamatdati).ToList();
            return View(cvs);
        }

        public IActionResult CVDetails()
        {
            return View();
        }

        [Route("CV/CVDetails/{id}")]
        public IActionResult CVDetails(int id)
        {
            var cv = _context.CV
                            .Include(c => c.Pamatdati)
                            .Include(c => c.Adrese)
                            .Include(c => c.Izglitiba)
                            .Include(c => c.DarbaPieredzes)
                            .Include(c => c.Prasmes)
                            .FirstOrDefault(c => c.ID == id);

            if (cv == null)
            {
                return NotFound();
            }

            return View(cv);
        }

        public IActionResult CVCreate()
        {
            var cvModel = new CV();

            cvModel.Izglitiba = new List<Izglitiba>() { new Izglitiba() };
            cvModel.DarbaPieredzes = new List<DarbaPieredze>() { new DarbaPieredze() };
            cvModel.Prasmes = new List<Prasmes>() { new Prasmes() };

            return View(cvModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CVCreate(CV cv)
        {
            if (ModelState.IsValid)
            {
                _context.CV.Add(cv);
                _context.SaveChanges();
                return RedirectToAction(nameof(CVSaraksts));
            }
            return View(cv);
        }

        public IActionResult CVEdit(int id)
        {
            var cvToEdit = _context.CV
                                   .Include(c => c.Pamatdati)
                                   .Include(c => c.Adrese)
                                   .Include(c => c.Izglitiba)
                                   .Include(c => c.DarbaPieredzes)
                                   .Include(c => c.Prasmes)
                                   .FirstOrDefault(c => c.ID == id);

            if (cvToEdit == null)
            {
                return NotFound();
            }

            return View(cvToEdit);
        }

        [HttpPost, ActionName("CVEdit")]
        [ValidateAntiForgeryToken]
        public IActionResult CVEdit(int id, CV cv)
        {
            if (id != cv.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(cv);
                _context.SaveChanges();
                return RedirectToAction(nameof(CVSaraksts));
            }
            return View(cv);
        }

        public IActionResult CVDelete(int id)
        {
            var cv = _context.CV.FirstOrDefault(c => c.ID == id);
            if (cv == null)
            {
                return NotFound();
            }
            return View(cv);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var cvToDelete = _context.CV.Include(c => c.Izglitiba)
                                        .Include(c => c.DarbaPieredzes)
                                        .Include(c => c.Prasmes)
                                        .FirstOrDefault(c => c.ID == id);

            if (cvToDelete != null)
            {
                _context.Izglitiba.RemoveRange(cvToDelete.Izglitiba);
                _context.DarbaPieredze.RemoveRange(cvToDelete.DarbaPieredzes);
                _context.Prasmes.RemoveRange(cvToDelete.Prasmes);

                _context.CV.Remove(cvToDelete);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(CVSaraksts));
        }
    }
}