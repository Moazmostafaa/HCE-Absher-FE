export interface PaginationResponse<T> {
  totalRecords: number;
  entities: T[];
}
