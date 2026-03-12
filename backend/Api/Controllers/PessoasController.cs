using Application.DTOs.Pessoa;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoasController : ControllerBase
    {
        private readonly IPessoaService _pessoaService;

        public PessoasController(IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PessoaDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PessoaDto>>> ObterTodos()
        {
            var pessoas = await _pessoaService.ObterTodosAsync();
            return Ok(pessoas);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PessoaDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PessoaDto>> ObterPorId(Guid id)
        {
            var pessoa = await _pessoaService.ObterPorIdAsync(id);
            if(pessoa is null)
                return NotFound();
            
            return Ok(pessoa);
        }

        [HttpPost]
        [ProducesResponseType(typeof(PessoaDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PessoaDto>> Criar([FromBody] CriarPessoaDto dto)
        {
            var pessoa = await _pessoaService.CriarAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = pessoa.Id }, pessoa);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PessoaDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PessoaDto>> Atualizar(Guid id, [FromBody] AtualizarPessoaDto dto)
        {
            if(id != dto.Id)
                return BadRequest("O ID da URL não corresponde ao ID do corpo da requisição.");

            var pessoa = await _pessoaService.AtualizarAsync(dto);
            return Ok(pessoa);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Deletar(Guid id)
        {
            await _pessoaService.DeletarAsync(id);
            return NoContent();
        }
    }
}
