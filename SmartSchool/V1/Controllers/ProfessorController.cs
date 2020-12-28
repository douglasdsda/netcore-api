using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.Data;
using SmartSchool.V1.Dtos;
using SmartSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartSchool.V1.Controllers
{
  [Route("api/v{version:apiVersion}/[controller]")]
  [ApiVersion("1.0")]
  [ApiController]
  public class ProfessorController : ControllerBase
  {
    private readonly IRepository _repo;

   private readonly IMapper _mapper;
    public ProfessorController(IRepository repo, IMapper mapper)
    {
      _repo = repo;
      _mapper = mapper;
    }
    // GET: api/<ProfessorController>
    [HttpGet]
    public ActionResult Get()
    {
      var professores = _repo.GetAllProfessores(true);
      return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(professores));
    }

    // GET api/<ProfessorController>/5
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
      var professor = _repo.GetAProfessorById(id, true);
      if (professor != null){
        var professorDto = _mapper.Map<ProfessorDto>(professor);
        return Ok(professorDto);
      }
      return BadRequest("O Professor não foi encontrado.");
    }

    // POST api/<ProfessorController>
    [HttpPost]
    public IActionResult Post(ProfessorRegisterDto model)
    {
      var professor = _mapper.Map<Aluno>(model);
      _repo.Add(professor);
      if (_repo.SaveChanges()) return Created($"api/professor/{professor.Id}", _mapper.Map<ProfessorDto>(professor));

      return BadRequest("Professor não cadastrado.");
    }

    // PUT api/<ProfessorController>/5
    [HttpPut("{id}")]
    public IActionResult Put(int id, ProfessorRegisterDto model)
    {
      var professor = _repo.GetAProfessorById(id);
      if (professor == null) return BadRequest("Professor nao encontrado");
      
      _mapper.Map(model, professor);
      _repo.Update(professor);
      if (_repo.SaveChanges()) return Ok(_mapper.Map<ProfessorDto>(professor));

      return BadRequest("Professor não cadastrado.");
    }
    
    // GET api/<Professor>/5
    [HttpGet("byaluno/{alunoId}")]
    public IActionResult ByAluno(int alunoId)
    {
     var professores = _repo.GetAProfessorByAlunoId(alunoId, true);
      if (professores == null) return BadRequest("Professores nao encontrado");
 
      return Ok( _mapper.Map<IEnumerable<ProfessorDto>>(professores));
    }

    // Patch api/<ProfessorController>/5
    [HttpPatch("{id}")]
    public IActionResult Patch(int id, ProfessorRegisterDto model)
    {
      var professor = _repo.GetAProfessorById(id);
      if (professor == null) return BadRequest("Professor nao encontrado");
      
      _mapper.Map(model, professor);
      _repo.Update(professor);
      if (_repo.SaveChanges()) return Ok(_mapper.Map<ProfessorDto>(professor));

      return BadRequest("Professor não cadastrado.");
    }

    // DELETE api/<ProfessorController>/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      var prof = _repo.GetAProfessorById(id);
      if (prof == null) return BadRequest("Professor nao encontrado");
      _repo.Remove(prof);
      if(_repo.SaveChanges()){
        return NoContent();
      }
      return BadRequest("Professor não deletado");
    }
  }
}
