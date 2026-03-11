using System;
using Domain.Entities;

namespace Domain.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<T?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<T>> ObterTodosAsync();
    Task<T> AdicionarAsync(T entity);
    Task AtualizarAsync(T entity);
    Task DeletarAsync(Guid id);
    Task<bool> ExisteAsync(Guid id);
}
