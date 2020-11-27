using System.Linq;
using Microsoft.EntityFrameworkCore;
using SmartSchool.Models;

namespace SmartSchool.Data
{
  public class Repository : IRepository
  {
    private readonly SmartContext _context;
    public Repository(SmartContext context){
      _context = context;
    }
    public void Add<T>(T entity) where T : class
    {
     _context.Add(entity);
    }

    public void Remove<T>(T entity) where T : class
    {
      _context.Remove(entity);
    }

    public bool SaveChanges()
    {
     return (_context.SaveChanges() > 0);
    }

    public void Update<T>(T entity) where T : class
    {
      _context.Update(entity);
    }

     public Aluno[] GetAllAlunos(bool includeProfessor = false)
    {
      IQueryable<Aluno> query = _context.Alunos;
      
      if(includeProfessor){
        query = query.Include(a => a.AlunosDisciplinas)
                     .ThenInclude(ad => ad.Disciplina)
                     .ThenInclude(d => d.Professor);
      
      }
      query = query.AsNoTracking().OrderBy(a => a.Id);

      return query.ToArray();
    }

    public Aluno[] GetAllAlunosByDisciplinaId(int disciplinaId, bool includeProfessor = false)
    {
       IQueryable<Aluno> query = _context.Alunos;
      
      if(includeProfessor){
        query = query.Include(a => a.AlunosDisciplinas)
                     .ThenInclude(ad => ad.Disciplina)
                     .ThenInclude(d => d.Professor);
      
      }
      query = query.AsNoTracking()
                  .OrderBy(a => a.Id)
                  .Where(aluno => aluno.AlunosDisciplinas.Any(ad => ad.DisciplinaId == disciplinaId ));

      return query.ToArray();
    }

    
    public Aluno GetAlunoById(int alunoId, bool includeProfessor = false)
    {
      IQueryable<Aluno> query = _context.Alunos;
      
      if(includeProfessor){
        query = query.Include(a => a.AlunosDisciplinas)
                     .ThenInclude(ad => ad.Disciplina)
                     .ThenInclude(d => d.Professor);
      
      }
      query = query.AsNoTracking()
                  .OrderBy(a => a.Id)
                  .Where(aluno => aluno.Id == alunoId);

      return query.FirstOrDefault();
    }

    public Professor[] GetAllProfessores(bool includeAlunos)
    {
      IQueryable<Professor> query = _context.Professores;
      
      if(includeAlunos){
        query = query.Include(d => d.Disciplinas)
                     .ThenInclude(ad => ad.AlunosDisciplinas)
                     .ThenInclude(a => a.Aluno);
      
      }
      query = query.AsNoTracking().OrderBy(p => p.Id);

      return query.ToArray();
    }

    public Professor[] GetAllProfessoresByDisciplinaId(int disciplinaId, bool includeAlunos = false)
    {
       IQueryable<Professor> query = _context.Professores;
      
      if(includeAlunos){
        query = query.Include(d => d.Disciplinas)
                     .ThenInclude(ad => ad.AlunosDisciplinas)
                     .ThenInclude(a => a.Aluno);
      
      }
  
       query = query.AsNoTracking()
                  .OrderBy(aluno => aluno.Id)
                  .Where(aluno => aluno.Disciplinas.Any(ad => ad.AlunosDisciplinas.Any(a => a.DisciplinaId == disciplinaId) ));

      return query.ToArray();
    }
 

    public Professor GetAProfessorById(int professorId, bool includeAlunos = false)
    {
       IQueryable<Professor> query = _context.Professores;
      
    
      if(includeAlunos){
        query = query.Include(p => p.Disciplinas)
                     .ThenInclude(ad => ad.AlunosDisciplinas)
                     .ThenInclude(a => a.Aluno);
      
      }
      query = query.AsNoTracking()
                  .OrderBy(p => p.Id)
                  .Where(professor => professor.Id == professorId);

      return query.FirstOrDefault();
    }
  }
}