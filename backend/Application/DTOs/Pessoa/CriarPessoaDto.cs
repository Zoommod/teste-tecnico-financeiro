namespace Application.DTOs.Pessoa;

public record class CriarPessoaDto
{
    public string Nome { get; init; } = string.Empty;
    public int Idade { get; init; }
}
