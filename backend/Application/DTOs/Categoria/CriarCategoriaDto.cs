using Domain.Enums;

namespace Application.DTOs.Categoria;

public record class CriarCategoriaDto
{
    public string Descricao { get; init; } = string.Empty;
    public Finalidade Finalidade { get; init; }
}
