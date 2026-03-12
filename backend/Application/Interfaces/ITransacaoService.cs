using System;
using Application.DTOs.Transacao;
using Domain.Entities;

namespace Application.Interfaces;

public interface ITransacaoService
{
    Task<TransacaoDto> CriarAsync(CriarTransacaoDto dto);
    Task<TransacaoDto?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<TransacaoDto>> ObterTodosAsync();
}
