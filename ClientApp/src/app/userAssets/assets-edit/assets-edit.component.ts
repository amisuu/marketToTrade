import { Component, HostListener, Input, OnInit, ViewChild } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, NgForm, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Observable, take } from 'rxjs';
import { AssetParams } from 'src/app/_models/assetParams';
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
  addForm: UntypedFormGroup;
  assetParams: AssetParams;
  showFormFlag = false;
  userAsset: Metal[] = [];
  user: User;

  @Input() asset: Metal;
  @ViewChild('editAssetForm') editAssetForm: NgForm | undefined;
  @HostListener('window:beforeunload', ['event']) unloadNotification($event: any) {
    if (this.editAssetForm?.dirty) {
      $event.returnValue = true;
    }
  }

  enumYesNo: string[] = ['Yes', 'No'];

  constructor(private assetService: AssetsService,
              private accountService: AccountService,
              private fb: UntypedFormBuilder,
              private toastr: ToastrService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
    this.assetParams = this.assetService.getAssetParams();
   }

  ngOnInit(): void {
    this.loadAssets();
    this.initializeForm();
  }

  loadAssets() {
    console.log(this.user.id);
    this.assetService.getAssets(this.assetParams).subscribe(asset => {
      asset.result.forEach(element => {
        if (element.appUserId == this.user.id) {
          console.log("elemtn - " + element);
          this.userAsset.push(element);
        }
      });
    });
  }

  showForm() {
    this.showFormFlag = true;
  }

  initializeForm() {
    this.addForm = this.fb.group({
      id: [''],
      type: ['',Validators.required],
      form: ['',Validators.required],
      title: ['',Validators.required],
      mass: ['',Validators.required],
      fineness: ['',Validators.required],
      quantity: ['',Validators.required],
      producer: ['',Validators.required],
      price: ['',Validators.required],
      year: ['',Validators.required],
      condition: ['',Validators.required]
    })
  }

  updateAsset(id: number){
    this.assetService.updateAsset(this.editAssetForm?.value).subscribe({
      next: _ => {
        this.toastr.success("Updated successfully");
        this.editAssetForm?.reset(this.userAsset.find(x => x.id == id));
      }
    });
  }
}
