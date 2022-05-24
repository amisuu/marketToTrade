import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs';
import { Metal } from 'src/app/_models/metal';
import { AssetsService } from 'src/app/_services/assets.service';

@Component({
  selector: 'app-assets-list',
  templateUrl: './assets-list.component.html',
  styleUrls: ['./assets-list.component.css']
})
export class AssetsListComponent implements OnInit {
  userAssets: Metal[];
  currentAsset: Metal;

  constructor(private assetService: AssetsService) {
    this.assetService.currentMetal$.pipe(take(1)).subscribe(asset => this.currentAsset = asset);
  }

  ngOnInit(): void {
    this.loadMetals();
  }

  loadMetals() {
    this.assetService.getAssets().subscribe(assets => {
      this.userAssets = assets;
    });
  }
}
