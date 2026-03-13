import { api } from './api';
import type { CriarTransacao, Transacao } from '../types';

export const transacaoService = {
  obterTodos: async (): Promise<Transacao[]> => (await api.get('/transacoes')).data,
  obterPorId: async (id: string): Promise<Transacao> => (await api.get(`/transacoes/${id}`)).data,
  criar: async (dto: CriarTransacao): Promise<Transacao> => (await api.post('/transacoes', dto)).data,
};