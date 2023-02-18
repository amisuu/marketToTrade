import { Component, Input, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { take } from 'rxjs';
import { Metal } from 'src/app/_models/metal';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { AssetsService } from 'src/app/_services/assets.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {
  @Input() asset: Metal;
  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  hasAnotherDropZoneOver:boolean;
  baseUrl = environment.apiUrl;
  user: User;

  constructor(private accountService: AccountService, private assetService: AssetsService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user == user);
  }

  ngOnInit(): void {
    this.initializeUploader();
  }

  fileOverBase(e: any) {
    this.hasBaseDropZoneOver = e;
  }

  setMainPhoto(photoId, id) {
    this.assetService.setMainPhoto(photoId.id, id).subscribe(() => {
      this.asset.photoUrl = photoId.url;
      this.accountService.setCurrentUser(this.user);
    });
  }

  initializeUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl + 'asset/add-photo',
      authToken: 'Bearer ' + this.user.token,
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024,
    });

    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
    }

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if(response)
      {
        const photo = JSON.parse(response);
        this.asset.photos.push(photo);
      }
    }
  }
}
