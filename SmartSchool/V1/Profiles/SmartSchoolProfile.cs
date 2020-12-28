using AutoMapper;
using SmartSchool.V1.Dtos;
using SmartSchool.Models;
using SmartSchool.Helpers;

namespace SmartSchool.V1.Profiles
{
    public class SmartSchoolProfile : Profile
    {
       public SmartSchoolProfile(){

             CreateMap<Aluno, AlunoDto>()
                .ForMember(
                    dest => dest.Nome,
                    opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}")
                )
                .ForMember(
                    dest => dest.Idade,
                    opt => opt.MapFrom(src => src.DataNasc.GetCurrentAge())
                );

                CreateMap<AlunoDto, Aluno>();
                CreateMap<Aluno, AlunoRegistrarDto>().ReverseMap();
                CreateMap<Aluno, AlunoPatchDto>().ReverseMap();

                CreateMap<Professor, ProfessorDto>().ReverseMap();
                CreateMap<Professor, ProfessorRegisterDto>().ReverseMap();
                CreateMap<DisciplinaDto, Disciplina>().ReverseMap();
                CreateMap<CursoDto, Curso>().ReverseMap();


                
       }
    }
}