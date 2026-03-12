using System;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext context) : base(context)
    {
        
    }

    public async Task<Categoria?> ObterPorIdComTransacoesAsync(Guid id)
    {
        return await _dbSet
            .Include(c => c.Transacoes)
            .ThenInclude(t => t.Pessoa)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Categoria>> ObterTodosComTransacoesAsync()
    {
        return await _dbSet
            .Include(c => c.Transacoes)
            .ThenInclude(t => t.Pessoa)
            .ToListAsync();
    }
}
