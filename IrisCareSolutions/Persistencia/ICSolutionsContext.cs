using IrisCareSolutions.Models;
using Microsoft.EntityFrameworkCore;

namespace IrisCareSolutions.Persistencia
{
    public class ICSolutionsContext : DbContext
    {
        public DbSet<Tutelado> Tutelados { get; set; }
        public DbSet<Lembrete> Lembretes { get; set; }
        public DbSet<Exame> Exames { get; set; }
        public DbSet<TuteladoLembrete> TuteladosLembretes { get; set; }

        public ICSolutionsContext(DbContextOptions op) : base(op) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TuteladoLembrete>()
                .HasKey(p => new { p.TuteladoId, p.LembreteId });

            modelBuilder.Entity<TuteladoLembrete>()
                .HasOne(p => p.Tutelado)
                .WithMany(p => p.TuteladosLembretes)
                .HasForeignKey(p => p.TuteladoId);

            modelBuilder.Entity<TuteladoLembrete>()
                .HasOne(p => p.Lembrete)
                .WithMany(p => p.TuteladosLembretes)
                .HasForeignKey(p => p.LembreteId);

            modelBuilder.Entity<Exame>()
               .Property(e => e.Nome)
               .IsUnicode(false);

            modelBuilder.Entity<Exame>()
                .Property(e => e.Descricao)
                .IsUnicode(false);

            modelBuilder.Entity<Exame>()
                .Property(e => e.ResultadoPath)
                .IsUnicode(false);

            modelBuilder.Entity<Exame>()
                .Property(e => e.ResultadoFileName)
                .IsUnicode(false);

            base.OnModelCreating(modelBuilder);


        }

    }
}
