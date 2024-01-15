import { Injectable } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Observable, map } from 'rxjs';
import { ConfirmPopupComponent } from '../modals/confirm-popup/confirm-popup.component';

@Injectable({
  providedIn: 'root'
})
export class ConfirmPopupService {
  bsModelRef?: BsModalRef<ConfirmPopupComponent>;

  constructor(private modalService: BsModalService) { }

  confirm(
    title = 'Confirmation', 
    message = 'Are you sure?', 
    btnOk = 'Ok', 
    btnCancel = 'Cancel'): Observable<boolean> {
      const config = {
        initialState: {
          title, 
          message,
          btnOk,
          btnCancel
        }
      }
      this.bsModelRef = this.modalService.show(ConfirmPopupComponent, config);
      return this.bsModelRef.onHidden!.pipe(
        map(() => {
          return this.bsModelRef!.content!.result
        })
      )
  }
}
