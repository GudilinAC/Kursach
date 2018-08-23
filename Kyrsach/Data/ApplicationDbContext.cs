using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Kyrsach.Models;
using Kyrsach.Models.InstructionViewModels;

namespace Kyrsach.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<InstractionTag>()
            .HasKey(t => new { t.InstructionId, t.TagId });

            builder.Entity<InstractionTag>()
                .HasOne(it => it.Instruction)
                .WithMany(i => i.InstractionTags)
                .HasForeignKey(it => it.InstructionId);

            builder.Entity<InstractionTag>()
                .HasOne(it => it.Tag)
                .WithMany(t => t.InstractionTags)
                .HasForeignKey(it => it.TagId);
        }

        public DbSet<Instruction> Instructions { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
