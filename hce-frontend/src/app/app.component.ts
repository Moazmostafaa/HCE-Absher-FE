import { Component, OnInit } from "@angular/core";
import { NbIconLibraries, NbMenuService } from "@nebular/theme";
import { AnalyticsService } from "./@core/utils/analytics.service";
import { SeoService } from "./@core/utils/seo.service";

@Component({
  selector: "ngx-app",
  template: "<router-outlet></router-outlet>",
})
export class AppComponent implements OnInit {
  constructor(
    private analytics: AnalyticsService,
    private seoService: SeoService,
    private menuService: NbMenuService,
    private iconLibraries: NbIconLibraries
  ) {
    this.menuService.onItemClick().subscribe((event) => {
      this.onContecxtItemSelection(event.item.title);
    });
    this.iconLibraries.registerFontPack('font-awesome', { packClass: 'fa' });
    this.iconLibraries.registerFontPack('font-awesome', { packClass: 'fas' });
  }

  ngOnInit(): void {
    this.analytics.trackPageViews();
    this.seoService.trackCanonicalChanges();
  }
  onContecxtItemSelection(title) {
    // console.log("click", title);
  }
}
