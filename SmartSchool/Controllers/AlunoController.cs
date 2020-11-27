﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.Data;
using SmartSchool.Dtos;
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
    private readonly IMapper _mapper;

    public AlunoController(IRepository repo, IMapper mapper)
    {
      _mapper = mapper;
      _repo = repo;
    }


    // GET: api/<AlunoController>
    [HttpGet]
    public IActionResult Get()
    {
      var alunos = _repo.GetAllAlunos(true);

      return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alunos));

    }

    // GET api/<AlunoController>/5
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
      var aluno = _repo.GetAlunoById(id, false);
      if (aluno != null)
      {
        var alunoDto = _mapper.Map<AlunoDto>(aluno);
        return Ok(alunoDto);
      }
      return BadRequest("O Aluno não foi encontrado.");
    }


    // POST api/<AlunoController>
    [HttpPost]
    public IActionResult Post(AlunoRegistrarDto model)
    {
      var aluno = _mapper.Map<Aluno>(model);
      _repo.Add(aluno);
      if (_repo.SaveChanges()) return Created($"api/aluno/{aluno.Id}", _mapper.Map<AlunoDto>(aluno));

      return BadRequest("Aluno não cadastrado.");
    }

    // PUT api/<AlunoController>/5
    [HttpPut("{id}")]
    public IActionResult Put(int id, AlunoRegistrarDto model)
    {
      var aluno = _repo.GetAlunoById(id);
      if (aluno == null) return BadRequest("Aluno nao encontrado");

      _mapper.Map(model, aluno);
      _repo.Update(aluno);
      if (_repo.SaveChanges()) return Ok(_mapper.Map<AlunoDto>(aluno));

      return BadRequest("Aluno não atualizado.");
    }

    // Patch api/<AlunoController>/5
    [HttpPatch("{id}")]
    public IActionResult Patch(int id, AlunoRegistrarDto model)
    {
      var aluno = _repo.GetAlunoById(id);
      if (aluno == null) return BadRequest("Aluno nao encontrado");

      _mapper.Map(model, aluno);
      _repo.Update(aluno);
      if (_repo.SaveChanges()) return Ok(_mapper.Map<AlunoDto>(aluno));

      return BadRequest("Aluno não atualizado.");
    }

    // DELETE api/<AlunoController>/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      var aluno = _repo.GetAlunoById(id);
      if (aluno == null) return BadRequest("Aluno nao encontrado");

      _repo.Remove(aluno);
      if (_repo.SaveChanges()) return NoContent();

      return BadRequest("Aluno não deletado.");
    }
  }
}
