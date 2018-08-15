using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kyrsach.Models
{
    public class Instruction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desription { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Step> Steps { get; set; }

        public Instruction()
        {
            Comments = new List<Comment>();
            Steps = new List<Step>();
        }
    }
}
