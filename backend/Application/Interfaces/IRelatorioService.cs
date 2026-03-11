using System;
using Application.DTOs.Relatorios;

namespace Application.Interfaces;

public interface IRelatorioService
{
    Task<RelatorioTotaisPorPessoaDto> ObterTotaisPorPessoaAsync();
    Task<RelatorioTotaisPorCategoriaDto> ObterTotaisPorCategoriaAsync();
}
