import { Link } from 'react-router-dom'

export function HomePage() {
  return (
    <section className="card">
      <h1 className="page-title">Sistema Financeiro</h1>
      <p style={{ marginBottom: 16 }}>
        Gestão de Pessoas, Categorias, Transações e Relatórios.
      </p>

      <div className="acoes-botoes">
        <Link to="/pessoas">
          <button type="button">Pessoas</button>
        </Link>
        <Link to="/categorias">
          <button type="button">Categorias</button>
        </Link>
        <Link to="/transacoes">
          <button type="button">Transações</button>
        </Link>
        <Link to="/relatorios">
          <button type="button">Relatórios</button>
        </Link>
      </div>
    </section>
  )
}