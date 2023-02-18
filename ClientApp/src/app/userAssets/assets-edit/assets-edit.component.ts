import { Component, HostListener, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
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
  userAsset: Metal[];
  @Input() asset: Metal;
  user: User;
  currentAsset: Metal;
  userAssets$: Observable<Metal[]>;
  showFormFlag = false;
  addForm: FormGroup;
  enumYesNo: string[] = ['Yes', 'No'];
  @ViewChild('editForm') editForm: NgForm;

  constructor(private assetService: AssetsService,
              private accountService: AccountService,
              private fb: FormBuilder,) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
   }

  ngOnInit(): void {
    this.loadAssets();
    //this.initializeForm();
  }

  loadAssets() {
    this.assetService.getAssets().subscribe(asset => {
      this.assets = asset
      asset.forEach(element => {
        if (element.appUserId == this.user.id) {
          //this.userAsset.push(element);
          console.log(element);
        }
      });
    });
  }

  showForm() {
    this.showFormFlag = true;
  }

 /* initializeForm() {
    this.addForm = this.fb.group({
      type: ['',Validators.required],
      form: ['',Validators.required],
      mass: ['',Validators.required],
      fineness: ['',Validators.required],
      quantity: ['',Validators.required],
      producer: ['',Validators.required],
      price: ['',Validators.required],
      year: ['',Validators.required],
      condition: ['',Validators.required],
      isOriginalPackage: ['', Validators.required],
      isReceipt: ['', Validators.required],
    })
  }*/

  updateAsset(){
    console.log(this.assets);
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
