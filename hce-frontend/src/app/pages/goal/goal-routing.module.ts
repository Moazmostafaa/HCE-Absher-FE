import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { CreateGoalComponent } from "./create-goal/create-goal.component";
import { DetailsGoalComponent } from "./details-goal/details-goal.component";
import { ListGoalComponent } from "./list-goal/list-goal.component";
import { UpdateGoalComponent } from "./update-goal/update-goal.component";

const routes: Routes = [
  {
    path: "",
    component: ListGoalComponent,
  },
  {
    path: "details/:id",
    component: DetailsGoalComponent,
  },
  {
    path: "create",
    component: CreateGoalComponent,
  },
  {
    path: "update/:id",
    component: UpdateGoalComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class GoalRoutingModule {}
