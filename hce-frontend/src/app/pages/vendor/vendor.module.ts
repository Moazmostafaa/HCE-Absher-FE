
import { MatTableModule } from "@angular/material/table";
import { MatIconModule } from "@angular/material/icon";
import {
  NbInputModule,
  NbCardModule,
  NbButtonModule,
  NbActionsModule,
  NbFormFieldModule,
  NbIconModule,
} from "@nebular/theme";
import { ThemeModule } from "../../@theme/theme.module";
import { SharedModule } from "../../shared/components/shared.module";
import { PipeModule } from "../../shared/pipes/pipes.module";
import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatButtonModule } from "@angular/material/button";
import { MatSortModule } from "@angular/material/sort";
import { MatPaginatorModule } from "@angular/material/paginator";
import { VendorRoutingModule } from "./vendor-routing.module";
import { ListVendorComponent } from "./list-vendor/list-vendor.component";
import { DetailsVendorComponent } from "./details-vendor/details-vendor.component";
import { CreateVendorComponent } from "./create-vendor/create-vendor.component";
import { UpdateVendorComponent } from "./update-vendor/update-vendor.component";

@NgModule({
  declarations: [
   ListVendorComponent,
   DetailsVendorComponent,
   CreateVendorComponent,
   UpdateVendorComponent
  ],
  imports: [
    CommonModule,
    VendorRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    ThemeModule,
    NbInputModule,
    NbCardModule,
    NbButtonModule,
    NbActionsModule,
    NbFormFieldModule,
    NbIconModule,
    MatButtonModule,
    MatIconModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    SharedModule,
    PipeModule.forRoot(),
  ],
})
export class VendorModule {}
