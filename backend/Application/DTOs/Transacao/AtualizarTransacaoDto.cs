using Domain.Enums;

namespace Application.DTOs.Transacao;

public record class AtualizarTransacaoDto
{
    public Guid Id { get; init; }
    public string Descricao { get; init; } = string.Empty;
    public decimal Valor { get; init; }
    public TipoTransacao Tipo { get; init; }
    public Guid CategoriaId { get; init; }

}
