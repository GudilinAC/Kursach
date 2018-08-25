using Kyrsach.Data;
using Kyrsach.Models;
using Kyrsach.Models.InstructionViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
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
            return View(_context.Instructions.Include(s => s.Steps).Single(c => c.Id == id));
        }

        public ActionResult Create(string creatorId)
        {
            return RedirectToAction("Edit", new { userId = creatorId });
        }

        private Instruction CreateInstruction(InstructionEditViewModel model)
        {
            List<Step> stepList = new List<Step>();
            model.Steps.ForEach(step => stepList.Add(CreateStep(step)));
            Instruction instruction = new Instruction()
            {
                Name = model.Name,
                Category = model.Category,
                Description = model.Description,
                Image = model.Image,
                UpdateDate = DateTime.Now,
                ApplicationUserId = model.AuthorId,
                Steps = stepList
            };
            return instruction;
        }

        private Step CreateStep(StepEditViewModel model)
        {
            Step step = new Step()
            {
                Name = model.Name,
                Text = model.Text,
                Image1 = model.Image1,
                Image2 = model.Image2,
                Image3 = model.Image3
            };
            return step;
        }

        private InstructionEditViewModel CreateInstructionModel(Instruction instruction)
        {
            List<StepEditViewModel> stepList = new List<StepEditViewModel>();
            instruction.Steps.ToList().ForEach(step => stepList.Add(CreateStepViewModel(step)));
            InstructionEditViewModel model = new InstructionEditViewModel()
            {
                Id = instruction.Id,
                AuthorId = instruction.ApplicationUserId,
                Name = instruction.Name,
                Category = instruction.Category,
                Description = instruction.Description,
                Image = instruction.Image,
                Steps = stepList
            };
            return model;
        }

        private StepEditViewModel CreateStepViewModel(Step step)
        {
            StepEditViewModel stepView = new StepEditViewModel()
            {
                Id = step.Id,
                Name = step.Name,
                Text = step.Text
            };
            return stepView;
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
                model = CreateInstructionModel(_context.Instructions.Find(id));
            }
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InstructionEditViewModel model)
        {
            if (model.ChangeStepsCount != 0) return View(Update(model));
            if (ModelState.IsValid)
            {
                int temp_id;
                if (model.Id == 0) temp_id = AddInstruction(model);
                else temp_id = UpdateInstruction(model);
                return RedirectToAction("Index", "Instruction", new { id = temp_id });
            }
            return View(model);
        }

        private int AddInstruction(InstructionEditViewModel model)
        {

            Instruction instruction = CreateInstruction(model);
            _context.Instructions.Add(instruction);
            _context.SaveChanges();
            return instruction.Id;
        }

        private int UpdateInstruction(InstructionEditViewModel model)
        {
            Instruction instruction = _context.Instructions.Find(model.Id);
            if (instruction != null)
            {
                instruction.UpdateDate = DateTime.Now;
                _context.Entry(instruction).CurrentValues.SetValues(model);
                foreach (var step in instruction.Steps.ToList())
                {
                    if (!model.Steps.Any(c => c.Id == step.Id))
                        _context.Steps.Remove(step);
                }
                foreach (var step in model.Steps)
                {
                    var existingStep = instruction.Steps
                        .Where(c => c.Id == step.Id && c.Id != default(int))
                        .SingleOrDefault();

                    if (existingStep != null)
                        _context.Entry(existingStep).CurrentValues.SetValues(step);
                    else
                    {
                        instruction.Steps.Add(CreateStep(step));
                    }
                }
                _context.SaveChanges();
            }
            return instruction.Id;
        }

        private void UpdateStep(StepEditViewModel stepView, Step stepToUpdate)
        {
            stepToUpdate.Name = stepView.Name;
            stepToUpdate.Text = stepView.Text;
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