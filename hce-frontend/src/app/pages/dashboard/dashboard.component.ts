import { Component, OnInit } from '@angular/core';
import { NbMenuItem } from '@nebular/theme';

@Component({
  selector: 'ngx-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  constructor() { }
  currentDate = new Date(new Date().valueOf() - 1000 * 60 * 60 * 24);
  fromDate = new Date(new Date().valueOf() - 1000 * 60 * 60 * 24);
  toDate = new Date(new Date().valueOf() - 1000 * 60 * 60 * 24);
  selectedItem = '0';
  
  
  ngOnInit(): void {
  }

}
