using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kyrsach.Models
{
    public class Instruction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public int CateforyId { get; set; }
        [ForeignKey("CateforyId")]
        public virtual Category Category { get; set; }
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<Step> Steps { get; set; } = new List<Step>();
        public virtual ICollection<InstractionTag> InstractionTags { get; set; } = new List<InstractionTag>();
    }
}
