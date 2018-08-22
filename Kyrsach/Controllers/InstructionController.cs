using Kyrsach.Data;
using Kyrsach.Models;
using Kyrsach.Models.InstructionViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Kyrsach.Controllers
{
    public class InstructionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InstructionController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Index(int id)
        {
            return View(_context.Instruction.Include(s => s.Steps).Single(c => c.Id == id));
        }

        public ActionResult Create(string userId)
        {
            return RedirectToAction("Edit", new { userId });
        }

        private Instruction CreateInstruction(InstructionEditViewModel model)
        {
            Instruction instruction = new Instruction();
            return instruction;
        }

        private InstructionEditViewModel CreateInstructionModel(Instruction instruction)
        {
            InstructionEditViewModel model = new InstructionEditViewModel();
            return model;
        }

        private InstructionEditViewModel Update(InstructionEditViewModel model)
        {
            if (model.ChangeStepsCount == -1) model.Steps.RemoveAt(model.Steps.Count - 1);
            if (model.ChangeStepsCount == 1) model.Steps.Add(new StepEditViewModel());
            model.ChangeStepsCount = 0;
            return model;
        }

        public ActionResult Edit(string userId, int? id)
        {
            InstructionEditViewModel model;
            if (id == null)
            {
                model = new InstructionEditViewModel() { AuthorId = userId, Steps = new List<StepEditViewModel> { new StepEditViewModel() } };
            }
            else
            {
                model = CreateInstructionModel(_context.Instruction.Find(id));
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InstructionEditViewModel model)
        {
            if (model.ChangeStepsCount != 0) return View(Update(model));
            if (ModelState.IsValid)
            {
                Instruction instruction = CreateInstruction(model);
                _context.Instruction.Add(instruction);
                _context.SaveChanges();
                return RedirectToAction("Index", "Manage");// TODO Change on instraction View
            }
            return View(model);
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