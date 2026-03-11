using Domain.Enums;

namespace Application.DTOs.Relatorios;

public record class TotaisPorCategoriaDto
{
    public Guid CategoriaId { get; init; }
    public string DescricaoCategoria { get; init; } = string.Empty;
    public Finalidade Finalidade { get; init; }
    public decimal TotalReceitas { get; init; }
    public decimal TotalDespesas { get; init; }
    public decimal SaldoLiquido { get; init; }
}
