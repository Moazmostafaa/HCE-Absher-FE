import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CreateCityComponent } from './create-city/create-city.component';
import { DetailsCityComponent } from './details-city/details-city.component';
import { ListCityComponent } from './list-city/list-city.component';
import { UpdateCityComponent } from './update-city/update-city.component';


const routes: Routes = [
  {
    path: "",
    component: ListCityComponent,
  },
  {
    path: "details/:id",
    component: DetailsCityComponent,
  },
  {
    path: "create",
    component: CreateCityComponent,
  },
  {
    path: "update/:id",
    component: UpdateCityComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CityRoutingModule { }
