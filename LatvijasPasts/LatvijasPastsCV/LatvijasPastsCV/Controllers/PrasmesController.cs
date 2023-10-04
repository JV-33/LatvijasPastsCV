using Microsoft.AspNetCore.Mvc;
using LatvijasPastsCV.Models;
using LatvijasPastsCV.Services;

namespace Pasts.Controllers
{
    public class PrasmesController : Controller
    {
        private readonly IPrasmesService _prasmesService;

        public PrasmesController(IPrasmesService prasmesService)
        {
            _prasmesService = prasmesService;
        }

        public IActionResult Index()
        {
            return View(_prasmesService.GetAll());
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prasmes = _prasmesService.GetById(id.Value);
            if (prasmes == null)
            {
                return NotFound();
            }

            return View(prasmes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Prasmes prasmes)
        {
            if (ModelState.IsValid)
            {
                _prasmesService.Add(prasmes);
                _prasmesService.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(prasmes);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prasmes = _prasmesService.GetById(id.Value);
            if (prasmes == null)
            {
                return NotFound();
            }
            return View(prasmes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Prasmes prasmes)
        {
            if (id != prasmes.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _prasmesService.Update(prasmes);
                _prasmesService.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(prasmes);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prasmes = _prasmesService.GetById(id.Value);
            if (prasmes == null)
            {
                return NotFound();
            }

            return View(prasmes);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var prasmes = _prasmesService.Prasmes.Find(id);
            if (prasmes != null)  
            {
                _prasmesService.Prasmes.Remove(prasmes);
                _prasmesService.SaveChanges();
            }
            else
            {

            }
            return RedirectToAction(nameof(Index));
        }
    }
}