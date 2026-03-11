using Domain.Enums;

namespace Application.DTOs.Categoria;

public record class AtualizarCategoriaDto
{
    public Guid Id { get; init; }
    public string Descricao { get; init; } = string.Empty;
    public Finalidade Finalidade { get; init; }
}
