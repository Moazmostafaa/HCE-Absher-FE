import { Component } from "@angular/core";
import { UserService } from "../services/user.service";

import { MENU_ITEMS } from "./pages-menu";

@Component({
  selector: "ngx-pages",
  styleUrls: ["pages.component.scss"],
  template: `
    <ngx-one-column-layout>
      <nb-menu [items]="menu"></nb-menu>
      <router-outlet></router-outlet>
    </ngx-one-column-layout>
  `,
})
export class PagesComponent {
  menu = MENU_ITEMS;
  userData: any;
  constructor(private userService: UserService) {
    if (
      JSON.parse(localStorage.getItem("auth_app_token")).value &&
      !localStorage.getItem("userData")
    ) {
      this.userService.getProfileData().subscribe((res) => {
        this.userData = res.entity;
        this.userService.setUserState(this.userData);
        localStorage.setItem("userData", JSON.stringify(this.userData));
      });
    } else if (localStorage.getItem("userData")) {
      this.userService.setUserState(
        JSON.parse(localStorage.getItem("userData"))
      );
    }
  }
}
