using System;
using Application.DTOs.Categoria;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services;

public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaService(ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }

    public async Task<CategoriaDto> CriarAsync(CriarCategoriaDto dto)
    {
        var categoria = new Categoria(dto.Descricao, dto.Finalidade);
        var categoriaCriada = await _categoriaRepository.AdicionarAsync(categoria);

        return MapearParaDto(categoriaCriada);
    }

    public async Task<CategoriaDto> AtualizarAsync(AtualizarCategoriaDto dto)
    {
        var categoria = await _categoriaRepository.ObterPorIdAsync(dto.Id);

        if(categoria is null)
            throw new EntityNotFoundException(nameof(Categoria), dto.Id);
        
        categoria.AtualizarDescricao(dto.Descricao);
        categoria.AtualizarFinalidade(dto.Finalidade);

        await _categoriaRepository.AtualizarAsync(categoria);

        return MapearParaDto(categoria);
    }

    public async Task DeletarAsync(Guid id)
    {
        var existe = await _categoriaRepository.ExisteAsync(id);

        if(!existe)
            throw new EntityNotFoundException(nameof(Categoria), id);
        
        await _categoriaRepository.DeletarAsync(id);
    }    

    public async Task<CategoriaDto?> ObterPorIdAsync(Guid id)
    {
        var categoria = await _categoriaRepository.ObterPorIdAsync(id);

        return categoria is null ? null : MapearParaDto(categoria);
    }

    public async Task<IEnumerable<CategoriaDto>> ObterTodosAsync()
    {
        var categorias = await _categoriaRepository.ObterTodosAsync();

        return categorias.Select(MapearParaDto);
    }

    private static CategoriaDto MapearParaDto(Categoria categoria)
    {
        return new CategoriaDto
        {
            Id = categoria.Id,
            Descricao = categoria.Descricao,
            Finalidade = categoria.Finalidade
        };
    }
}
