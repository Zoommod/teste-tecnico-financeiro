using System;
using Domain.Enums;

namespace Domain.Entities;

public class Transacao : BaseEntity
{
    public string Descricao { get; private set; }
    public decimal Valor { get; private set; }
    public TipoTransacao Tipo { get; private set; }
    public Guid CategoriaId { get; private set; }
    public Guid PessoaId { get; private set; }

    public Categoria Categoria { get; private set; } = null!;
    public Pessoa Pessoa { get; private set; } = null!;

    private Transacao() : base()
    {
        Descricao = string.Empty;
    }

    public Transacao(
        string descricao,
        decimal valor,
        TipoTransacao tipo,
        Guid categoriaId,
        Guid pessoaId
    ) : base()
    {
        ValidarDescricao(descricao);
        ValidarValor(valor);

        Descricao = descricao;
        Valor = valor;
        Tipo = tipo;
        CategoriaId = categoriaId;
        PessoaId = pessoaId;
    }

    private static void ValidarDescricao(string descricao)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("Descrição não pode ser vazia.", nameof(descricao));
        
        if(descricao.Length > 400)
            throw new ArgumentException("Descrição não pode ter mais de 400 caracteres.", nameof(descricao));
    }

    private static void ValidarValor(decimal valor)
    {
        if(valor <= 0)
            throw new ArgumentException("Valor deve ser maior que zero.", nameof(valor));
    }

}
