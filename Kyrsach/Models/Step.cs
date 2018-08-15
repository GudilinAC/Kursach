using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Models
{
    public class Step
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public int InstructionId { get; set; }
        [ForeignKey("InstructionId")]
        public Instruction Instruction { get; set; }
    }
}
