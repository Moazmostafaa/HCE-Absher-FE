import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { NbInputModule, NbCardModule, NbButtonModule, NbActionsModule, NbFormFieldModule, NbIconModule, NbSelectModule } from '@nebular/theme';
import { ThemeModule } from '../../@theme/theme.module';
import { SharedModule } from '../../shared/components/shared.module';
import { PipeModule } from '../../shared/pipes/pipes.module';
import { CreateCountryComponent } from './create-country/create-country.component';
import { UpdateCountryComponent } from './update-country/update-country.component';
import { DetailsCountryComponent } from './details-country/details-country.component';
import { ListCountryComponent } from './list-country/list-country.component';
import { CountryRoutingModule } from './country-routing.module';


@NgModule({
  declarations: [CreateCountryComponent, UpdateCountryComponent, DetailsCountryComponent, ListCountryComponent],
  imports: [
    CommonModule,
    CountryRoutingModule,
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
    NbSelectModule
  ]
})
export class CountryModule { }
