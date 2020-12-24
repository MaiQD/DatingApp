import { AccountService } from './../_services/account.service';
import { Component, OnInit } from '@angular/core';

@Component({
	selector: 'app-nav',
	templateUrl: './nav.component.html',
	styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
	model: any = {
	};
	constructor(public accountService: AccountService) {
	}
	ngOnInit() {
		this.getCurrentUser();
	}
	login() {
		this.accountService.login(this.model).subscribe(res => {
			console.log(res);
		}, error =>{
			console.error(error);
			
		})
	}
	logout(){
		this.accountService.logout();
	}
	getCurrentUser(){
		this.accountService.currentUser$.subscribe(user=>{
			//!! turn object to boolean
		},error=>{
			console.error(error);
		})
	}
}
