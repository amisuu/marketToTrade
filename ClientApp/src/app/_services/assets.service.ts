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
}
