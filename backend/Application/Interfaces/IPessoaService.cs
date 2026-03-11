using System;
using Application.DTOs.Pessoa;

namespace Application.Interfaces;

public interface IPessoaService
{
    Task<PessoaDto> CriarAsync(CriarPessoaDto dto);
    Task<PessoaDto> AtualizarAsync(AtualizarPessoaDto dto);
    Task DeletarAsync(Guid id);
    Task<PessoaDto?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<PessoaDto>> ObterTodosAsync();
}
