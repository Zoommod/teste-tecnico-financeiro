using System;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PessoaRepository : Repository<Pessoa>, IPessoaRepository
{
    public PessoaRepository(AppDbContext context) : base(context)
    {
        
    }

    public async Task<Pessoa?> ObterPorIdComTransacoesAsync(Guid id)
    {
        return await _dbSet
            .Include(p => p.Transacoes)
            .ThenInclude(t => t.Categoria)
            .FirstOrDefaultAsync(p => p.Id == id); 
    }

    public async Task<IEnumerable<Pessoa>> ObterTodosComTransacoesAsync()
    {
        return await _dbSet
            .Include(p => p.Transacoes)
            .ThenInclude(t => t.Categoria)
            .ToListAsync();
    }
}
