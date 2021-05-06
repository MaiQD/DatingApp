import { MembersService } from './../_services/members.service';
import { Member } from './../_models/member';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable({
    providedIn:'root'
})
//lấy data trước khi tạo template view
export class MemberDetailedResolver implements Resolve<Member>{

    constructor(private membersService:MembersService) {}
    

    resolve(route: ActivatedRouteSnapshot): Observable<Member>  {
        return this.membersService.getMember(route.paramMap.get('username'));
    }

}