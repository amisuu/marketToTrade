import { map } from "rxjs";
import { PaginatedResult } from "../_models/pagination";
import { HttpParams, HttpClient } from "@angular/common/http";

export function getPaginatedResult<T>(url: string, params: HttpParams, http: HttpClient) {
    const paginatedResult: PaginatedResult<T> = new PaginatedResult<T>();
    return http.get<T>(url, { observe: 'response', params }).pipe(
      map(response => {
        if (response.body){
            paginatedResult.result = response.body;
        }
        const pagination = response.headers.get('Pagination')
        if (pagination) {
          paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }
        return paginatedResult;
      })
    );
  }

export function getPaginationHeaders(pageNumber: number, pageSize: number) {
    let params = new HttpParams();

    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    return params;
}