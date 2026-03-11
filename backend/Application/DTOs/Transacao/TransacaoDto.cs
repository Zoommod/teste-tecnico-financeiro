using Domain.Enums;

namespace Application.DTOs.Transacao;

public record class TransacaoDto
{
    public Guid Id { get; init; }
    public string Descricao { get; init; } = string.Empty;
    public decimal Valor { get; init; }
    public TipoTransacao Tipo { get; init; }
    public Guid CategoriaId { get; init; }
    public string CategoriaNome { get; init; } = string.Empty;
    public Guid PessoaId { get; init; }
    public string PessoaNome { get; init; } = string.Empty;
}
