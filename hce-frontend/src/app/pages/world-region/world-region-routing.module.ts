import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CreateWorldRegionComponent } from './create-world-region/create-world-region.component';
import { DetailsWorldRegionComponent } from './details-world-region/details-world-region.component';
import { ListWorldRegionComponent } from './list-world-region/list-world-region.component';
import { UpdateWorldRegionComponent } from './update-world-region/update-world-region.component';

const routes: Routes = [
  {
    path: "",
    component: ListWorldRegionComponent,
  },
  {
    path: "details/:id",
    component: DetailsWorldRegionComponent,
  },
  {
    path: "create",
    component: CreateWorldRegionComponent,
  },
  {
    path: "update/:id",
    component: UpdateWorldRegionComponent,
  },
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class WorldRegionRoutingModule { }
