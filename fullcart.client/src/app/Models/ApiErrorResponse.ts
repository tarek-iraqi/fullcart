import { ApiError } from './ApiError';

export interface ApiErrorResponse {
  errors: ApiError[];
  isSuccess: boolean;
}
