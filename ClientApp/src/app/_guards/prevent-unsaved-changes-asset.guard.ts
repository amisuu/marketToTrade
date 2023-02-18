import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanDeactivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AssetsEditComponent } from '../userAssets/assets-edit/assets-edit.component';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesAssetGuard implements CanDeactivate<unknown> {
  canDeactivate(component: AssetsEditComponent): boolean {
    if (component.editForm.dirty) {
      return confirm('Are you sure you want to continue? Any unsaved changes will be lost');
    }
    return true;
  }

}
