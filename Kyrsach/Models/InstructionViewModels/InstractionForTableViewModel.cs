using System.Collections.Generic;

namespace Kyrsach.Models.InstructionViewModels
{
    public class InstractionForTableViewModel
    {
        public IEnumerable<Instruction> Instructions { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
    }
}
