using System;
using System.ComponentModel;
using Application.DTOs.Relatorios;
using Application.Interfaces;
using Domain.Enums;
using Domain.Interfaces;

namespace Application.Services;

public class RelatorioService : IRelatorioService
{
    private readonly IPessoaRepository _pessoaRepository;
    private readonly ICategoriaRepository _categoriaRepository;

    public RelatorioService(IPessoaRepository pessoaRepository, ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
        _pessoaRepository = pessoaRepository;
    }

    public async Task<RelatorioTotaisPorPessoaDto> ObterTotaisPorPessoaAsync()
    {
        var pessoas = await _pessoaRepository.ObterTodosComTransacoesAsync();

        var totaisPorPessoa = pessoas.Select(pessoa =>
        {
            var totalReceitas = pessoa.Transacoes.Where(t => t.Tipo == TipoTransacao.Receita).Sum(t => t.Valor);

            var totalDespesas = pessoa.Transacoes.Where(t => t.Tipo == TipoTransacao.Despesa).Sum(t => t.Valor);

            return new TotaisPorPessoaDto
            {
                PessoaId = pessoa.Id,
                NomePessoa = pessoa.Nome,
                Idade = pessoa.Idade,
                TotalReceitas = totalReceitas,
                TotalDespesas = totalDespesas,
                SaldoLiquido = totalReceitas - totalDespesas
            };
        }).ToList();

        var totalizador = new TotalizadorGeralDto
        {
            TotalReceitas = totaisPorPessoa.Sum(pessoas => pessoas.TotalReceitas),
            TotalDespesas = totaisPorPessoa.Sum(p => p.TotalDespesas),
            SaldoLiquido = totaisPorPessoa.Sum(p => p.SaldoLiquido)
        };

        return new RelatorioTotaisPorPessoaDto
        {
            Pessoas = totaisPorPessoa,
            Totalizador = totalizador
        };
    }

    public async Task<RelatorioTotaisPorCategoriaDto> ObterTotaisPorCategoriaAsync()
    {
        var categorias = await _categoriaRepository.ObterTodosComTransacoesAsync();

        var totaisPorCategoria = categorias.Select(categoria =>
        {
            var totalReceitas = categoria.Transacoes.Where(t => t.Tipo == TipoTransacao.Receita).Sum(t => t.Valor);
            var totalDespesas = categoria.Transacoes.Where(t => t.Tipo == TipoTransacao.Despesa).Sum(t => t.Valor);

            return new TotaisPorCategoriaDto
            {
                CategoriaId = categoria.Id,
                DescricaoCategoria = categoria.Descricao,
                Finalidade = categoria.Finalidade,
                TotalReceitas = totalReceitas,
                TotalDespesas = totalDespesas,
                SaldoLiquido = totalReceitas - totalDespesas
            };
        }).ToList();

        var totalizador = new TotalizadorGeralDto
        {
            TotalReceitas = totaisPorCategoria.Sum(c => c.TotalReceitas),
            TotalDespesas = totaisPorCategoria.Sum(c => c.TotalDespesas),
            SaldoLiquido = totaisPorCategoria.Sum(c => c.SaldoLiquido)
        };

        return new RelatorioTotaisPorCategoriaDto
        {
            Categorias = totaisPorCategoria,
            Totalizador = totalizador
        };
    }
}
