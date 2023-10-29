using Microsoft.AspNetCore.Mvc;
using LatvijasPastsCV.Models;
using LatvijasPastsCV.Services;

namespace Pasts.Controllers
{
    public class IzglitibaController : Controller
    {
        private readonly IIzglitibaService _izglitibaService;

        public IzglitibaController(IIzglitibaService izglitibaService)
        {
            _izglitibaService = izglitibaService;
        }

        public IActionResult Index()
        {
            return View(_izglitibaService.GetAll());
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var izglitiba = _izglitibaService.GetById(id.Value);
            if (izglitiba == null)
            {
                return NotFound();
            }

            return View(izglitiba);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Izglitiba izglitiba)
        {
            if (ModelState.IsValid)
            {
                _izglitibaService.Add(izglitiba);
                return RedirectToAction(nameof(Index));
            }
            return View(izglitiba);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var izglitiba = _izglitibaService.GetById(id.Value);
            if (izglitiba == null)
            {
                return NotFound();
            }
            return View(izglitiba);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Izglitiba izglitiba)
        {
            if (id != izglitiba.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _izglitibaService.Update(izglitiba);
                return RedirectToAction(nameof(Index));
            }
            return View(izglitiba);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var izglitiba = _izglitibaService.GetById(id.Value);
            if (izglitiba == null)
            {
                return NotFound();
            }

            return View(izglitiba);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _izglitibaService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}