using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kyrsach.Models.InstructionViewModels
{
    public class InstructionEditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AuthorId { get; set; }
        public List<StepEditViewModel> Steps { get; set; } = new List<StepEditViewModel>();
        public int ChangeStepsCount { get; set; } = 0;
    }
}
