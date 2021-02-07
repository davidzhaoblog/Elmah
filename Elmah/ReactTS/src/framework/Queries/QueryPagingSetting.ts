// import { NameValuePair } from "../Models/NameValuePair";

export interface QueryPagingSetting {
  currentPage: number;
  pageSize: number;
  // originalPageSize: number;
  countOfRecords: number;
  countOfPages: number; // TODO: we should add this in C# web api
  recordCountOfCurrentPage: number;
  // pageSizeSelectionList: Array<NameValuePair<string>>;
  // pageNumberSelectionList: Array<NameValuePair<string>>;
}

export const createQueryPagingSetting = (pageSize: number, currentPage: number): QueryPagingSetting => {
  return {
    currentPage,
    pageSize,
    // originalPageSize: pageSize,
    countOfRecords: 0,
    countOfPages: 0,
    recordCountOfCurrentPage: 0,
    // pageSizeSelectionList: [],
    // pageNumberSelectionList: [],
  };
}