using Microsoft.AspNetCore.Mvc;
using LatvijasPastsCV.Services;
using LatvijasPastsCV.Models;

namespace Pasts.Controllers
{
    public class DarbaPieredzeController : Controller
    {
        private readonly IDarbaPieredzeService _service;

        public DarbaPieredzeController(IDarbaPieredzeService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View(_service.GetAll());
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var darbaPieredze = _service.GetById(id.Value);
            if (darbaPieredze == null)
            {
                return NotFound();
            }

            return View(darbaPieredze);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DarbaPieredze darbaPieredze)
        {
            if (ModelState.IsValid)
            {
                _service.Add(darbaPieredze);
                return RedirectToAction(nameof(Index));
            }
            return View(darbaPieredze);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var darbaPieredze = _service.GetById(id.Value);
            if (darbaPieredze == null)
            {
                return NotFound();
            }
            return View(darbaPieredze);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, DarbaPieredze darbaPieredze)
        {
            if (id != darbaPieredze.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _service.Update(darbaPieredze);
                return RedirectToAction(nameof(Index));
            }
            return View(darbaPieredze);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var darbaPieredze = _service.GetById(id.Value);
            if (darbaPieredze == null)
            {
                return NotFound();
            }

            return View(darbaPieredze);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}