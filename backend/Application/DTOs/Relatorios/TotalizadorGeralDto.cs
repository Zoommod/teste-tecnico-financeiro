namespace Application.DTOs.Relatorios;

public record class TotalizadorGeralDto
{
    public decimal TotalReceitas { get; init; }
    public decimal TotalDespesas { get; init; }
    public decimal SaldoLiquido { get; init; }
}
