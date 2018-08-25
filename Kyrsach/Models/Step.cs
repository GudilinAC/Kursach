using System.ComponentModel.DataAnnotations.Schema;

namespace Kyrsach.Models
{
    public class Step
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public int InstructionId { get; set; }
        [ForeignKey("InstructionId")]
        public virtual Instruction Instruction { get; set; }
    }
}
