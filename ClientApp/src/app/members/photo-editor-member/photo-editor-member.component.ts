import { Component, Input, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { take } from 'rxjs';
import { Member } from 'src/app/_models/member';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-photo-editor-member',
  templateUrl: './photo-editor-member.component.html',
  styleUrls: ['./photo-editor-member.component.css']
})
export class PhotoEditorMemberComponent implements OnInit {
  @Input() member: Member;
  @Input() user: User;
  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  hasAnotherDropZoneOver = false;
  baseUrl = environment.apiUrl;
  token: string;

  constructor(private accountService: AccountService, 
              private memberService: MembersService) {}

  ngOnInit(): void {
    this.initializeUploader();
  }

  fileOverBase(e: any) {
    this.hasBaseDropZoneOver = e;
  }

  /*setMainPhoto(photoId, id) {
    this.memberService.setMainPhoto(photoId.id, id).subscribe(() => {
      this.asset.photoUrl = photoId.url;
      this.accountService.setCurrentUser(this.user);
    });
  }*/

  initializeUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl + 'users/add-photo',
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
        this.member.photos.push(photo);
      }
    }
  }

  deletePhoto(photoId: number){
    this.memberService.deletePhoto(photoId).subscribe({
      next: _ =>{
        if(this.member)
        {
          this.member.photos = this.member.photos.filter(p => p.id !== photoId);
        }
      }
   });
  }
}
