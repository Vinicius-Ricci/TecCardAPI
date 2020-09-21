using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TecCardAPI.Aplicacao.Entidades;

namespace TecCardAPI.InfraEstrutura.BancoDados
{
    public class BancoContexto : DbContext
    {

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Curso> Curso { get; set; }
        public DbSet<Acesso> Acesso { get; set; }
        public DbSet<Status> Status { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=teccard;user=root;password=Vini2468765*");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Curso>(c => {

                c.HasKey(e => e.Codigo); //Nessa linha a gente esta dizendo ao BD qual é a chave primaria
                c.Property(e => e.Codigo).ValueGeneratedOnAdd(); //Essa linha a gente esta dizendo que o codigo é Auto Incremento
                c.Property(e => e.Nome).HasMaxLength(60).IsRequired(); //Dizendo que a coluna Nome é um campo obrigatorio


            });

            modelBuilder.Entity<Usuario>(a =>
            {
                a.HasKey(e => e.RM);
                a.Property(e => e.Nome).HasMaxLength(100).IsRequired();
                a.Property(e => e.Email).HasMaxLength(100).IsRequired();
                a.Property(e => e.Tipo).HasMaxLength(100).IsRequired();
                a.Property(e => e.Senha).HasMaxLength(128).IsRequired();
                a.Property(e => e.QrCode).HasColumnType("longtext").IsRequired();
                a.Property(e => e.Foto).HasColumnType("longtext");
                a.HasOne(e => e.Curso).WithMany(c => c.Usuarios).IsRequired();
                a.HasIndex(e => e.Email).IsUnique();

            });

            modelBuilder.Entity<Acesso>(a => {

                a.HasKey(e => e.Codigo);
                a.Property(e => e.Codigo).ValueGeneratedOnAdd();
                a.Property(e => e.Data).HasDefaultValue(DateTime.Now);
                a.Property(e => e.Status).HasMaxLength(45).IsRequired();
                a.Property(e => e.Resultado).HasMaxLength(45).IsRequired();
                a.HasOne(e => e.Usuario).WithMany(r => r.Acessos).IsRequired();


            });

            modelBuilder.Entity<Status>(s => {

                s.HasKey(e => e.Codigo);
                s.Property(e => e.Codigo).ValueGeneratedOnAdd();
                s.Property(e => e.Descricao).HasMaxLength(45).IsRequired();
                s.Property(e => e.DataInicio).HasDefaultValue(DateTime.Now).IsRequired();
                s.Property(e => e.DataFim);
                s.HasOne(e => e.Usuario).WithMany(r => r.Situacoes).IsRequired();


            });
        }
    }
}
