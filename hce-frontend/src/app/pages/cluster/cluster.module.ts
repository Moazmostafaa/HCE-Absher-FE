import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ClusterRoutingModule } from './cluster-routing.module';
import { CreateClusterComponent } from './create-cluster/create-cluster.component';
import { UpdateClusterComponent } from './update-cluster/update-cluster.component';
import { DetailsClusterComponent } from './details-cluster/details-cluster.component';
import { ListClusterComponent } from './list-cluster/list-cluster.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { NbInputModule, NbCardModule, NbButtonModule, NbActionsModule, NbFormFieldModule, NbIconModule, NbSpinnerModule, NbSelectModule } from '@nebular/theme';
import { ThemeModule } from '../../@theme/theme.module';
import { SharedModule } from '../../shared/components/shared.module';
import { PipeModule } from '../../shared/pipes/pipes.module';


@NgModule({
  declarations: [CreateClusterComponent, UpdateClusterComponent, DetailsClusterComponent, ListClusterComponent],
  imports: [
    CommonModule,
    ClusterRoutingModule,
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
    NbSpinnerModule,
    PipeModule.forRoot(),
    NbSelectModule
  ]
})
export class ClusterModule { }
