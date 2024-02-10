import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CreateStateRegionComponent } from './create-state-region/create-state-region.component';
import { DetailsStateRegionComponent } from './details-state-region/details-state-region.component';
import { ListStateRegionComponent } from './list-state-region/list-state-region.component';
import { UpdateStateRegionComponent } from './update-state-region/update-state-region.component';

const routes: Routes = [
  {
    path: "",
    component: ListStateRegionComponent,
  },
  {
    path: "details/:id",
    component: DetailsStateRegionComponent,
  },
  {
    path: "create",
    component: CreateStateRegionComponent,
  },
  {
    path: "update/:id",
    component: UpdateStateRegionComponent,
  },
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StateRegionRoutingModule { }
