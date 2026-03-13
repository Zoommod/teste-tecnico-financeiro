import axios from 'axios';
import type { AxiosError } from 'axios';
import type { ApiError } from '../types';

export const api = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL,
  headers: { 'Content-Type': 'application/json' },
});

api.interceptors.response.use(
  (response) => response,
  (error: AxiosError<ApiError>) => {
    if (error.response?.data) return Promise.reject(error.response.data);
    return Promise.reject({
      statusCode: error.response?.status ?? 500,
      message: error.message ?? 'Erro inesperado',
    } as ApiError);
  },
);