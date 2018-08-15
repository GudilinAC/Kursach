using System.ComponentModel.DataAnnotations.Schema;

namespace Kyrsach.Models
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
