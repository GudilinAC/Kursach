using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kyrsach.Models.InstructionViewModels
{
    public class HomeViewModel
    {
        public List<InstructionTitleViewModel> Random { get; set; } = new List<InstructionTitleViewModel>();
        public List<InstructionTitleViewModel> Newest { get; set; } = new List<InstructionTitleViewModel>();
    }
}
