import { Component, HostListener, Input, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { take } from 'rxjs';
import { Member } from 'src/app/_models/member';
import { Metal } from 'src/app/_models/metal';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { AssetsService } from 'src/app/_services/assets.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-assets-edit',
  templateUrl: './assets-edit.component.html',
  styleUrls: ['./assets-edit.component.css']
})
export class AssetsEditComponent implements OnInit {
  @Input() assets: Metal[];
  @ViewChild('editForm') editForm: NgForm;
  member: Member;
  user: User;
  currentAsset: Metal;
  @HostListener('window:beforeunload', ['event']) unloadNotification($event: any) {
    if (this.editForm.dirty) {
      $event.returnValue = true;
    }
  }

  constructor(private assetService: AssetsService, private accountService: AccountService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
    this.assetService.currentMetal$.pipe(take(1)).subscribe(asset => this.currentAsset = asset);
   }

  ngOnInit(): void {
    console.log(this.currentAsset.id);
  }

  loadAsset() {

  }

}
