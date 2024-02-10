import { RouterModule, Routes } from "@angular/router";
import { NgModule } from "@angular/core";

import { PagesComponent } from "./pages.component";
import { DashboardComponent } from "./dashboard/dashboard.component";
import { NotFoundComponent } from "./miscellaneous/not-found/not-found.component";

const routes: Routes = [
  {
    path: "",
    component: PagesComponent,
    children: [
      {
        path: "dashboard",
        loadChildren: () =>
          import("./dashboard/dashboard.module").then((m) => m.DashboardModule),
      },
      {
        path: "miscellaneous",
        loadChildren: () =>
          import("./miscellaneous/miscellaneous.module").then(
            (m) => m.MiscellaneousModule
          ),
      },
      {
        path: "access-technology",
        loadChildren: () =>
          import("./access-technology/access-technology.module").then(
            (m) => m.AccessTechnologyModule
          ),
      },
      {
        path: "goal",
        loadChildren: () =>
          import("./goal/goal.module").then(
            (m) => m.GoalModule
          ),
      },
      {
        path: "vendor",
        loadChildren: () =>
          import("./vendor/vendor.module").then(
            (m) => m.VendorModule
          ),
      },
      {
        path: "core-type",
        loadChildren: () =>
          import("./core-type/core-type.module").then(
            (m) => m.CoreTypeModule
          ),
      },
      {
        path: "world-region",
        loadChildren: () =>
          import("./world-region/world-region.module").then(
            (m) => m.WorldRegionModule
          ),
      },
      {
        path: "state-region",
        loadChildren: () =>
          import("./state-region/state-region.module").then(
            (m) => m.StateRegionModule
          ),
      },
      {
        path: "country",
        loadChildren: () =>
          import("./country/country.module").then(
            (m) => m.CountryModule
          ),
      },
      {
        path: "city",
        loadChildren: () =>
          import("./city/city.module").then(
            (m) => m.CityModule
          ),
      },
      {
        path: "district",
        loadChildren: () =>
          import("./district/district.module").then(
            (m) => m.DistrictModule
          ),
      },
      {
        path: "cluster",
        loadChildren: () =>
          import("./cluster/cluster.module").then(
            (m) => m.ClusterModule
          ),
      },
      {
        path: "",
        redirectTo: "dashboard",
        pathMatch: "full",
      },
      {
        path: "**",
        component: NotFoundComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PagesRoutingModule {}
