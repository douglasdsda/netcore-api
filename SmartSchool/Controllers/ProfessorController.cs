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
  public class ProfessorController : ControllerBase
  {
    private readonly SmartContext _context;

    public ProfessorController(SmartContext context)
    {
      _context = context;
    }
    // GET: api/<ProfessorController>
    [HttpGet]
    public ActionResult Get()
    {
          return Ok(_context.Professores);
    }

    // GET api/<ProfessorController>/5
    [HttpGet("byId/{id}")]
    public IActionResult GetById(int id)
    {
      Professor professor = _context.Professores.FirstOrDefault(item => item.Id == id);
      if (professor != null)
        return Ok(professor);
      return BadRequest("O Aluno não foi encontrado.");
    }

    // GET api/<ProfessorController>/nome
    [HttpGet("Byname")]
    public IActionResult GetByNome(string nome)
    {
      Professor professor = _context.Professores.FirstOrDefault(item => item.Nome.Contains(nome));
      if (professor != null)
        return Ok(professor);
      return BadRequest("O Professor não foi encontrado.");
    }

    // POST api/<ProfessorController>
    [HttpPost]
    public IActionResult Post(Professor professor)
    {
      _context.Add(professor);
      _context.SaveChanges();

      return Created("Professor Criado", professor);
    }

    // PUT api/<ProfessorController>/5
    [HttpPut("{id}")]
    public IActionResult Put(int id, Professor professor)
    {
      var prof = _context.Professores.AsNoTracking().FirstOrDefault(item => item.Id == id);
      if (prof == null) return BadRequest("Aluno nao encontrado");
      _context.Update(professor);
      _context.SaveChanges();

      return Ok(professor);
    }

    // Patch api/<ProfessorController>/5
    [HttpPatch("{id}")]
    public IActionResult Patch(int id, Professor professor)
    {
     var prof = _context.Professores.AsNoTracking().FirstOrDefault(item => item.Id == id);
      if (prof == null) return BadRequest("Aluno nao encontrado");
      _context.Update(professor);
      _context.SaveChanges();

      return Ok(professor);
    }

    // DELETE api/<ProfessorController>/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      var professor = _context.Professores.FirstOrDefault(item => item.Id == id);
      if (professor == null) return BadRequest("Professor nao encontrado");
      _context.Professores.Remove(professor);
      _context.SaveChanges();
      return NoContent();
    }
  }
}
