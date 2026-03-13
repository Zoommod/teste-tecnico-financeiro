import { useEffect, useState } from 'react'
import { categoriaService } from '../services/categoriaService'
import { pessoaService } from '../services/pessoaService'
import { transacaoService } from '../services/transacaoService'
import type { ApiError, Categoria, Pessoa, Transacao } from '../types/index'
import { TipoTransacao } from '../types/index'
import { formatarMoeda, formatarTipo } from '../utils/formatters'

export function TransacoesPage() {
  const [transacoes, setTransacoes] = useState<Transacao[]>([])
  const [pessoas, setPessoas] = useState<Pessoa[]>([])
  const [categorias, setCategorias] = useState<Categoria[]>([])

  const [descricao, setDescricao] = useState('')
  const [valor, setValor] = useState('')
  const [tipo, setTipo] = useState('')
  const [pessoaId, setPessoaId] = useState('')
  const [categoriaId, setCategoriaId] = useState('')

  const [erro, setErro] = useState('')

  async function carregar() {
    try {
      setErro('')
      const [trs, pes, cats] = await Promise.all([
        transacaoService.obterTodos(),
        pessoaService.obterTodos(),
        categoriaService.obterTodos(),
      ])

      setTransacoes(trs)
      setPessoas(pes)
      setCategorias(cats)
    } catch (e) {
      setErro((e as ApiError).message ?? 'Erro ao carregar dados')
    }
  }

  useEffect(() => {
    void carregar()
  }, [])

  async function criar(e: React.FormEvent) {
    e.preventDefault()

    try {
      setErro('')

      const valorNumero = Number(valor)

      if (!descricao.trim()) {
        setErro('Informe uma descrição válida.')
        return
      }

      if (!valor || Number.isNaN(valorNumero) || valorNumero <= 0) {
        setErro('Informe um valor válido maior que zero.')
        return
      }

      if (!tipo) {
        setErro('Selecione o tipo da transação.')
        return
      }

      if (!pessoaId) {
        setErro('Selecione a pessoa.')
        return
      }

      if (!categoriaId) {
        setErro('Selecione a categoria.')
        return
      }

      await transacaoService.criar({
        descricao: descricao.trim(),
        valor: valorNumero,
        tipo: Number(tipo) as TipoTransacao,
        pessoaId,
        categoriaId,
      })

      setDescricao('')
      setValor('')
      setTipo('')
      setPessoaId('')
      setCategoriaId('')
      await carregar()
    } catch (e) {
      setErro((e as ApiError).message ?? 'Erro ao criar transação')
    }
  }

  return (
    <section className="card">
      <h1 className="page-title">Transações</h1>

      <form onSubmit={criar} className="transacoes-form">
        <input
          placeholder="Descrição"
          value={descricao}
          onChange={(e) => setDescricao(e.target.value)}
          maxLength={400}
          required
        />

        <input
          type="number"
          min={0.01}
          step="0.01"
          placeholder="Valor"
          value={valor}
          onChange={(e) => setValor(e.target.value)}
          required
        />

        <select value={tipo} onChange={(e) => setTipo(e.target.value)} required>
          <option value="" disabled>
            Tipo
          </option>
          <option value={TipoTransacao.Despesa}>Despesa</option>
          <option value={TipoTransacao.Receita}>Receita</option>
        </select>

        <select value={pessoaId} onChange={(e) => setPessoaId(e.target.value)} required>
          <option value="" disabled>
            Pessoa
          </option>
          {pessoas.map((p) => (
            <option key={p.id} value={p.id}>
              {p.nome}
            </option>
          ))}
        </select>

        <select value={categoriaId} onChange={(e) => setCategoriaId(e.target.value)} required>
          <option value="" disabled>
            Categoria
          </option>
          {categorias.map((c) => (
            <option key={c.id} value={c.id}>
              {c.descricao}
            </option>
          ))}
        </select>

        <div className="form-actions">
          <button type="submit">Criar</button>
        </div>
      </form>

      {erro && <p className="error">{erro}</p>}

      <div className="table-wrap">
        <table className="table mobile-stack">
          <thead>
            <tr>
              <th>Descrição</th>
              <th>Valor</th>
              <th>Tipo</th>
              <th>Pessoa</th>
              <th>Categoria</th>
            </tr>
          </thead>
          <tbody>
            {transacoes.map((t) => (
              <tr key={t.id}>
                <td data-label="Descrição">{t.descricao}</td>
                <td data-label="Valor">{formatarMoeda(t.valor)}</td>
                <td data-label="Tipo">{formatarTipo(t.tipo)}</td>
                <td data-label="Pessoa">{t.pessoaNome}</td>
                <td data-label="Categoria">{t.categoriaNome}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </section>
  )
}