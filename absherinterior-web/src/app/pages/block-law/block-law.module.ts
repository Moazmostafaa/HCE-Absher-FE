import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BlockLawRoutingModule } from './block-law-routing.module';
import { ListBlockLawsComponent } from './list-block-laws/list-block-laws.component';
import { AddBlockLawComponent } from './add-block-law/add-block-law.component';
import { EditBlockLawComponent } from './edit-block-law/edit-block-law.component';
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
import { TranslateModule } from '@ngx-translate/core';


@NgModule({
  declarations: [ListBlockLawsComponent, AddBlockLawComponent, EditBlockLawComponent],
  imports: [
    CommonModule,
    BlockLawRoutingModule,
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
    TranslateModule,
    PipeModule.forRoot(),
  ]
})
export class BlockLawModule { }
