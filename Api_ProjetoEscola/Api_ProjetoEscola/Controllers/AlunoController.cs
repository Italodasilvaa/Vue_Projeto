using Api_ProjetoEscola.Data;
using Api_ProjetoEscola.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Api_ProjetoEscola.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        public  IRepository _repository { get; }
        public AlunoController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _repository.GetAllAlunosAsync(true);
                return Ok(result);
            }
            catch (SystemException)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Bando de Dados Falhou");
            }

        }

        [HttpGet("{AlunoId}")]
        public async Task<IActionResult> GetByAlunoId(int AlunoId)
        {
            try
            {
                var result = await _repository.GetAlunoAsyncById(AlunoId, true);
                return Ok(result);
            }
            catch (SystemException)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Bando de Dados Falhou");
            };
        }

        [HttpGet("ByProfessor/{ProfessorId}")]
        public async Task<IActionResult> GetByProfessorId(int ProfessorId)
        {
            try
            {
                var result = await _repository.GetAlunosAsyncProfessorId(ProfessorId, true);
                return Ok(result);
            }
            catch (SystemException)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Bando de Dados Falhou");
            };
        }

        [HttpPost]
        public async Task<IActionResult> Post(Aluno model)
        {
            try
            {
                _repository.Add(model);
                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/aluno/{model.AlunoId}", model);
                }

            }
            catch (SystemException)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Bando de Dados Falhou");
            }
            return BadRequest();
        }



        [HttpPut("{AlunoId}")]
        public async Task<IActionResult> Put(int AlunoId, Aluno model)
        {
            try
            {
                var aluno = await _repository.GetAlunoAsyncById(AlunoId, false);
                if (aluno == null) return NotFound();

                _repository.Update(model);

                if (await _repository.SaveChangesAsync())
                {
                    aluno = await _repository.GetAlunoAsyncById(AlunoId, true);
                    return Created($"api/aluno/{model.AlunoId}", aluno);
                }

            }
            catch (SystemException)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Bando de Dados Falhou");
            }
            return BadRequest();
        }

        [HttpDelete("{AlunoId}")]
        public async Task<IActionResult> Delete(int AlunoId)
        {
            try
            {
                var aluno = await _repository.GetAlunoAsyncById(AlunoId, false);
                if (aluno == null) return NotFound();

                _repository.Delete(aluno);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok();
                    }
            }
            catch (SystemException)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Bando de Dados Falhou");
            }
            return BadRequest();
        }


    }
}
