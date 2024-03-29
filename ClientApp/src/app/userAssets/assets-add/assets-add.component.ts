import { Component, Input, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { AssetsService } from 'src/app/_services/assets.service';
import { Metal } from 'src/app/_models/metal';
import { YesOrNo } from 'src/app/enum/yesOrNoEnum';
import { Router } from '@angular/router';

@Component({
  selector: 'app-assets-add',
  templateUrl: './assets-add.component.html',
  styleUrls: ['./assets-add.component.css']
})
export class AssetsAddComponent implements OnInit {
  yesOrNo: YesOrNo;
  addForm: UntypedFormGroup;
  asset: Metal;

  constructor(private fb: UntypedFormBuilder,
              private assetService: AssetsService,
              private router: Router) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
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
  }

  onSubmit(assetFromForm: any) {
    this.assetService.addAsset(assetFromForm.value).subscribe({
      next: _ => {
        this.router.navigateByUrl('/assets').then(() => {
          window.location.reload();
        });
      }
    });
  }

  onCancel() {
    this.router.navigateByUrl('/assets');
  }
}
