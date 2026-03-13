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

  return (
    <section className="card">
      <h1 className="page-title">Relatórios</h1>

      {carregando && <p>Carregando...</p>}
      {erro && <p className="error">{erro}</p>}

      {!carregando && !erro && (
        <>
          <h3>Totais por Pessoa</h3>
          <div className="table-wrap">
            <table className="table mobile-stack">
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
                    <td data-label="Pessoa">{p.nomePessoa}</td>
                    <td data-label="Idade">{p.idade}</td>
                    <td data-label="Receitas">{formatarMoeda(p.totalReceitas)}</td>
                    <td data-label="Despesas">{formatarMoeda(p.totalDespesas)}</td>
                    <td data-label="Saldo">{formatarMoeda(p.saldoLiquido)}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>

          {porPessoa && (
            <p style={{ marginTop: 10 }}>
              <strong>Total geral (Pessoas): </strong>
              {formatarMoeda(porPessoa.totalizador.saldoLiquido)}
            </p>
          )}

          <h3 style={{ marginTop: 20 }}>Totais por Categoria</h3>
          <div className="table-wrap">
            <table className="table mobile-stack">
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
                    <td data-label="Categoria">{c.descricaoCategoria}</td>
                    <td data-label="Finalidade">{formatarFinalidade(c.finalidade)}</td>
                    <td data-label="Receitas">{formatarMoeda(c.totalReceitas)}</td>
                    <td data-label="Despesas">{formatarMoeda(c.totalDespesas)}</td>
                    <td data-label="Saldo">{formatarMoeda(c.saldoLiquido)}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>

          {porCategoria && (
            <p style={{ marginTop: 10 }}>
              <strong>Total geral (Categorias): </strong>
              {formatarMoeda(porCategoria.totalizador.saldoLiquido)}
            </p>
          )}
        </>
      )}
    </section>
  )
}