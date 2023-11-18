import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, take } from 'rxjs';
import { AssetParams } from 'src/app/_models/assetParams';
import { Metal } from 'src/app/_models/metal';
import { AssetsService } from 'src/app/_services/assets.service';
import { environment } from 'src/environments/environment';
import { Pagination } from 'src/app/_models/pagination';

@Component({
  selector: 'app-assets-list',
  templateUrl: './assets-list.component.html',
  styleUrls: ['./assets-list.component.css']
})
export class AssetsListComponent implements OnInit {
  userAssets$: Observable<Metal[]>;
  currentAsset: Metal;
  baseUrl = environment.apiUrl;
  pagination: Pagination;
  assetParams: AssetParams;
  assets: Metal[];

  constructor(private assetService: AssetsService, private router: Router) {
    this.assetService.currentMetal$.pipe(take(1)).subscribe(asset => this.currentAsset = asset);
    this.assetParams = this.assetService.getAssetParams();
  }

  ngOnInit(): void {
    this.loadAssets();
  }

  loadAssets() {
    this.assetService.getAssets(this.assetParams).subscribe(response => {
      this.assets = response.result;
      this.pagination = response.pagination;
    });
  }

  showAddForm() {
    this.router.navigateByUrl('asset/add');
  }
}
