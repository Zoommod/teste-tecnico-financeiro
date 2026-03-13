import { api } from './api';
import type { RelatorioTotaisPorCategoria, RelatorioTotaisPorPessoa } from '../types';

export const relatorioService = {
  obterTotaisPorPessoa: async (): Promise<RelatorioTotaisPorPessoa> =>
    (await api.get('/relatorios/totais-por-pessoa')).data,
  obterTotaisPorCategoria: async (): Promise<RelatorioTotaisPorCategoria> =>
    (await api.get('/relatorios/totais-por-categoria')).data,
};