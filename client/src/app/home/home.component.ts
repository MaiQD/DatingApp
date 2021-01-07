import { animate, style, transition, trigger } from '@angular/animations';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  animations: [
    trigger('slideInOut', [
      transition(':enter', [
        style({transform: 'translateY(-100%)'}),
        animate('300ms ease-in', style({transform: 'translateY(0%)'}))
      ]),
      transition(':leave', [
        animate('300ms ease-in', style({transform: 'translateY(-100%)'}))
      ])
    ])
  ]
})
export class HomeComponent implements OnInit {
  registerMode: boolean = false;
  users: any;
  constructor(private http: HttpClient) {  }
  ngOnInit(): void {
    this.getUsers();
  }
  registerToggle()
  {
    this.registerMode = !this.registerMode;
    // console.log(this.registerMode)
  }
  getUsers(){
    this.http.get('https://localhost:44356/api/Users/').subscribe(users=>this.users= users);
    console.log(this.users);
  }
}
