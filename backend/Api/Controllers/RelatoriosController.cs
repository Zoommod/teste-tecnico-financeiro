using Application.DTOs.Relatorios;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatoriosController : ControllerBase
    {
        private readonly IRelatorioService _relatorioService;

        public RelatoriosController(IRelatorioService relatorioService)
        {
            _relatorioService = relatorioService;
        }

        [HttpGet("totais-por-pessoa")]
        [ProducesResponseType(typeof(RelatorioTotaisPorPessoaDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<RelatorioTotaisPorPessoaDto>> ObterTotaisPorPessoa()
        {
            var relatorio = await _relatorioService.ObterTotaisPorPessoaAsync();
            return Ok(relatorio);
        }

        [HttpGet("totais-por-categoria")]
        [ProducesResponseType(typeof(RelatorioTotaisPorCategoriaDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<RelatorioTotaisPorCategoriaDto>> ObterTotaisPorCategoria()
        {
            var relatorio = await _relatorioService.ObterTotaisPorCategoriaAsync();

            return Ok(relatorio);
        }
    }
}
