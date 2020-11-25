using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.Data;
using SmartSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartSchool.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AlunoController : ControllerBase
  {
    public readonly IRepository _repo;

    public AlunoController( IRepository repo)
    {
      _repo = repo;
    }

  
    // GET: api/<AlunoController>
    [HttpGet]
    public IActionResult Get()
    {
      var result = _repo.GetAllAlunos(true);
      return Ok(result);

    }

    // GET api/<AlunoController>/5
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
      var aluno = _repo.GetAlunoById(id, true);
      if (aluno != null)
        return Ok(aluno);
      return BadRequest("O Aluno não foi encontrado.");
    }

 
    // POST api/<AlunoController>
    [HttpPost]
    public IActionResult Post(Aluno aluno)
    {
       _repo.Add(aluno);
       if(_repo.SaveChanges()) return Created("Aluno Criado", aluno);

      return BadRequest("Aluno não cadastrado.");
    }

    // PUT api/<AlunoController>/5
    [HttpPut("{id}")]
    public IActionResult Put(int id, Aluno aluno)
    {
      var alu =  _repo.GetAlunoById(id);
      if (alu == null) return BadRequest("Aluno nao encontrado");
       
       _repo.Update(aluno);
       if(_repo.SaveChanges()) return Ok(aluno);

      return BadRequest("Aluno não atualizado.");
    }

    // Patch api/<AlunoController>/5
    [HttpPatch("{id}")]
    public IActionResult Patch(int id, Aluno aluno)
    {
      var alu =  _repo.GetAlunoById(id);
      if (alu == null) return BadRequest("Aluno nao encontrado");
     
       _repo.Update(aluno);
       if(_repo.SaveChanges()) return Ok(aluno);

      return BadRequest("Aluno não atualizado.");
    }

    // DELETE api/<AlunoController>/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
       var aluno = _repo.GetAlunoById(id);
      if (aluno == null) return BadRequest("Aluno nao encontrado");
     
       _repo.Remove(aluno);
       if(_repo.SaveChanges()) return NoContent();

      return BadRequest("Aluno não deletado.");
    }
  }
}
