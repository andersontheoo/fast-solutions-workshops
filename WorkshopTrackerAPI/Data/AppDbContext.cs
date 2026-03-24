using Microsoft.EntityFrameworkCore;
using SeuProjeto.Models;

namespace SeuProjeto.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Workshop> Workshops { get; set; }
        public DbSet<Colaborador> Colaboradores { get; set; }
        public DbSet<WorkshopColaborador> WorkshopColaboradores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkshopColaborador>()
                .HasKey(wc => new { wc.WorkshopId, wc.ColaboradorId });

            modelBuilder.Entity<WorkshopColaborador>()
                .HasOne(wc => wc.Workshop)
                .WithMany(w => w.WorkshopColaboradores)
                .HasForeignKey(wc => wc.WorkshopId);

            modelBuilder.Entity<WorkshopColaborador>()
                .HasOne(wc => wc.Colaborador)
                .WithMany(c => c.WorkshopColaboradores)
                .HasForeignKey(wc => wc.ColaboradorId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
