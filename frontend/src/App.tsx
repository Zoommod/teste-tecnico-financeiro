import { BrowserRouter, NavLink, Route, Routes } from 'react-router-dom';
import { HomePage } from './pages/HomePage';
import { PessoasPage } from './pages/PessoasPage';
import { CategoriasPage } from './pages/CategoriasPage';
import { TransacoesPage } from './pages/TransacoesPage';
import { RelatoriosPage } from './pages/RelatoriosPage';

export default function App() {
  return (
    <BrowserRouter>
      <nav className="navigation">
        <div className="nav-links">
          <NavLink to="/">Início</NavLink>
          <NavLink to="/pessoas">Pessoas</NavLink>
          <NavLink to="/categorias">Categorias</NavLink>
          <NavLink to="/transacoes">Transações</NavLink>
          <NavLink to="/relatorios">Relatórios</NavLink>
        </div>
      </nav>

      <main className="container">
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/pessoas" element={<PessoasPage />} />
          <Route path="/categorias" element={<CategoriasPage />} />
          <Route path="/transacoes" element={<TransacoesPage />} />
          <Route path="/relatorios" element={<RelatoriosPage />} />
        </Routes>
      </main>
    </BrowserRouter>
  );
}