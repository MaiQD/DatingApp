import { AccountService } from './_services/account.service';
import { User } from './_models/user';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
	title = 'Dating app title';
	users :any;
	constructor(private accountService:AccountService){
		
	}
	ngOnInit() {
		this.setCurrentUser();
	}
	/// gán user hiện tại = user từ localStorage
	setCurrentUser()
	{
		const user:User = JSON.parse(localStorage.getItem('user'));
		this.accountService.setCurrentUser(user);
	}
}

