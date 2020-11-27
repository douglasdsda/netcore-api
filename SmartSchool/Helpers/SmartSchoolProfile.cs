using AutoMapper;
using SmartSchool.Dtos;
using SmartSchool.Models;

namespace SmartSchool.Helpers
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

                CreateMap<Professor, ProfessorDto>().ReverseMap();
                CreateMap<Professor, ProfessorRegisterDto>().ReverseMap();
                
       }
    }
}