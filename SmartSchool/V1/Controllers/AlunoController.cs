﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.Data;
using SmartSchool.V1.Dtos;
using SmartSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SmartSchool.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartSchool.V1.Controllers
{
  /// <summary>
  /// Versão 1 do controlador de alunos
  /// </summary>
  [ApiController]
  [ApiVersion("1.0")]
  [Route("api/v{version:apiVersion}/[controller]")]
  public class AlunoController : ControllerBase
  {
    public readonly IRepository _repo;
    private readonly IMapper _mapper;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="repo"></param>
    /// <param name="mapper"></param>
    public AlunoController(IRepository repo, IMapper mapper)
    {
      _mapper = mapper;
      _repo = repo;
    }


   /// <summary>
   /// Método responsavel para retornar todos alunos
   /// </summary>
   /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery]PageParams pageParams)
    {
 
      var alunos = await _repo.GetAllAlunosAsync(pageParams, true );
      var alunosResult = _mapper.Map<IEnumerable<AlunoDto>>(alunos);
      Response.AddPagination(alunos.CurrentPage, alunos.PageSize, alunos.TotalCount, alunos.TotalPages);
      return Ok(alunosResult);

    }
    /// <summary>
    /// Método responsavel para retornar apenas um aluno por meio do codigo ID
    /// </summary>
    /// <returns></returns>
    // GET api/<AlunoController>/5
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
      var aluno = _repo.GetAlunoById(id, false);
      if (aluno != null)
      {
        var alunoDto = _mapper.Map<AlunoRegistrarDto>(aluno);
        return Ok(alunoDto);
      }
      return BadRequest("O Aluno não foi encontrado.");
    }
    
    /// <summary>
    /// Método responsavel aluno by disciplina
    /// </summary>
    /// <returns></returns>
    // GET api/<Aluno>/5
    [HttpGet("ByDisciplina/{id}")]
    public async Task<IActionResult> GetByDisciplina(int id)
    {
      var result = await _repo.GetAllAlunosByDisciplinaIdAsync(id, false);
      if (result != null)
      {
 
        return Ok(result);
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
    public IActionResult Patch(int id, AlunoPatchDto model)
    {
      var aluno = _repo.GetAlunoById(id);
      if (aluno == null) return BadRequest("Aluno nao encontrado");

      _mapper.Map(model, aluno);
      _repo.Update(aluno);
      if (_repo.SaveChanges()) return Ok(_mapper.Map<AlunoPatchDto>(aluno));

      return BadRequest("Aluno não atualizado.");
    }
   
    [HttpPatch("{id}/trocarEstado")]
    public IActionResult trocarEstado(int id, TrocarEstadoDto estado)
    {
      var aluno = _repo.GetAlunoById(id);
      if (aluno == null) return BadRequest("Aluno nao encontrado");

      aluno.Ativo = estado.Estado;
      _repo.Update(aluno);
      if (_repo.SaveChanges()){
        var msn = aluno.Ativo ? "ativado":"desativado";
        return Ok(new { message = $"Aluno {msn} com sucesso!"});
      } 

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
