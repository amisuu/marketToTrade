import { Component, HostListener, Input, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Observable, take } from 'rxjs';
import { Metal } from 'src/app/_models/metal';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { AssetsService } from 'src/app/_services/assets.service';

@Component({
  selector: 'app-assets-edit',
  templateUrl: './assets-edit.component.html',
  styleUrls: ['./assets-edit.component.css']
})
export class AssetsEditComponent implements OnInit {
  assets: Metal[];
  @Input() asset: Metal;
  user: User;
  currentAsset: Metal;
  userAssets$: Observable<Metal[]>;
  showFormFlag = false;


  constructor(private assetService: AssetsService, private accountService: AccountService, private route: ActivatedRoute) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
   }

  ngOnInit(): void {
    this.loadAssets();
  }

  loadAssets() {
    this.assetService.getAssets().subscribe(asset => {
      this.assets = asset
    });
  }

  showForm() {
    this.showFormFlag = true;
  }
/*
  loadAsset() {
    this.assetService.getAsset(this.route.snapshot.paramMap.get('id')).subscribe(asset => {
      this.asset = asset;
      console.log(this.asset.userId);
    })
  }
  */
}
