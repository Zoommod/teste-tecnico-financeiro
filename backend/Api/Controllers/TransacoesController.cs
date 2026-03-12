using Application.DTOs.Transacao;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransacoesController : ControllerBase
    {
        private readonly ITransacaoService _transacaoService;

        public TransacoesController(ITransacaoService transacaoService)
        {
            _transacaoService = transacaoService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TransacaoDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TransacaoDto>>> ObterTodos()
        {
            var transacoes = await _transacaoService.ObterTodosAsync();

            return Ok(transacoes);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TransacaoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TransacaoDto>> ObterPorId(Guid id)
        {
            var transacao = await _transacaoService.ObterPorIdAsync(id);

            if(transacao is null)
                return NotFound();
            
            return Ok(transacao);
        }

        [HttpPost]
        [ProducesResponseType(typeof(TransacaoDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TransacaoDto>> Criar ([FromBody] CriarTransacaoDto dto)
        {
            var transacao = await _transacaoService.CriarAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = transacao.Id }, transacao);
        }
    }
}
