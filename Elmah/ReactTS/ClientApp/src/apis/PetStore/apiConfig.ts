// the following code from:
// https://medium.com/@enetoOlveda/how-to-use-axios-typescript-like-a-pro-7c882f71e34a

import * as qs from "qs";
import { PathLike } from "fs";

// withCredentials is disabled. developer should write code on how to get token or other authentication type in ApiBase.ts
export const apiConfig = {
    returnRejectedPromiseOnError: true,
    withCredentials: false,
    timeout: 30000,
    baseURL: "https://petstore3.swagger.io/api/v3",
    headers: {
        common: {
            "Content-Type": "application/json",
            Accept: "application/json",
        },
    },
    paramsSerializer: (params: PathLike) => qs.stringify(params, { indices: false }),
}


// 'Access-Control-Allow-Origin': '*',
// 'Access-Control-Allow-Headers': 'Access-Control-Allow-Headers, Origin,Accept, X-Requested-With, Content-Type, Access-Control-Request-Method, Access-Control-Request-Headers'
