using System;
using Domain.Entities;

namespace Domain.Interfaces;

public interface IPessoaRepository : IRepository<Pessoa>
{
    Task<Pessoa?> ObterPorIdComTransacoesAsync(Guid id);
    Task<IEnumerable<Pessoa>> ObterTodosComTransacoesAsync();
}
