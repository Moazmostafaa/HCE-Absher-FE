import { Component, OnDestroy, OnInit } from "@angular/core";
import {
  NbContextMenuContext,
  NbMediaBreakpointsService,
  NbMenuItem,
  NbMenuService,
  NbSidebarService,
  NbThemeService,
} from "@nebular/theme";

import { UserData } from "../../../@core/data/users";
import { LayoutService } from "../../../@core/utils";
import { map, takeUntil } from "rxjs/operators";
import { Subject } from "rxjs";
import { NbAuthJWTToken, NbAuthService } from "@nebular/auth";
import { LanguageService } from "../../../services/language.service";
import { LookupsService } from "../../../services/lookups.service";
import { Base64ImagePipe } from "../../../shared/pipes/base64-image.pipe";

@Component({
  selector: "ngx-header",
  styleUrls: ["./header.component.scss"],
  templateUrl: "./header.component.html",
  providers: [Base64ImagePipe],
})
export class HeaderComponent implements OnInit, OnDestroy {
  private destroy$: Subject<void> = new Subject<void>();
  userPictureOnly: boolean = false;
  user: any;
  languages = ["ar", "en"];
  currentTheme = "default";
  currentLang = localStorage.getItem("language") || "ar";
  image = "";
  userMenu: NbMenuItem[] = [
    { title: "Profile", icon: "person-outline", link: "/pages/profile" },
    {
      title: "Reset Password",
      icon: "refresh-outline",
      link: "/auth/reset-password",
    },
    { title: "Log out", icon: "log-out-outline", link: "/auth/logout" },
  ];

  constructor(
    private sidebarService: NbSidebarService,
    private menuService: NbMenuService,
    private layoutService: LayoutService,
    private authService: NbAuthService,
    private lang: LanguageService,
    private lookupSerivce: LookupsService,
    private base64ImagePipe: Base64ImagePipe
  ) {
    this.authService.onTokenChange().subscribe((token: NbAuthJWTToken) => {
      if (token.isValid()) {
        this.user = token.getPayload(); // here we receive a payload from the token and assigns it to our `user` variable
      }
    });
  }

  ngOnInit() {
    if (this.user?.profile_image) {
      this.lookupSerivce
        .getAttachmentById(this.user.profile_image)
        .subscribe((res: any) => {
          this.image = this.base64ImagePipe.transform(
            res.entity.fileData,
            res.entity.extention
          );
        });
    }
  }
  changeLang(lang) {
    this.currentLang = lang;
    this.lang.changeLange();
    location.reload();
  }
  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
  }
  toggleSidebar(): boolean {
    this.sidebarService.toggle(true, "menu-sidebar");
    this.layoutService.changeLayoutSize();

    return false;
  }

  navigateHome() {
    this.menuService.navigateHome();
    return false;
  }
}
