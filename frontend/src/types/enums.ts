export const Finalidade = {
  Despesa: 1,
  Receita: 2,
  Ambas: 3,
} as const;

export type Finalidade = (typeof Finalidade)[keyof typeof Finalidade];

export const TipoTransacao = {
  Despesa: 1,
  Receita: 2,
} as const;

export type TipoTransacao = (typeof TipoTransacao)[keyof typeof TipoTransacao];