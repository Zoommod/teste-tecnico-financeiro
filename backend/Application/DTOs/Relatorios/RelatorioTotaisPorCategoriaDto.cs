namespace Application.DTOs.Relatorios;

public record class RelatorioTotaisPorCategoriaDto
{
    public IEnumerable<TotaisPorCategoriaDto> Categorias { get; init; } = new List<TotaisPorCategoriaDto>();
    public TotalizadorGeralDto Totalizador { get; init; } = new TotalizadorGeralDto();
}
