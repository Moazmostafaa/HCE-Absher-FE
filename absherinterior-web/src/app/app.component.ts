import { Component, OnInit } from "@angular/core";
import { NbIconLibraries, NbMenuService } from "@nebular/theme";
import { TranslateService } from "@ngx-translate/core";
import { AnalyticsService } from "./@core/utils/analytics.service";
import { SeoService } from "./@core/utils/seo.service";

@Component({
  selector: "ngx-app",
  template: "<router-outlet></router-outlet>",
})
export class AppComponent implements OnInit {
  currentLang = localStorage.getItem("language") || "ar";
  readonly html = document.getElementsByTagName("html")[0];
  constructor(
    private analytics: AnalyticsService,
    private seoService: SeoService,
    private menuService: NbMenuService,
    private iconLibraries: NbIconLibraries,
    private translate: TranslateService
  ) {
    if (!localStorage.getItem("language")) {
      localStorage.setItem("language", "ar");
    }

    this.html.setAttribute("lang", this.currentLang);
    translate.use(this.currentLang);

    this.menuService.onItemClick().subscribe((event) => {
      this.onContecxtItemSelection(event.item.title);
    });
    this.iconLibraries.registerFontPack("font-awesome", { packClass: "fa" });
    this.iconLibraries.registerFontPack("font-awesome", { packClass: "fas" });
  }

  ngOnInit(): void {
    this.analytics.trackPageViews();
    this.seoService.trackCanonicalChanges();
  }
  onContecxtItemSelection(title) {
    // console.log("click", title);
  }
}
