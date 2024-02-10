import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CreateCoreTypeComponent } from './create-core-type/create-core-type.component';
import { DetailsCoreTypeComponent } from './details-core-type/details-core-type.component';
import { ListCoreTypeComponent } from './list-core-type/list-core-type.component';
import { UpdateCoreTypeComponent } from './update-core-type/update-core-type.component';

const routes: Routes = [
  {
    path: "",
    component: ListCoreTypeComponent,
  },
  {
    path: "details/:id",
    component: DetailsCoreTypeComponent,
  },
  {
    path: "create",
    component: CreateCoreTypeComponent,
  },
  {
    path: "update/:id",
    component: UpdateCoreTypeComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CoreTypeRoutingModule { }
