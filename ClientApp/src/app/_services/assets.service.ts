import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, of, ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AssetParams } from '../_models/assetParams';
import { Metal } from '../_models/metal';
import { PaginatedResult } from '../_models/pagination';
import { UpdateMetal } from '../_models/updateMetal';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AssetsService {
  baseUrl = environment.apiUrl;
  private currentMetalSource = new ReplaySubject<Metal>(1);
  currentMetal$ = this.currentMetalSource.asObservable();
  assetCache = new Map();
  assetParams: AssetParams;

  constructor(private http: HttpClient) { 
    this.assetParams = new AssetParams();
  }

  getAssets(assetParams: AssetParams) {
    var response = this.assetCache.get(Object.values(assetParams).join('-'));
    if (response) {
      return of(response);
    }

    let params = this.getPaginationHeaders(assetParams.pageNumber, assetParams.pageSize);

    if (assetParams.search !== undefined) {
      params = params.append('search', assetParams.search);
    }
    params = params.append('orderBy', assetParams.orderBy);

    //return this.http.get<Metal[]>(this.baseUrl + 'assets');

    return this.getPaginatedResult<Metal[]>(this.baseUrl + 'assets', params)
        .pipe(map(response => {
          this.assetCache.set(Object.values(assetParams).join('-'), response);
          return response;
        }));
  }

  getAsset(id: any) {
    return this.http.get<Metal>(this.baseUrl + 'assets/' + id);
  }

  setCurrentMetal(metal: Metal) {
    this.currentMetalSource.next(metal);
  }

  updateAsset(updateAsset: UpdateMetal) {
    return this.http.put(this.baseUrl + 'assets/update-asset', updateAsset);
  }

  addLike(id: number) {
    return this.http.post(this.baseUrl + 'likes/' + id, {});
  }

  getLikes(predicate: string, pageNumber, pageSize) {
    let params = this.getPaginationHeaders(pageNumber, pageSize);
    params = params.append('predicate', predicate); //.append('id', id);
    ///this.http.get<Partial<Metal[]>>(this.baseUrl + 'likes?predicate=' + predicate, {params: params});
    return this.getPaginatedResult<Partial<Metal[]>>(this.baseUrl + 'likes/assetLikes/liked', params);
  }

  getUsersWithLikes(predicate: string, pageNumber, pageSize) {
    let params = this.getPaginationHeaders(pageNumber, pageSize);
    params = params.append('predicate', predicate); //.append('id', id);
    ///this.http.get<Partial<Metal[]>>(this.baseUrl + 'likes?predicate=' + predicate, {params: params});
    return this.getPaginatedResult<Partial<User[]>>(this.baseUrl + 'likes/usersWithLike/likedBy', params);
  }

  getUserAssets(id: any) {
    return this.http.get(this.baseUrl + 'assets/user-assets/' + id);
  }

  setMainPhoto(photoId: number, id: any) {
    return this.http.put(this.baseUrl + 'assets/set-main-photo', photoId, id);
  }

  addAsset(asset: Metal) {
    return this.http.post(this.baseUrl + 'assets/add-asset', asset);
  }

  getAssetParams() {
    return this.assetParams;
  }

  private getPaginatedResult<T>(url, params) {
    const paginatedResult: PaginatedResult<T> = new PaginatedResult<T>();
    return this.http.get<T>(url, { observe: 'response', params }).pipe(
      map(response => {
        paginatedResult.result = response.body;
        if (response.headers.get('Pagination') !== null) {
          paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }
        return paginatedResult;
      })
    );
  }

  private getPaginationHeaders(pageNumber: number, pageSize: number) {
    let params = new HttpParams();

    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    return params;
  }
}
