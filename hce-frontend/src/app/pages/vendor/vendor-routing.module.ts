import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { CreateVendorComponent } from "./create-vendor/create-vendor.component";
import { DetailsVendorComponent } from "./details-vendor/details-vendor.component";
import { ListVendorComponent } from "./list-vendor/list-vendor.component";
import { UpdateVendorComponent } from "./update-vendor/update-vendor.component";


const routes: Routes = [
  {
    path: "",
    component: ListVendorComponent,
  },
  {
    path: "details/:id",
    component: DetailsVendorComponent,
  },
  {
    path: "create",
    component: CreateVendorComponent,
  },
  {
    path: "update/:id",
    component: UpdateVendorComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class VendorRoutingModule {}
