import { ResponseStatus } from './ResponseStatus'

export interface IResponse<TResponse>
{
  message: TResponse;
  businessLogicLayerResponseStatus: ResponseStatus;
  serverErrorMessage: string;
}
