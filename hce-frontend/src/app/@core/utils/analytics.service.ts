import { Injectable } from "@angular/core";
import { NavigationEnd, Router } from "@angular/router";
import { Location } from "@angular/common";
import { filter } from "rxjs/operators";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { EndPoints } from "../../services/EndPoints";

declare const ga: any;

@Injectable()
export class AnalyticsService {
  private enabled: boolean;

  constructor(
    private location: Location,
    private router: Router,
    private http: HttpClient
  ) {
    this.enabled = false;
  }

  trackPageViews() {
    if (this.enabled) {
      this.router.events
        .pipe(filter((event) => event instanceof NavigationEnd))
        .subscribe(() => {
          ga("send", { hitType: "pageview", page: this.location.path() });
        });
    }
  }

  trackEvent(eventName: string) {
    if (this.enabled) {
      ga("send", "event", eventName);
    }
  }
  getAllStatiscs(statistcsParams): Observable<any> {
    return this.http.get(
      EndPoints.baseUrl + "SuperAdmin/GetDashboardStatistics",
      {
        params: statistcsParams,
      }
    );
  }
}
