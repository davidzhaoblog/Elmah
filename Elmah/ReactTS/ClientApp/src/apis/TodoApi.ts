import { Todo } from "src/features/Todo/types";
import { IListRequest, IListResponse } from "src/framework/IndexComponentBase";
import { QueryOrderDirections } from "src/framework/Queries/QueryOrderDirections";

export class TodoApi {
  public GetIndexVM = (payload: IListRequest<Todo>): Promise<IListResponse<Todo[]>> => {
    let result1 = [
      { id: '1', completed: true, text: 'todo 001' },
      { id: '2', completed: true, text: 'todo 002' },
      { id: '3', completed: true, text: 'todo 003' },
      { id: '4', completed: true, text: 'todo 004' },
      { id: '5', completed: true, text: 'todo 005' },
      { id: '6', completed: true, text: 'todo 006' },
      { id: '7', completed: true, text: 'todo 007' },
      { id: '8', completed: true, text: 'todo 008' },
      { id: '9', completed: true, text: 'todo 009' },
      { id: '10', completed: true, text: 'todo 010' },
      { id: '11', completed: true, text: 'todo 011' },
      { id: '12', completed: true, text: 'todo 012' },
      { id: '13', completed: true, text: 'todo 013' },
      { id: '14', completed: true, text: 'todo 014' },
      { id: '15', completed: true, text: 'todo 015' },
      { id: '16', completed: true, text: 'todo 016' },
      { id: '17', completed: true, text: 'todo 017' },
      { id: '18', completed: true, text: 'todo 018' },
      { id: '19', completed: true, text: 'todo 019' },
      { id: '20', completed: true, text: 'todo 020' },
      { id: '21', completed: true, text: 'todo 021' },
      { id: '22', completed: true, text: 'todo 022' },
      { id: '23', completed: true, text: 'todo 023' },
      { id: '24', completed: true, text: 'todo 024' },
      { id: '25', completed: true, text: 'todo 025' },
      { id: '26', completed: true, text: 'todo 026' },
      { id: '27', completed: true, text: 'todo 027' },
      { id: '28', completed: true, text: 'todo 028' },
      { id: '29', completed: true, text: 'todo 029' },
    ];
    if(payload.orderBy.propertyName === 'text')
    {
      if(payload.orderBy.direction === QueryOrderDirections.Ascending)
        result1 = result1.sort((a, b) => (a.text < b.text ? -1 : 1 ));
      else
      result1 = result1.sort((a, b) => (a.text > b.text ? -1 : 1 ));
    }
    const result = result1.slice((payload.queryPagingSetting.currentPage - 1) * payload.queryPagingSetting.pageSize, payload.queryPagingSetting.currentPage * payload.queryPagingSetting.pageSize);
    const queryPagingSetting = {
      currentPage: payload.queryPagingSetting.currentPage,
      pageSize: payload.queryPagingSetting.pageSize,
      recordCountOfCurrentPage: result.length,
      countOfRecords: result1.length,
      countOfPages: Math.ceil(result1.length / payload.queryPagingSetting.pageSize)
    };

    return new Promise<IListResponse<Todo[]>>((resolve) => {
      resolve({ result, orderBy: payload.orderBy, queryPagingSetting } as IListResponse<Todo[]>);
    });
  }
}

export const todoApi = new TodoApi();