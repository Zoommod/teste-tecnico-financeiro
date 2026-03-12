using System;
using System.Reflection.Metadata;
using Application.DTOs.Transacao;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services;

public class TransacaoService : ITransacaoService
{
    private readonly ITransacaoRepository _transacaoRepository;
    private readonly IPessoaRepository _pessoaRepository;
    private readonly ICategoriaRepository _categoriaRepository;

    public TransacaoService(ITransacaoRepository transacaoRepository, IPessoaRepository pessoaRepository, ICategoriaRepository categoriaRepository)
    {
        _transacaoRepository = transacaoRepository;
        _pessoaRepository = pessoaRepository;
        _categoriaRepository = categoriaRepository;
    }

    public async Task<TransacaoDto> CriarAsync(CriarTransacaoDto dto)
    {
        var pessoa = await _pessoaRepository.ObterPorIdAsync(dto.PessoaId);
        if(pessoa is null)
            throw new EntityNotFoundException(nameof(Pessoa), dto.PessoaId);
        
        var categoria = await _categoriaRepository.ObterPorIdAsync(dto.CategoriaId);
        if(categoria is null)
            throw new EntityNotFoundException(nameof(Categoria), dto.CategoriaId);
        
        ValidarMenorDeIdade(pessoa, dto.Tipo);
        ValidarCompatibilidadeCategoria(categoria, dto.Tipo);

        var transacao = new Transacao(dto.Descricao, dto.Valor, dto.Tipo, dto.CategoriaId, dto.PessoaId);

        var transacaoCriada = await _transacaoRepository.AdicionarAsync(transacao);

        return await MapearParaDtoAsync(transacaoCriada);
    }

    public async Task<TransacaoDto?> ObterPorIdAsync(Guid id)
    {
        var transacao = await _transacaoRepository.ObterPorIdComRelacoesAsync(id);
        return transacao is null ? null : await MapearParaDtoAsync(transacao);
    }

    public async Task<IEnumerable<TransacaoDto>> ObterTodosAsync()
    {
        var transacoes = await _transacaoRepository.ObterTodosComRelacoesAsync();
        var dtos = new List<TransacaoDto>();
        foreach(var transacao in transacoes)
        {
            dtos.Add(await MapearParaDtoAsync(transacao));
        }

        return dtos;
    }

    private static void ValidarMenorDeIdade(Pessoa pessoa, TipoTransacao tipo)
    {
        if(pessoa.MenordeIdade() && tipo == TipoTransacao.Receita)
        {
            throw new BusinessRuleException("MenorDeIdadeDespesa", $"'{pessoa.Nome}' é menor de 18 anos e só pode ter transações do tipo Despesa.");
        }
    }

    private static void ValidarCompatibilidadeCategoria(Categoria categoria, TipoTransacao tipo)
    {
        if (!categoria.AceitaTipoTransacao(tipo))
        {
            var mensagem = categoria.Finalidade switch
            {
                Finalidade.Despesa => $"A categoria '{categoria.Descricao}' aceita apenas transações do tipo Despesa.",
                Finalidade.Receita => $"A categoria '{categoria.Descricao}' aceita apenas transações do tipo Receita.",
                _ => $"A categoria '{categoria.Descricao}' não aceita transações do tipo {tipo}."
                
            };

            throw new BusinessRuleException("CompatibilidadeCategoria", mensagem);
        }
    }

    private async Task<TransacaoDto> MapearParaDtoAsync(Transacao transacao)
    {
        if(transacao.Categoria is null)
        {
            var categoria = await _categoriaRepository.ObterPorIdAsync(transacao.CategoriaId);
            transacao = new Transacao(transacao.Descricao, transacao.Valor, transacao.Tipo, transacao.CategoriaId, transacao.PessoaId);
        }

        if(transacao.Pessoa is null)
        {
            var pessoa = await _pessoaRepository.ObterPorIdAsync(transacao.PessoaId);
        }

        return new TransacaoDto
        {
            Id = transacao.Id,
            Descricao = transacao.Descricao,
            Valor = transacao.Valor,
            Tipo = transacao.Tipo,
            CategoriaId = transacao.CategoriaId,
            CategoriaNome = transacao.Categoria?.Descricao ?? string.Empty,
            PessoaId = transacao.PessoaId,
            PessoaNome = transacao.Pessoa?.Nome ?? string.Empty
        };
    }
}
