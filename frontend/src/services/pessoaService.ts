import { api } from './api';
import type { AtualizarPessoa, CriarPessoa, Pessoa } from '../types';

export const pessoaService = {
  obterTodos: async (): Promise<Pessoa[]> => (await api.get('/pessoas')).data,
  obterPorId: async (id: string): Promise<Pessoa> => (await api.get(`/pessoas/${id}`)).data,
  criar: async (dto: CriarPessoa): Promise<Pessoa> => (await api.post('/pessoas', dto)).data,
  atualizar: async (dto: AtualizarPessoa): Promise<Pessoa> =>
    (await api.put(`/pessoas/${dto.id}`, dto)).data,
  deletar: async (id: string): Promise<void> => {
    await api.delete(`/pessoas/${id}`);
  },
};