using System.ComponentModel.DataAnnotations.Schema;

namespace Kyrsach.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public int InstructionId { get; set; }
        [ForeignKey("InstructionId")]
        public virtual Instruction Instruction { get; set; }
        public string Text { get; set; }
    }
}
