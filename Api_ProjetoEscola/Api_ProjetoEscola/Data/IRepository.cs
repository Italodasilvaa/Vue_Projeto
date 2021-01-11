using Api_ProjetoEscola.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_ProjetoEscola.Data
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;

        Task<Aluno[]> GetAllAlunosAsync(bool includeProfessor);
        Task<Aluno[]> GetAlunosAsyncProfessorId(int ProfessorId ,bool includeProfessor);
        Task<Aluno> GetAlunoAsyncById(int AlunoId,bool includeProfessor);



        Task<Professor[]> GetAllProfessoresAsync(bool includeAluno);
       
        Task<Professor> GetProfessorAsyncById(int ProfessorId, bool includeAluno);

        Task<bool> SaveChangesAsync();


    }
}
