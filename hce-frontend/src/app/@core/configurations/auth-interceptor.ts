import { Injectable } from "@angular/core";

import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent,
} from "@angular/common/http";

import { Router } from "@angular/router";

import { Observable, throwError } from "rxjs";
import { catchError, throwIfEmpty } from "rxjs/operators";
import { HttpErrorResponse } from "@angular/common/http";
import { NbToastrService } from "@nebular/theme";
import { NbAuthService } from "@nebular/auth";
@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(
    private _router: Router,
    private toastr: NbToastrService,
    private nbAuthService: NbAuthService
  ) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    let token = JSON.parse(localStorage.getItem("auth_app_token"));
    if (token) {
      req = req.clone({
        setHeaders: {
          Authorization: "Bearer " + token.value,
        },
      });
    }

    return next.handle(req).pipe(
      catchError((error: HttpErrorResponse) => {
        let errorMsg = "";
        if (error.status == 401 || error.error?.status == 401) {
          console.log("unauthorized");
          this.nbAuthService.logout("email");
            this._router.navigate(["/auth/login"]).finally(() => {
              this.toastr.danger("Unauthorized", "Error");
            });
        } else if (error.status == 404 || error.error?.status == 404 ) {
          console.log("not found");
          errorMsg = `Not found`;
          this.toastr.danger(errorMsg, "Error");
        }else {
          console.log("this is server side error");
          errorMsg = error.error?.message || error.error?.error || error.message || "Error";
          this.toastr.danger(errorMsg, "Error");
        }
        return throwError(errorMsg);
      })
    );
  }
}
