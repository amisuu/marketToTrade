import { Component, Input, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { Metal } from 'src/app/_models/metal';
import { User } from 'src/app/_models/user';
import { AssetsService } from 'src/app/_services/assets.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {
  @Input() asset: Metal;
  @Input() user: User;
  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  hasAnotherDropZoneOver = false;
  baseUrl = environment.apiUrl;

  constructor(private assetService: AssetsService) {}

  ngOnInit(): void {
    this.initializeUploader();
  }

  fileOverBase(e: any) {
    this.hasBaseDropZoneOver = e;
  }

  /*setMainPhoto(photoId, id) {
    this.assetService.setMainPhoto(photoId.id, id).subscribe(() => {
      this.asset.photoUrl = photoId.url;
      this.accountService.setCurrentUser(this.user);
    });
  }*/

  initializeUploader() {
    var idToString = this.asset.id.toString();
    this.uploader = new FileUploader({
      url: this.baseUrl + 'assets/add-photo',
      authToken: 'Bearer ' + this.user.token,
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024,
      headers: [{ name: 'id', value: idToString }]
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
