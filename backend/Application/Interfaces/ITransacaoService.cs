using System;
using Application.DTOs.Transacao;
using Domain.Entities;

namespace Application.Interfaces;

public interface ITransacaoService
{
    Task<TransacaoDto> CriarAsync(CriarTransacaoDto dto);
    Task<TransacaoDto> AtualizarAsync(AtualizarTransacaoDto dto);
    Task DeletarAsync(Guid id);
    Task<TransacaoDto?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<TransacaoDto>> ObterTodosAsync();
}
