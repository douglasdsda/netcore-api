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
    private readonly IRepository _repo;

    public ProfessorController(IRepository repo)
    {

      _repo = repo;
    }
    // GET: api/<ProfessorController>
    [HttpGet]
    public ActionResult Get()
    {
      var result = _repo.GetAllProfessores(true);
      return Ok(result);
    }

    // GET api/<ProfessorController>/5
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
      var professor = _repo.GetAProfessorById(id, false);
      if (professor != null)
        return Ok(professor);
      return BadRequest("O Professor não foi encontrado.");
    }

    // POST api/<ProfessorController>
    [HttpPost]
    public IActionResult Post(Professor professor)
    {
      _repo.Add(professor);
      if (_repo.SaveChanges()) return Created("Professor Criado", professor);

      return BadRequest("Professor não cadastrado.");
    }

    // PUT api/<ProfessorController>/5
    [HttpPut("{id}")]
    public IActionResult Put(int id, Professor professor)
    {

      var prof = _repo.GetAProfessorById(id);
      if (prof == null) return BadRequest("Professor nao encontrado");
      _repo.Update(professor);
      if (_repo.SaveChanges()) return Ok(professor);

      return BadRequest("Professor não cadastrado.");
    }

    // Patch api/<ProfessorController>/5
    [HttpPatch("{id}")]
    public IActionResult Patch(int id, Professor professor)
    {
      var prof = _repo.GetAProfessorById(id);
      if (prof == null) return BadRequest("Professor nao encontrado");
       _repo.Update(professor);
      if (_repo.SaveChanges()) return Ok(professor);

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
