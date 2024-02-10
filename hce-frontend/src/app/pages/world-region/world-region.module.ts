import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { WorldRegionRoutingModule } from './world-region-routing.module';
import { CreateWorldRegionComponent } from './create-world-region/create-world-region.component';
import { UpdateWorldRegionComponent } from './update-world-region/update-world-region.component';
import { DetailsWorldRegionComponent } from './details-world-region/details-world-region.component';
import { ListWorldRegionComponent } from './list-world-region/list-world-region.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { NbInputModule, NbCardModule, NbButtonModule, NbActionsModule, NbFormFieldModule, NbIconModule } from '@nebular/theme';
import { ThemeModule } from '../../@theme/theme.module';
import { SharedModule } from '../../shared/components/shared.module';
import { PipeModule } from '../../shared/pipes/pipes.module';


@NgModule({
  declarations: [CreateWorldRegionComponent, UpdateWorldRegionComponent, DetailsWorldRegionComponent, ListWorldRegionComponent],
  imports: [
    CommonModule,
    WorldRegionRoutingModule,
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
  ]
})
export class WorldRegionModule { }
