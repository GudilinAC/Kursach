using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Kyrsach.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Instruction> Instructions { get; set; }

        public ApplicationUser()
        {
            Instructions = new List<Instruction>();
        }
    }
}
