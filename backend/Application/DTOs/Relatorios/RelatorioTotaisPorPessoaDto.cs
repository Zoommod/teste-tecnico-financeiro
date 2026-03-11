namespace Application.DTOs.Relatorios;

public record class RelatorioTotaisPorPessoaDto
{
    public IEnumerable<TotaisPorPessoaDto> Pessoas { get; init; } = new List<TotaisPorPessoaDto>();
    public TotalizadorGeralDto Totalizador { get; init; } = new TotalizadorGeralDto();
}
