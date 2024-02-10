import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { DashboardStatisticsComponent } from "./dashboard-statistics/dashboard-statistics.component";

const routes: Routes = [
  {
    path: "",
    component: DashboardStatisticsComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class StatistcsDashboardRoutingModule {}
