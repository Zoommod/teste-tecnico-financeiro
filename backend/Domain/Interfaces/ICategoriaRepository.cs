using System;
using Domain.Entities;

namespace Domain.Interfaces;

public interface ICategoriaRepository : IRepository<Categoria>
{
    Task<Categoria?> ObterPorIdComTransacoesAsync(Guid id);
    Task<IEnumerable<Categoria>> ObterTodosComTransacoesAsync();
}
