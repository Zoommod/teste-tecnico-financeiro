using System;
using System.Reflection.Metadata;
using Domain.Enums;

namespace Domain.Entities;

public class Categoria : BaseEntity
{
    public string Descricao { get; private set; }
    public Finalidade Finalidade { get; private set; }
    public ICollection<Transacao> Transacoes { get; private set; }
    
    private Categoria() : base()
    {
        Descricao = string.Empty;
        Transacoes = new List<Transacao>();
    }

    public Categoria(string descricao, Finalidade finalidade) : base()
    {
        ValidarDescricao(descricao);

        Descricao = descricao;
        Finalidade = finalidade;
        Transacoes = new List<Transacao>();
    }

    public void AtualizarFinalidade(Finalidade finalidade)
    {
        Finalidade = finalidade;
    }

    public bool AceitaTipoTransacao(TipoTransacao tipo)
    {
        return Finalidade switch
        {
            Finalidade.Ambas => true,
            Finalidade.Despesa => tipo == TipoTransacao.Despesa,
            Finalidade.Receita => tipo == TipoTransacao.Receita,
            _ => false
        };
    }

    private static void ValidarDescricao(string descricao)
    {
        if(string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("Descrição não pode ser vazia.", nameof(descricao));
        
        if(descricao.Length > 400)
            throw new ArgumentException("Descrição não pode ter mais de 400 caracteres", nameof(descricao));
    }
}
