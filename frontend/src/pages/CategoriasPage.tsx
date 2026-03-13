import { useEffect, useState } from 'react'
import { categoriaService } from '../services/categoriaService'
import type { ApiError, Categoria } from '../types/index'
import { Finalidade } from '../types/index'
import { formatarFinalidade } from '../utils/formatters'

export function CategoriasPage() {
  const [categorias, setCategorias] = useState<Categoria[]>([])
  const [descricao, setDescricao] = useState('')
  const [finalidade, setFinalidade] = useState('')
  const [erro, setErro] = useState('')

  async function carregar() {
    try {
      setErro('')
      setCategorias(await categoriaService.obterTodos())
    } catch (e) {
      setErro((e as ApiError).message ?? 'Erro ao carregar categorias')
    }
  }

  useEffect(() => {
    void carregar()
  }, [])

  async function criar(e: React.FormEvent) {
    e.preventDefault()

    try {
      setErro('')

      if (!descricao.trim()) {
        setErro('Informe uma descrição válida.')
        return
      }

      if (!finalidade) {
        setErro('Selecione a finalidade.')
        return
      }

      await categoriaService.criar({
        descricao: descricao.trim(),
        finalidade: Number(finalidade) as Finalidade,
      })

      setDescricao('')
      setFinalidade('')
      await carregar()
    } catch (e) {
      setErro((e as ApiError).message ?? 'Erro ao criar categoria')
    }
  }

  return (
    <section className="card">
      <h1 className="page-title">Categorias</h1>

      <form onSubmit={criar}>
        <input
          placeholder="Descrição"
          value={descricao}
          onChange={(e) => setDescricao(e.target.value)}
          maxLength={400}
          required
        />

        <select
          value={finalidade}
          onChange={(e) => setFinalidade(e.target.value)}
          required
        >
          <option value="" disabled>Finalidade</option>
          <option value={Finalidade.Despesa}>Despesa</option>
          <option value={Finalidade.Receita}>Receita</option>
          <option value={Finalidade.Ambas}>Ambas</option>
        </select>

        <button type="submit">Criar</button>
      </form>

      {erro && <p className="error">{erro}</p>}

      <table className="table">
        <thead>
          <tr>
            <th>Descrição</th>
            <th>Finalidade</th>
          </tr>
        </thead>
        <tbody>
          {categorias.map((c) => (
            <tr key={c.id}>
              <td>{c.descricao}</td>
              <td>{formatarFinalidade(c.finalidade)}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </section>
  )
}