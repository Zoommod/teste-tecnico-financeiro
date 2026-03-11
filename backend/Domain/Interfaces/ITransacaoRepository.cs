using System;
using Domain.Entities;

namespace Domain.Interfaces;

public interface ITransacaoRepository : IRepository<Transacao>
{
    Task<Transacao?> ObterPorIdComRelacoesAsync(Guid id);
    Task<IEnumerable<Transacao>> ObterTodosComRelacoesAsync();
    Task<IEnumerable<Transacao>> ObterPorPessoaIdAsync(Guid pessoaId);
    Task<IEnumerable<Transacao>> ObterPorCategoriaIdAsync(Guid categoriaId);
}
