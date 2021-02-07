export interface IListProps
{
  classes?: any;
  items: any[];
  page: number;
  count: number;
  pageSize: number;
  pageSizes: number[];
  handlePageChange: (event: any, value: any) => void;
}
