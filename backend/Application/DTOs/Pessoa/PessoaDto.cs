namespace Application.DTOs.Pessoa;

public record class PessoaDto
{
    public Guid Id { get; init; }
    public string Nome { get; init; } = string.Empty;
    public int Idade { get; init; }
}
