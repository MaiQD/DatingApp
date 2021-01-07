import { User } from './../_models/user';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { ReplaySubject } from 'rxjs';
import { error } from 'protractor';
@Injectable({
	providedIn: 'root'
})
export class AccountService {
	baseUrl = "https://localhost:44356/api/"
	//chỉ lưu 1 user
	private currentUserSource = new ReplaySubject<User>(1);
	currentUser$ = this.currentUserSource.asObservable();
	constructor(private http: HttpClient) { }
	login(model: any) {
		return this.http.post(this.baseUrl + 'account/login', model).pipe(map((response: User) => {
			const user = response;
			if (user) {
				localStorage.setItem("user", JSON.stringify(response));
				this.currentUserSource.next(user);
			}
		}))
	}
	register(model:any)
	{
		return this.http.post(this.baseUrl+'account/register',model).pipe(map((user: User) => {
			if (user) {
				localStorage.setItem("user", JSON.stringify(user));
				this.currentUserSource.next(user);
			}
			return user;
		}));
	}
	setCurrentUser(user:User)
	{
		this.currentUserSource.next(user);
	}
	logout(){
		localStorage.removeItem("user");
		this.currentUserSource.next(null);
	}
}
