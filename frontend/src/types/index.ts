import { Finalidade, TipoTransacao } from './enums';

export interface Pessoa {
  id: string;
  nome: string;
  idade: number;
}

export interface CriarPessoa {
  nome: string;
  idade: number;
}

export interface AtualizarPessoa {
  id: string;
  nome: string;
  idade: number;
}

export interface Categoria {
  id: string;
  descricao: string;
  finalidade: Finalidade;
}

export interface CriarCategoria {
  descricao: string;
  finalidade: Finalidade;
}

export interface Transacao {
  id: string;
  descricao: string;
  valor: number;
  tipo: TipoTransacao;
  categoriaId: string;
  categoriaNome: string;
  pessoaId: string;
  pessoaNome: string;
}

export interface CriarTransacao {
  descricao: string;
  valor: number;
  tipo: TipoTransacao;
  categoriaId: string;
  pessoaId: string;
}

export interface TotaisPorPessoa {
  pessoaId: string;
  nomePessoa: string;
  idade: number;
  totalReceitas: number;
  totalDespesas: number;
  saldoLiquido: number;
}

export interface TotaisPorCategoria {
  categoriaId: string;
  descricaoCategoria: string;
  finalidade: Finalidade;
  totalReceitas: number;
  totalDespesas: number;
  saldoLiquido: number;
}

export interface TotalizadorGeral {
  totalReceitas: number;
  totalDespesas: number;
  saldoLiquido: number;
}

export interface RelatorioTotaisPorPessoa {
  pessoas: TotaisPorPessoa[];
  totalizador: TotalizadorGeral;
}

export interface RelatorioTotaisPorCategoria {
  categorias: TotaisPorCategoria[];
  totalizador: TotalizadorGeral;
}

export interface ApiError {
  statusCode: number;
  message: string;
  details?: Record<string, unknown>;
}