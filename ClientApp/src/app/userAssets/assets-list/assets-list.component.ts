import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, take } from 'rxjs';
import { Metal } from 'src/app/_models/metal';
import { AssetsService } from 'src/app/_services/assets.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-assets-list',
  templateUrl: './assets-list.component.html',
  styleUrls: ['./assets-list.component.css']
})
export class AssetsListComponent implements OnInit {
  userAssets$: Observable<Metal[]>;
  currentAsset: Metal;
  baseUrl = environment.apiUrl;

  constructor(private assetService: AssetsService, private router: Router) {
    this.assetService.currentMetal$.pipe(take(1)).subscribe(asset => this.currentAsset = asset);
  }

  ngOnInit(): void {
    this.userAssets$ = this.assetService.getAssets();
  }

  showAddForm() {
    this.router.navigateByUrl('asset/add');
  }
}
