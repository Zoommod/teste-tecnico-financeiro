using System;
using Application.DTOs.Categoria;

namespace Application.Interfaces;

public interface ICategoriaService
{
    Task<CategoriaDto> CriarAsync(CriarCategoriaDto dto);
    Task<CategoriaDto> AtualizarAsync(AtualizarCategoriaDto dto);
    Task DeletarAsync(Guid id);
    Task<CategoriaDto?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<CategoriaDto>> ObterTodosAsync();
}
