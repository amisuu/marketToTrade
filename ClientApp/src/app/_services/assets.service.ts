import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Metal } from '../_models/metal';

@Injectable({
  providedIn: 'root'
})
export class AssetsService {
  baseUrl = environment.apiUrl;
  private currentMetalSource = new ReplaySubject<Metal>(1);
  currentMetal$ = this.currentMetalSource.asObservable();

  constructor(private http: HttpClient) { }

  getAssets() {
    return this.http.get<Metal[]>(this.baseUrl + 'assets');
  }

  getAsset(id: any) {
    return this.http.get<Metal>(this.baseUrl + 'assets/' + id);
  }

  setCurrentMetal(metal: Metal) {
    this.currentMetalSource.next(metal);
  }

  updateAsset(asset: Metal) {
    return this.http.put(this.baseUrl + 'assets', asset);
  }

  addLike(id: any) {
    return this.http.post(this.baseUrl + 'likes/' + id, {});
  }

  getLikes(predicate: string) {
    return this.http.get(this.baseUrl + 'likes?=' + predicate);
  }

  getUserAssets(id: any) {
    return this.http.get(this.baseUrl + 'assets/user-assets' + id);
  }

  setMainPhoto(photoId: number, id: any) {
    return this.http.put(this.baseUrl + 'assets/set-main-photo', photoId, id);
  }

  addAsset(asset: Metal) {
    return this.http.post(this.baseUrl + 'assets/add-asset', asset);
  }
}
