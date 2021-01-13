using Api_ProjetoEscola.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Api_ProjetoEscola.Data
{
    public class DataContext : DbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{            
        //    optionsBuilder.UseSqlServer("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=dbProjetoEscola;Data Source=ALPHAS0004\\DESE");
        //}
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            //    //Instanciei o datacontext o data recebe os parametros   depois que receber os parametros por meio do base passa para o Dbcontext a informacao
        }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Professor> Professores { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Professor>().HasData(

                new List<Professor>()
                {
                    new Professor()
                    {
      
                        Id= 1,
                        Nome = "Vinicius Prof",
                        Materia = "Ed.fisica"

                    },
                     new Professor()
                    {
                        Id= 2,
                        Nome = "Marco Prof",
                        Materia = "Matematica"

                    },
                      new Professor()
                    {
                        Id= 3,
                        Nome = "Fernando Prof",
                        Materia = "Portugues"

                    },
                }
                );

            builder.Entity<Aluno>().HasData(

                new List<Aluno>()
                {
                    new Aluno()
                    {

                        AlunoId= 1,
                        Nome = "Italo",
                        Sobrenome="Silva",
                        DataNascimento="10/05/1993",
                        ProfessorId=1
                        

                    },
                     new Aluno()
                    {
                        AlunoId= 2,
                        Nome = "Igor",
                        Sobrenome="Cavalcante",
                        DataNascimento="05/03/2002",
                          ProfessorId=3


                    },
                      new Aluno()
                    {
                        AlunoId= 3,
                        Nome = "Jaime",
                        Sobrenome="Ferreira",
                        DataNascimento="10/05/1973",
                          ProfessorId=3

                    },
                }
                );
        }

    }
}
