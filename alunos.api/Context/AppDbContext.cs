using alunos.api.Entities;
using Microsoft.EntityFrameworkCore;


namespace alunos.api.Context
{
    public partial class AppDbContext : DbContext
    {
        //Definindo o construtor.
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


        //Mapeamento para a tabela de usuário.
        public DbSet<Aluno> Alunos { get; set; }

        public DbSet<Temporizador> Temporizador { get; set; }

        //Definindo configuração da criação do modelo.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Definindo a quantidade máxima de caracteres.
            modelBuilder.Entity<Aluno>()
                .Property(p => p.Nome)
                .HasMaxLength(50);

            modelBuilder.Entity<Aluno>()
                .Property(p => p.SobreNome)
                .HasMaxLength(30);

            modelBuilder.Entity<Aluno>()
                .Property(p => p.Email)
                .HasMaxLength(100);

            modelBuilder.Entity<Aluno>()
                .Property(p => p.Telefone)
                .HasMaxLength(15);

            modelBuilder.Entity<Aluno>();

            modelBuilder.Entity<Temporizador>()
                .HasData(
                    new Temporizador { Id = 1, Temp = 5 });
        }
    }
}
