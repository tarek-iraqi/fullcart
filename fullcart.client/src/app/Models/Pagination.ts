export interface Pagination {
  hasPrevious: boolean;
  hasNext: boolean;
  totalPages: number;
  totalRecords: number;
  pageIndex: number;
}
