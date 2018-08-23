using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kyrsach.Models
{
    public class InstractionTag
    {
        public int InstructionId { get; set; }
        public virtual Instruction Instruction { get; set; }

        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
