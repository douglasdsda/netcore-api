using SmartSchool.Models;

namespace SmartSchool.Data
{
    public interface IRepository
    {
        void Add<T>(T entity) where T: class;
        void Update<T>(T entity) where T: class;
        void Remove<T>(T entity) where T: class;
        bool SaveChanges();

        // Alunos
        Aluno[] GetAllAlunos(bool includeProfessor = false);
        Aluno[] GetAllAlunosByDisciplinaId(int disciplinaId, bool includeProfessor = false);
        Aluno GetAlunoById(int alunoId, bool includeProfessor = false);
      
        // Professores
        Professor[] GetAllProfessores(bool includeAlunos);
        Professor[] GetAllProfessoresByDisciplinaId(int disciplinaId, bool includeAlunos = false);
        Professor GetAProfessorById(int professorId, bool includeAlunos = false);
    }
}