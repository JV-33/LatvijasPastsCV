using Microsoft.AspNetCore.Mvc;
using LatvijasPastsCV.Models;
using LatvijasPastsCV.Services;
using Microsoft.EntityFrameworkCore;

namespace Pasts.Controllers
{
    public class AdreseController : Controller
    {
        private readonly IAdreseService _adreseService;

        public AdreseController(IAdreseService adreseService)
        {
            _adreseService = adreseService;
        }

        public IActionResult Index()
        {
            return View(_adreseService.GetAll());
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adrese = _adreseService.GetById(id.Value);
            if (adrese == null)
            {
                return NotFound();
            }

            return View(adrese);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Adrese adrese)
        {
            if (ModelState.IsValid)
            {
                _adreseService.Add(adrese);
                return RedirectToAction(nameof(Index));
            }
            return View(adrese);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adrese = _adreseService.GetById(id.Value);
            if (adrese == null)
            {
                return NotFound();
            }
            return View(adrese);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Adrese adrese)
        {
            if (id != adrese.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _adreseService.Update(adrese);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdreseExists(adrese.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(adrese);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adrese = _adreseService.GetById(id.Value);
            if (adrese == null)
            {
                return NotFound();
            }

            return View(adrese);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _adreseService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool AdreseExists(int id)
        {
            return _adreseService.GetAll().Any(e => e.ID == id);
        }
    }
}