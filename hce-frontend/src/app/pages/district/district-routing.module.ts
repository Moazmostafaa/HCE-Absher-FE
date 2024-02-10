import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CreateDistrictComponent } from './create-district/create-district.component';
import { DetailsDistrictComponent } from './details-district/details-district.component';
import { ListDistrictComponent } from './list-district/list-district.component';
import { UpdateDistrictComponent } from './update-district/update-district.component';

const routes: Routes = [
  {
    path: "",
    component: ListDistrictComponent,
  },
  {
    path: "details/:id",
    component: DetailsDistrictComponent,
  },
  {
    path: "create",
    component: CreateDistrictComponent,
  },
  {
    path: "update/:id",
    component: UpdateDistrictComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DistrictRoutingModule { }
