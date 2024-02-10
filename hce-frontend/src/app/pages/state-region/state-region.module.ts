import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { NbInputModule, NbCardModule, NbButtonModule, NbActionsModule, NbFormFieldModule, NbIconModule, NbSelectModule, NbSpinnerModule } from '@nebular/theme';
import { ThemeModule } from '../../@theme/theme.module';
import { SharedModule } from '../../shared/components/shared.module';
import { PipeModule } from '../../shared/pipes/pipes.module';
import { StateRegionRoutingModule } from './state-region-routing.module';
import { CreateStateRegionComponent } from './create-state-region/create-state-region.component';
import { UpdateStateRegionComponent } from './update-state-region/update-state-region.component';
import { DetailsStateRegionComponent } from './details-state-region/details-state-region.component';
import { ListStateRegionComponent } from './list-state-region/list-state-region.component';


@NgModule({
  declarations: [CreateStateRegionComponent, UpdateStateRegionComponent, DetailsStateRegionComponent, ListStateRegionComponent],
  imports: [
    CommonModule,
    StateRegionRoutingModule,
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
    NbSelectModule,
    NbSpinnerModule,
  ]
})
export class StateRegionModule { }
