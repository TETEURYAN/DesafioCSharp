using Desafio_3._1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_3._1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Agenda> Agendas { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurações adicionais (opcional)
            modelBuilder.Entity<Paciente>()
                .HasKey(p => p.CPF);

            modelBuilder.Entity<Agenda>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Agenda>()
                .HasOne(a => a.Paciente)
                .WithMany()
                .HasForeignKey(a => a.CPF);
        }
    }
}
