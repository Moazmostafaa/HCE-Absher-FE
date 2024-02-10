import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { CreateAccessTechnologyComponent } from "./create-access-technology/create-access-technology.component";
import { DetailsAccessTechnologyComponent } from "./details-access-technology/details-access-technology.component";
import { ListAccessTechnologyComponent } from "./list-access-technology/list-access-technology.component";
import { UpdateAccessTechnologyComponent } from "./update-access-technology/update-access-technology.component";

const routes: Routes = [
  {
    path: "",
    component: ListAccessTechnologyComponent,
  },
  {
    path: "details/:id",
    component: DetailsAccessTechnologyComponent,
  },
  {
    path: "create",
    component: CreateAccessTechnologyComponent,
  },
  {
    path: "update/:id",
    component: UpdateAccessTechnologyComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AccessTechnologyRoutingModule {}
