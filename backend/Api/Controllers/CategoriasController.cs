using Application.DTOs.Categoria;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriasController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CategoriaDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CategoriaDto>>> ObterTodos()
        {
            var categorias = await _categoriaService.ObterTodosAsync();
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategoriaDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoriaDto>> ObterPorId(Guid id)
        {
            var categoria = await _categoriaService.ObterPorIdAsync(id);
            if(categoria is null)
                return NotFound();
            
            return Ok(categoria);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CategoriaDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoriaDto>> Criar([FromBody] CriarCategoriaDto dto)
        {
            var categoria = await _categoriaService.CriarAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = categoria.Id }, categoria);
        }

    }
}
