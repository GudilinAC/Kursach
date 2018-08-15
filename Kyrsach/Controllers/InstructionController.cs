using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kyrsach.Controllers
{
    public class InstructionController : Controller
    {
        // GET: Instruction
        public ActionResult Index()
        {
            return View();
        }

        // GET: Instruction/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Instruction/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Instruction/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Instruction/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Instruction/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Instruction/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Instruction/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}