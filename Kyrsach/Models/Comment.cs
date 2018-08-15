using Kyrsach.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
        public int InstructionId { get; set; }
        [ForeignKey("InstructionId")]
        public Instruction Instruction { get; set; }
        public string Text { get; set; }
    }
}
