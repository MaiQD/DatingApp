import { error } from 'protractor';
import { map } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from './../_services/account.service';
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private accountService: AccountService,
    private toast: ToastrService) {
    
  }
  
  canActivate(): Observable<boolean>  {
    return this.accountService.currentUser$.pipe(
      map( user =>{
        if(user)
          return true;
          this.toast.error("You don't have permission.");
      })
    )
  }
  
}
