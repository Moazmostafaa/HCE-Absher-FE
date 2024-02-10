import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CreateCountryComponent } from './create-country/create-country.component';
import { DetailsCountryComponent } from './details-country/details-country.component';
import { ListCountryComponent } from './list-country/list-country.component';
import { UpdateCountryComponent } from './update-country/update-country.component';
const routes: Routes = [
  {
    path: "",
    component: ListCountryComponent,
  },
  {
    path: "details/:id",
    component: DetailsCountryComponent,
  },
  {
    path: "create",
    component: CreateCountryComponent,
  },
  {
    path: "update/:id",
    component: UpdateCountryComponent,
  },
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CountryRoutingModule { }
