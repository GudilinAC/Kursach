using System.Collections.Generic;

namespace Kyrsach.Models.InstructionViewModels
{
    public class InstructionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public ICollection<Step> Steps { get; set; }

        public InstructionViewModel()
        {
            if (Steps == null)
            {
                Steps = new List<Step>();
            }
        }
    }
}
