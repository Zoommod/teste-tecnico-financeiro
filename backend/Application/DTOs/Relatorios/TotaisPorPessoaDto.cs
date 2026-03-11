namespace Application.DTOs.Relatorios;

public record class TotaisPorPessoaDto
{
    public Guid PessoaId { get; init; }
    public string NomePessoa { get; init; } = string.Empty;
    public int Idade { get; init; }
    public decimal TotalReceitas { get; init; }
    public decimal TotalDespesas { get; init; }
    public decimal SaldoLiquido { get; init; }
}
