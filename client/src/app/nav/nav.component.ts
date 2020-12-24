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
	loggedIn: boolean;
	constructor(private accountService: AccountService) {
	}
	ngOnInit() {
	}
	login() {
		this.accountService.login(this.model).subscribe(res => {
			this.loggedIn = true;
			console.log(res);
		}, error =>{
			console.error(error);
			
		})
	}
	logout(){
		this.loggedIn = false;
	}
}
