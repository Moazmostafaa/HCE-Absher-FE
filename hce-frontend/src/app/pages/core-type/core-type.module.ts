import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CoreTypeRoutingModule } from './core-type-routing.module';
import { CreateCoreTypeComponent } from './create-core-type/create-core-type.component';
import { DetailsCoreTypeComponent } from './details-core-type/details-core-type.component';
import { ListCoreTypeComponent } from './list-core-type/list-core-type.component';
import { UpdateCoreTypeComponent } from './update-core-type/update-core-type.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ThemeModule } from '../../@theme/theme.module';
import { NbActionsModule, NbButtonModule, NbCardModule, NbFormFieldModule, NbIconModule, NbInputModule } from '@nebular/theme';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginatorModule } from '@angular/material/paginator';
import { SharedModule } from '../../shared/components/shared.module';
import { PipeModule } from '../../shared/pipes/pipes.module';


@NgModule({
  declarations: [CreateCoreTypeComponent, DetailsCoreTypeComponent, ListCoreTypeComponent, UpdateCoreTypeComponent],
  imports: [
    CommonModule,
    CoreTypeRoutingModule,
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
export class CoreTypeModule { }
