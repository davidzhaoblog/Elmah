export interface QueryPagingSetting {
  currentPage: number;
  pageSize: number;
  countOfRecords: number;
  countOfPages: number;
  recordCountOfCurrentPage: number;

}

export const createQueryPagingSetting = (pageSize: number, currentPage: number): QueryPagingSetting => {
  return {
    currentPage,
    pageSize,
    countOfRecords: 0,
    countOfPages: 0,
    recordCountOfCurrentPage: 0,
  };
}