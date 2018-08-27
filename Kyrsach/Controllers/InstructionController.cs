using Kyrsach.Data;
using Kyrsach.Models;
using Kyrsach.Models.InstructionViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            Instruction instruction = _context.Instructions.Include(s => s.Steps).Include(u => u.ApplicationUser).Single(c => c.Id == id);
            instruction.Steps.OrderBy(i => i.Index);
            return View(instruction);
        }

        [Authorize]
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
                CategoryId = model.Category.Id,
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
                Index = model.Index,
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
                Index = step.Index,
                Name = step.Name,
                Text = step.Text,
                Image1 = step.Image1,
                Image2 = step.Image2,
                Image3 = step.Image3
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

        [Authorize]
        public ActionResult Edit(string userId, int? id)
        {
            InstructionEditViewModel model;
            if (id == null)
            {
                model = new InstructionEditViewModel() { AuthorId = userId, Steps = new List<StepEditViewModel> { new StepEditViewModel() } };
            }
            else
            {
                Instruction instruction = _context.Instructions.Include(a => a.Steps).Include(a => a.Category).Single(i => i.Id == id);
                instruction.Steps.OrderBy(i => i.Index);
                model = CreateInstructionModel(instruction);
            }
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string userId, int? id, InstructionEditViewModel model)
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
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
            Instruction instruction = _context.Instructions.Include(a => a.Steps).Include(a => a.Category).FirstOrDefault(x => x.Id == model.Id);
            if (instruction != null)
            {
                _context.Remove(instruction);
                _context.SaveChanges();
                instruction = CreateInstruction(model);
                _context.Instructions.Add(instruction);
                _context.SaveChanges();
            }
            return instruction.Id;
        }

        private void UpdateStep(StepEditViewModel stepView, Step stepToUpdate)
        {
            stepToUpdate.Name = stepView.Name;
            stepToUpdate.Text = stepView.Text;
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            _context.Remove(_context.Instructions.Find(id));
            _context.SaveChanges();
            return RedirectToAction("Instructions", "Manage");
        }


        public async Task<IActionResult> Search(int? category, string request, int page = 1)
        {
            int pageSize = 5;
            IQueryable<Instruction> instructions = Search(_context.Instructions.Include(x => x.Category).Include(u => u.ApplicationUser).OrderByDescending(s => s.UpdateDate), category, request);
            var count = await instructions.CountAsync();
            var items = await instructions.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            InstractionForTableViewModel viewModel = new InstractionForTableViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                FilterViewModel = new FilterViewModel(_context.Categories.ToList(), category, request),
                Instructions = items
            };
            return View(viewModel);
        }

        private IQueryable<Instruction> Search(IQueryable<Instruction> instructions, int? category, string request)
        {
            if (category != null && category != 0)
            {
                instructions = instructions.Where(p => p.CategoryId == category);
            }
            if (!String.IsNullOrEmpty(request))
            {
                instructions = instructions.Where(p => p.Name.Contains(request));
            }
            return instructions;
        }
    }
}