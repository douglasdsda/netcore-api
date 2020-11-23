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
    private readonly SmartContext _context;

    public AlunoController(SmartContext context)
    {
      _context = context;
    }

    // GET: api/<AlunoController>
    [HttpGet]
    public IActionResult Get()
    {
      return Ok(_context.Alunos);
    }

    // GET api/<AlunoController>/5
    [HttpGet("byId/{id}")]
    public IActionResult GetById(int id)
    {
      Aluno aluno = _context.Alunos.FirstOrDefault(item => item.Id == id);
      if (aluno != null)
        return Ok(aluno);
      return BadRequest("O Aluno não foi encontrado.");
    }

    // GET api/<AlunoController>/nome
    [HttpGet("Byname")]
    public IActionResult GetByNome(string nome)
    {
      Aluno aluno = _context.Alunos.FirstOrDefault(item => item.Nome.Contains(nome));
      if (aluno != null)
        return Ok(aluno);
      return BadRequest("O Aluno não foi encontrado.");
    }

    // POST api/<AlunoController>
    [HttpPost]
    public IActionResult Post(Aluno aluno)
    {
      _context.Add(aluno);
      _context.SaveChanges();

      return Created("Aluno Criado", aluno);
    }

    // PUT api/<AlunoController>/5
    [HttpPut("{id}")]
    public IActionResult Put(int id, Aluno aluno)
    {
      var alu = _context.Alunos.AsNoTracking().FirstOrDefault(item => item.Id == id);
      if (alu == null) return BadRequest("Aluno nao encontrado");
      _context.Update(aluno);
      _context.SaveChanges();

      return Ok(aluno);
    }

    // Patch api/<AlunoController>/5
    [HttpPatch("{id}")]
    public IActionResult Patch(int id, Aluno aluno)
    {
      var alu = _context.Alunos.AsNoTracking().FirstOrDefault(item => item.Id == id);
      if (alu == null) return BadRequest("Aluno nao encontrado");
      _context.Update(aluno);
      _context.SaveChanges();

      return Ok(aluno);
    }

    // DELETE api/<AlunoController>/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      var aluno = _context.Alunos.FirstOrDefault(item => item.Id == id);
      if (aluno == null) return BadRequest("Aluno nao encontrado");
      _context.Alunos.Remove(aluno);
      _context.SaveChanges();
      return NoContent();
    }
  }
}
