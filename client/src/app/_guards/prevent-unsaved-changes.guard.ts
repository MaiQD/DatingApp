import { ConfirmService } from './../_services/confirm.service';
import { MemberEditComponent } from './../members/member-edit/member-edit.component';
import { Injectable } from '@angular/core';
import { CanDeactivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesGuard implements CanDeactivate<unknown> {

  constructor(private confirmService: ConfirmService) {
    
  }
  

  canDeactivate(
    component: MemberEditComponent): Observable<boolean> | boolean  {
      if(component.editForm.dirty)
      {
        return this.confirmService.confirm()
        //return confirm('Are you sure you want to continue? Any unsaved changes will be lost')
      }
    return true;
  }
  
}
