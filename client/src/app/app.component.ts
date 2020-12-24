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
	constructor(private http: HttpClient, private accountService:AccountService){
		
	}
	ngOnInit() {
		//hiển thị danh sách user
		this.getUser();
		this.setCurrentUser();
	}
	/// gán user hiện tại = user từ localStorage
	setCurrentUser()
	{
		const user:User = JSON.parse(localStorage.getItem('user'));
		this.accountService.setCurrentUser(user);
	}
	getUser(){
		this.http.get('https://localhost:44356/api/Users/').subscribe(response =>{
			this.users = response;
		},error =>{
			console.log(error);
		})
	}
}

