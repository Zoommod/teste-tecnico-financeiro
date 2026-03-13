import { Finalidade, TipoTransacao } from '../types/enums'

export const formatarMoeda = (valor: number): string =>
  new Intl.NumberFormat('pt-BR', { style: 'currency', currency: 'BRL' }).format(valor)

export const formatarFinalidade = (finalidade: Finalidade): string => {
  if (finalidade === Finalidade.Despesa) return 'Despesa'
  if (finalidade === Finalidade.Receita) return 'Receita'
  return 'Ambas'
}

export const formatarTipo = (tipo: TipoTransacao): string =>
  tipo === TipoTransacao.Despesa ? 'Despesa' : 'Receita'