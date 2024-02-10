import { NgModule } from "@angular/core";
import { NbMenuModule } from "@nebular/theme";
import { ThemeModule } from "../@theme/theme.module";
import { PagesComponent } from "./pages.component";
import { PagesRoutingModule } from "./pages-routing.module";
import { MiscellaneousModule } from "./miscellaneous/miscellaneous.module";
import { UpdateVendorComponent } from './vendor/update-vendor/update-vendor.component';
import { CreateVendorComponent } from './vendor/create-vendor/create-vendor.component';
import { ListVendorComponent } from './vendor/list-vendor/list-vendor.component';
import { DetailsVendorComponent } from './vendor/details-vendor/details-vendor.component';
import { CreateStateRegionComponent } from './state-region/create-state-region/create-state-region.component';
import { UpdateStateRegionComponent } from './state-region/update-state-region/update-state-region.component';
import { ListStateRegionComponent } from './state-region/list-state-region/list-state-region.component';
import { DetailsCountryComponent } from './country/details-country/details-country.component';
import { CreateCountryComponent } from './country/create-country/create-country.component';
import { UpdateCountryComponent } from './country/update-country/update-country.component';
import { ListCountryComponent } from './country/list-country/list-country.component';


@NgModule({
  imports: [
    PagesRoutingModule,
    ThemeModule,
    NbMenuModule,
    MiscellaneousModule,
  ],
  declarations: [PagesComponent,],
})
export class PagesModule {}
