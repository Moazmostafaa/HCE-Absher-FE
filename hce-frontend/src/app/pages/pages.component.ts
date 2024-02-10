import { Component, OnInit } from "@angular/core";
import { NbAuthJWTToken, NbAuthService } from "@nebular/auth";
import { resolve } from "dns";
import { UserToken } from "../models/User-Token.model";

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
export class PagesComponent implements OnInit {
  constructor(private authService: NbAuthService) {}
  menu = MENU_ITEMS;

  ngOnInit() {
    this.authService.getToken().subscribe((result) => {
      let userToken: UserToken = {
        userId: result.getPayload().sub,
        role: result.getPayload().role,
        token: result.getValue(),
        email: result.getPayload().email,
        given_name: result.getPayload().given_name,
        profile_image: result.getPayload().profile_image,
      };
      sessionStorage.setItem("user-token", JSON.stringify(userToken));
    });
  }
}
