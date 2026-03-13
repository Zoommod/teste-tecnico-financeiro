import { useEffect, useState } from 'react'
import { pessoaService } from '../services/pessoaService'
import type { ApiError, Pessoa } from '../types/index'

export function PessoasPage() {
  const [pessoas, setPessoas] = useState<Pessoa[]>([])
  const [nome, setNome] = useState('')
  const [idade, setIdade] = useState('')
  const [erro, setErro] = useState('')
  const [editandoId, setEditandoId] = useState<string | null>(null)

  async function carregar() {
    try {
      setErro('')
      setPessoas(await pessoaService.obterTodos())
    } catch (e) {
      setErro((e as ApiError).message ?? 'Erro ao carregar pessoas')
    }
  }

  useEffect(() => {
    void carregar()
  }, [])

  async function salvar(e: React.FormEvent) {
    e.preventDefault()
    try {
      setErro('')

      const idadeNumero = Number(idade)
      if (!idade || Number.isNaN(idadeNumero)) {
        setErro('Informe uma idade válida.')
        return
      }

      if (editandoId) {
        await pessoaService.atualizar({ id: editandoId, nome, idade: idadeNumero })
      } else {
        await pessoaService.criar({ nome, idade: idadeNumero })
      }
      setNome('')
      setIdade('')
      setEditandoId(null)
      await carregar()
    } catch (e) {
      setErro((e as ApiError).message ?? 'Erro ao salvar pessoa')
    }
  }

  async function excluir(id: string) {
    if (!confirm('Confirma exclusão da pessoa e transações?')) return
    try {
      await pessoaService.deletar(id)
      await carregar()
    } catch (e) {
      setErro((e as ApiError).message ?? 'Erro ao excluir pessoa')
    }
  }

  return (
    <section className="card">
      <h1 className="page-title">Pessoas</h1>

      <form onSubmit={salvar}>
        <input placeholder="Nome" value={nome} onChange={(e) => setNome(e.target.value)} maxLength={200} required />
        <input
          type="number"
          min={0}
          placeholder="Idade"
          value={idade}
          onChange={(e) => setIdade(e.target.value)}
          required
        />
        <button type="submit">{editandoId ? 'Atualizar' : 'Criar'}</button>
        {editandoId && (
          <button type="button" onClick={() => { setEditandoId(null); setNome(''); setIdade('') }}>
            Cancelar
          </button>
        )}
      </form>

      {erro && <p className="error">{erro}</p>}

      <table className="table">
        <thead>
          <tr><th>Nome</th><th>Idade</th><th>Ações</th></tr>
        </thead>
        <tbody>
          {pessoas.map((p) => (
            <tr key={p.id}>
              <td>{p.nome}</td>
              <td>{p.idade}</td>
              <td>
                <div className="acoes-botoes">
    <button onClick={() => { setEditandoId(p.id); setNome(p.nome); setIdade(String(p.idade)) }}>
      Editar
    </button>
    <button onClick={() => void excluir(p.id)}>
      Excluir
    </button>
  </div>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </section>
  )
}