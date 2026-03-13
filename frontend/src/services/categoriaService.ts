import { api } from './api';
import type { Categoria, CriarCategoria } from '../types';

export const categoriaService = {
  obterTodos: async (): Promise<Categoria[]> => (await api.get('/categorias')).data,
  obterPorId: async (id: string): Promise<Categoria> => (await api.get(`/categorias/${id}`)).data,
  criar: async (dto: CriarCategoria): Promise<Categoria> => (await api.post('/categorias', dto)).data,
};