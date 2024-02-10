import { Component, OnInit } from "@angular/core";
import { NbResetPasswordComponent } from "@nebular/auth";

@Component({
  selector: "ngx-change-password",
  templateUrl: "./change-password.component.html",
  styleUrls: ["./change-password.component.scss"],
})
export class ChangePasswordComponent extends NbResetPasswordComponent {}
