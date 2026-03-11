using System;
using Application.DTOs.Pessoa;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services;

public class PessoaService : IPessoaService
{
    private readonly IPessoaRepository _pessoaRepository;

    public PessoaService(IPessoaRepository pessoaRepository)
    {
        _pessoaRepository = pessoaRepository;
    }

    public async Task<PessoaDto> CriarAsync(CriarPessoaDto dto)
    {
        var pessoa = new Pessoa(dto.Nome, dto.Idade);
        var pessoaCriada = await _pessoaRepository.AdicionarAsync(pessoa);
        
        return MapearParaDto(pessoaCriada);
    }

    public async Task<PessoaDto> AtualizarAsync(AtualizarPessoaDto dto)
    {
        var pessoa = await _pessoaRepository.ObterPorIdAsync(dto.Id);

        if(pessoa is null)
            throw new EntityNotFoundException(nameof(Pessoa), dto.Id);
        
        pessoa.AtualizarNome(dto.Nome);
        pessoa.AtualizarIdade(dto.Idade);

        await _pessoaRepository.AtualizarAsync(pessoa);

        return MapearParaDto(pessoa);
    }

    public async Task DeletarAsync(Guid id)
    {
        var existe = await _pessoaRepository.ExisteAsync(id);

        if(!existe)
            throw new EntityNotFoundException(nameof(Pessoa), id);
        
        await _pessoaRepository.DeletarAsync(id);
    }

    public async Task<PessoaDto?> ObterPorIdAsync(Guid id)
    {
        var pessoa = await _pessoaRepository.ObterPorIdAsync(id);
        return pessoa is null ? null : MapearParaDto(pessoa);
    }

    public async Task<IEnumerable<PessoaDto>> ObterTodosAsync()
    {
        var pessoas = await _pessoaRepository.ObterTodosAsync();

        return pessoas.Select(MapearParaDto);
    }

    private static PessoaDto MapearParaDto(Pessoa pessoa)
    {
        return new PessoaDto
        {
            Id = pessoa.Id,
            Nome = pessoa.Nome,
            Idade = pessoa.Idade
        };
    }
}
