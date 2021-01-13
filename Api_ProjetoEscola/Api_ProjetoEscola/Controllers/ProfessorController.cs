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
    public class ProfessorController : ControllerBase
    {

        private readonly IRepository _repository;
        public ProfessorController(IRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _repository.GetAllProfessoresAsync(true);
                return Ok(result);
            }
            catch (SystemException)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Bando de Dados Falhou");
            }


        }

        [HttpGet("{ProfessorId}")]
        public async Task<IActionResult> GetAsync(int ProfessorId)
        {
            try
            {
                var result = await _repository.GetProfessorAsyncById(ProfessorId, true);
                return Ok(result);
            }
            catch (SystemException)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Bando de Dados Falhou");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Professor model)
        {
            try
            {
                _repository.Add(model);
                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/Professor/{model.Id}", model);
                }

            }
            catch (SystemException)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Bando de Dados Falhou");
            }
            return BadRequest();
        }


        [HttpPut("{ProfessorId}")]
        public async Task<IActionResult> PutAsync(int ProfessorId, Professor model)
        {
            try
            {
                var professor = await _repository.GetProfessorAsyncById(ProfessorId, true);
                    if(professor == null) return NotFound();

                _repository.Update(model);

                if (await _repository.SaveChangesAsync())
                {
                    professor = await _repository.GetProfessorAsyncById(ProfessorId, true);
                    return Created($"api/Professor/{model.Id}", model);
                }


            }
            catch (SystemException)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Bando de Dados Falhou");
            }
            return BadRequest();
        }

        [HttpDelete("{ProfessorId}")]
        public async Task<IActionResult> DeleteAsync(int ProfessorId , int AlunoId)
        {
            try
            {
                
                var professor = await _repository.GetProfessorAsyncById(ProfessorId, true);
                
                
                if (professor == null) return NotFound();
                if(professor.Alunos.Count > 0) 
                {
                    return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
                }
                else
                {
                    _repository.Delete(professor);

                    if (await _repository.SaveChangesAsync())
                    {
                        return Ok();
                    }
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
