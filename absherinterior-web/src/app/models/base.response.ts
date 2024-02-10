export interface BaseResponse<T> {
  isSuccess: boolean;
  message: string;
  errors: string[];
  status: number;
  entity: T;
}
