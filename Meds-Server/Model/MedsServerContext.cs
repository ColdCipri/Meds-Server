using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Meds_Server.Model
{
    public partial class MedsServerContext : DbContext
    {
        public MedsServerContext()
        {
        }

        public MedsServerContext(DbContextOptions<MedsServerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Meds> Meds { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source = CIPRI-ROG\\SQLEXPRESS; Initial Catalog = Meds; Integrated Security = True; ConnectRetryCount=0");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meds>(entity =>
            {
                
                entity.Property(e => e.BaseSubstanceQuantity).IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.Type).IsUnicode(false);
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
