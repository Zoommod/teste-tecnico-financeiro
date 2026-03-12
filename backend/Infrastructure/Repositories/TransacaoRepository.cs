using System;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TransacaoRepository : Repository<Transacao>, ITransacaoRepository
{
    public TransacaoRepository(AppDbContext context) : base(context)
    {
        
    }

    public async Task<Transacao?> ObterPorIdComRelacoesAsync(Guid id)
    {
        return await _dbSet
            .Include(t => t.Pessoa)
            .Include(t => t.Categoria)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<Transacao>> ObterTodosComRelacoesAsync()
    {
        return await _dbSet
            .Include(t => t.Pessoa)
            .Include(t => t.Categoria)
            .ToListAsync();
    }

    public async Task<IEnumerable<Transacao>> ObterPorPessoaIdAsync(Guid pessoaId)
    {
        return await _dbSet
            .Include(t => t.Categoria)
            .Where(t => t.PessoaId == pessoaId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Transacao>> ObterPorCategoriaIdAsync(Guid categoriaId)
    {
        return await _dbSet
            .Include(t => t.Pessoa)
            .Where(t => t.CategoriaId == categoriaId)
            .ToListAsync();
    }
}
