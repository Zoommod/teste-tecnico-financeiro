using System;
using Domain.Enums;

namespace Domain.Entities;

public class Pessoa : BaseEntity
{
    public string Nome { get; private set; }
    public int Idade { get; private set; }
    public ICollection<Transacao> Transacoes { get; private set; }

    private Pessoa() : base()
    {
        Nome = string.Empty;
        Transacoes = new List<Transacao>();
    }

    public Pessoa(string nome, int idade) : base()
    {
        ValidarNome(nome);
        ValidarIdade(idade);

        Nome = nome;
        Idade = idade;
        Transacoes = new List<Transacao>();
    }

    public void AtualizarNome(string nome)
    {
        ValidarNome(nome);
        Nome = nome;
    }

    public void AtualizarIdade(int idade)
    {
        ValidarIdade(idade);
        Idade = idade;
    }

    public bool MenordeIdade() => Idade < 18;

    private static void ValidarNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome não pode ser vazio.", nameof(nome));
        
        if (nome.Length > 200)
            throw new ArgumentException("Nome não pode ter mais de 200 caracteres.", nameof(nome));
    }

    private static void ValidarIdade(int idade)
    {
        if (idade < 0)
            throw new ArgumentException("Idade não pode ser negativa.", nameof(idade));
        
        if (idade > 150)
            throw new ArgumentException("Idade inválida", nameof(idade));
    }

}
