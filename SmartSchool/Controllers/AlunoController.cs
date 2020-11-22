using Microsoft.AspNetCore.Mvc;
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

        public List<Aluno> Alunos = new List<Aluno>() {
            new Aluno(){
                Id = 1,
                Nome = "Douglas",
                Telefone = "123456789"
            },
             new Aluno(){
                Id = 2,
                Nome = "Marta",
                Telefone = "232321"
            },
              new Aluno(){
                Id = 3,
                Nome = "Julia",
                Telefone = "32141232"
            }
        };

        public AlunoController() { }

        // GET: api/<AlunoController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Alunos);
        }

        // GET api/<AlunoController>/5
        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {   
             Aluno aluno = Alunos.Find(item => item.Id == id);
             if(aluno != null)
                 return Ok(aluno);
              return BadRequest("O Aluno não foi encontrado.");
        }
      
        // GET api/<AlunoController>/nome
        [HttpGet("{nome}")]
        public IActionResult GetByNome(string nome)
        {   
             Aluno aluno = Alunos.Find(item => item.Nome.Contains(nome));
             if(aluno != null)
                 return Ok(aluno);
              return BadRequest("O Aluno não foi encontrado.");
        }

        // POST api/<AlunoController>
        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            return Ok(aluno);
        }

        // PUT api/<AlunoController>/5
        [HttpPut("{id}")]
        public void Put(int id, Aluno aluno)
        {
              var index = Alunos.FindIndex(item => item.Id == id);
              Alunos[index] = aluno;  
        }

        // Patch api/<AlunoController>/5
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            return Ok("teste");
        }

        // DELETE api/<AlunoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var index = Alunos.FindIndex(item => item.Id == id);
            Alunos.RemoveAt(index);
        }
    }
}
