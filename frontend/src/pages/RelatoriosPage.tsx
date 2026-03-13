import { useEffect, useState } from 'react'
import { relatorioService } from '../services/relatorioService'
import type {
  ApiError,
  RelatorioTotaisPorCategoria,
  RelatorioTotaisPorPessoa,
} from '../types/index'
import { formatarFinalidade, formatarMoeda } from '../utils/formatters'

export function RelatoriosPage() {
  const [porPessoa, setPorPessoa] = useState<RelatorioTotaisPorPessoa | null>(null)
  const [porCategoria, setPorCategoria] = useState<RelatorioTotaisPorCategoria | null>(null)
  const [erro, setErro] = useState('')
  const [carregando, setCarregando] = useState(true)

  async function carregar() {
    try {
      setErro('')
      setCarregando(true)

      const [dadosPessoa, dadosCategoria] = await Promise.all([
        relatorioService.obterTotaisPorPessoa(),
        relatorioService.obterTotaisPorCategoria(),
      ])

      setPorPessoa(dadosPessoa)
      setPorCategoria(dadosCategoria)
    } catch (e) {
      setErro((e as ApiError).message ?? 'Erro ao carregar relatórios')
    } finally {
      setCarregando(false)
    }
  }

  useEffect(() => {
    void carregar()
  }, [])

  if (carregando) {
    return (
      <section className="card">
        <h1 className="page-title">Relatórios</h1>
        <p>Carregando...</p>
      </section>
    )
  }

  return (
    <section className="card">
      <h1 className="page-title">Relatórios</h1>

      {erro && <p className="error">{erro}</p>}

      {!erro && (
        <>
          <h3>Totais por Pessoa</h3>
          <table className="table">
            <thead>
              <tr>
                <th>Pessoa</th>
                <th>Idade</th>
                <th>Receitas</th>
                <th>Despesas</th>
                <th>Saldo</th>
              </tr>
            </thead>
            <tbody>
              {porPessoa?.pessoas.map((p) => (
                <tr key={p.pessoaId}>
                  <td>{p.nomePessoa}</td>
                  <td>{p.idade}</td>
                  <td>{formatarMoeda(p.totalReceitas)}</td>
                  <td>{formatarMoeda(p.totalDespesas)}</td>
                  <td>{formatarMoeda(p.saldoLiquido)}</td>
                </tr>
              ))}
            </tbody>
          </table>

          {porPessoa && (
            <p>
              <strong>Total geral (Pessoas): </strong>
              {formatarMoeda(porPessoa.totalizador.saldoLiquido)}
            </p>
          )}

          <h3 style={{ marginTop: 24 }}>Totais por Categoria</h3>
          <table className="table">
            <thead>
              <tr>
                <th>Categoria</th>
                <th>Finalidade</th>
                <th>Receitas</th>
                <th>Despesas</th>
                <th>Saldo</th>
              </tr>
            </thead>
            <tbody>
              {porCategoria?.categorias.map((c) => (
                <tr key={c.categoriaId}>
                  <td>{c.descricaoCategoria}</td>
                  <td>{formatarFinalidade(c.finalidade)}</td>
                  <td>{formatarMoeda(c.totalReceitas)}</td>
                  <td>{formatarMoeda(c.totalDespesas)}</td>
                  <td>{formatarMoeda(c.saldoLiquido)}</td>
                </tr>
              ))}
            </tbody>
          </table>

          {porCategoria && (
            <p>
              <strong>Total geral (Categorias): </strong>
              {formatarMoeda(porCategoria.totalizador.saldoLiquido)}
            </p>
          )}
        </>
      )}
    </section>
  )
}