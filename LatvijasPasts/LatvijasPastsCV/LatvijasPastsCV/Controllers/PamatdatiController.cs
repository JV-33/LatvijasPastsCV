using Microsoft.AspNetCore.Mvc;
using LatvijasPastsCV.Models;
using LatvijasPastsCV.Services;
namespace Pasts.Controllers
{
    public class PamatdatiController : Controller
    {
        private readonly IPamatdatiService _pamatdatiService;

        public PamatdatiController(IPamatdatiService pamatdatiService)
        {
            _pamatdatiService = pamatdatiService;
        }

        public IActionResult Index()
        {
            return View(_pamatdatiService.GetAll());
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pamatdati = _pamatdatiService.GetById(id.Value);
            if (pamatdati == null)
            {
                return NotFound();
            }

            return View(pamatdati);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pamatdati newPamatdati)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _pamatdatiService.Add(newPamatdati);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Log the exception here if needed
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the data. Please try again later.");
                }
            }

            return View(newPamatdati);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pamatdati = _pamatdatiService.GetById(id.Value);
            if (pamatdati == null)
            {
                return NotFound();
            }
            return View(pamatdati);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Pamatdati pamatdati)
        {
            if (id != pamatdati.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _pamatdatiService.Update(pamatdati);
                return RedirectToAction(nameof(Index));
            }
            return View(pamatdati);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pamatdati = _pamatdatiService.GetById(id.Value);
            if (pamatdati == null)
            {
                return NotFound();
            }

            return View(pamatdati);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var pamatdati = _pamatdatiService.GetById(id);
            if (pamatdati == null)
            {
                return NotFound();
            }

            _pamatdatiService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}