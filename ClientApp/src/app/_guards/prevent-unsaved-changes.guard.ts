import { Injectable, inject } from '@angular/core';
import { MemberEditComponent } from '../members/member-edit/member-edit.component';
import { ConfirmPopupService } from '../_services/confirm-popup.service';
import { CanDeactivateFn } from '@angular/router';


export const PreventUnsavedChangesGuard: CanDeactivateFn<MemberEditComponent> = (component) => {
  const confirmService = inject(ConfirmPopupService);

  if (component.editForm?.dirty) {
    return confirmService.confirm();
  }

  return true;
}
