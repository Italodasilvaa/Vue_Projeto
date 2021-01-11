using Api_ProjetoEscola.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_ProjetoEscola.Data
{
    public class Repository : IRepository
    {
        public DataContext _context { get; }
        public Repository(DataContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }


        //ALUNOS
        public async Task<Aluno[]> GetAllAlunosAsync(bool includeProfessor )
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(p => p.Professor);
            }

            query = query.AsNoTracking().OrderBy(a => a.AlunoId);

            return await query.ToArrayAsync();
        }

        public async Task<Aluno[]> GetAlunosAsyncProfessorId(int ProfessorId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(p => p.Professor);
            }

            query = query.AsNoTracking()
                .OrderBy(a => a.AlunoId)
                .Where(aluno => aluno.ProfessorId == ProfessorId);

            return await query.ToArrayAsync();
        }

        public async Task<Aluno> GetAlunoAsyncById(int AlunoId, bool includeProfessor)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(p => p.Professor);
            }

            query = query.AsNoTracking().OrderBy(a => a.AlunoId).Where(aluno => aluno.AlunoId == AlunoId);

            return await query.FirstOrDefaultAsync();
        }


        //PROFESSORES
        public async Task<Professor[]> GetAllProfessoresAsync(bool includeAluno = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeAluno)
            {
                query = query.Include(p => p.Alunos);
            }

            query = query.AsNoTracking().OrderBy(a => a.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Professor> GetProfessorAsyncById(int ProfessorId, bool includeAluno )
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeAluno)
            {
                query = query.Include(p => p.Alunos);
            }

            query = query.AsNoTracking()
                .OrderBy(a => a.Id)
                .Where(professor => professor.Id == ProfessorId);

            return await query.FirstOrDefaultAsync();
        }
    }
}
