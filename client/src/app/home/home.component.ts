import { animate, style, transition, trigger } from '@angular/animations';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  animations: [
    trigger('slideInOut', [
      transition(':enter', [
        style({ transform: 'translateY(-100%)' }),
        animate('300ms ease-in', style({ transform: 'translateY(0%)' }))
      ]),
      transition(':leave', [
        animate('300ms ease-in', style({ transform: 'translateY(-100%)' }))
      ])
    ])
  ]
})
export class HomeComponent implements OnInit {
  registerMode: boolean = false;
  users: any;
  constructor() { }
  ngOnInit(): void {
  }
  registerToggle() {
    this.registerMode = !this.registerMode;
    // console.log(this.registerMode)
  }
  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }
}
