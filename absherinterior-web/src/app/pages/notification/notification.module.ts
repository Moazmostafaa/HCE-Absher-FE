import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NotificationRoutingModule } from './notification-routing.module';
import { ListNotificationsComponent } from './list-notifications/list-notifications.component';
import { SendNotificationComponent } from './send-notification/send-notification.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { NbInputModule, NbCardModule, NbButtonModule, NbActionsModule, NbFormFieldModule, NbUserModule, NbCheckboxModule, NbRadioModule, NbDatepickerModule, NbSelectModule, NbIconModule, NbToggleModule } from '@nebular/theme';
import { Ng2TelInputModule } from 'ng2-tel-input';
import { ThemeModule } from '../../@theme/theme.module';
import { SharedModule } from '../../shared/components/shared.module';
import { PipeModule } from '../../shared/pipes/pipes.module';


@NgModule({
  declarations: [ListNotificationsComponent, SendNotificationComponent],
  imports: [
    CommonModule,
    NotificationRoutingModule,
    ThemeModule,
    NbInputModule,
    NbCardModule,
    NbButtonModule,
    NbActionsModule,
    NbFormFieldModule,
    NbUserModule,
    NbCheckboxModule,
    NbRadioModule,
    NbDatepickerModule,
    NbSelectModule,
    NbIconModule,
    FormsModule,
    MatButtonModule,
    MatIconModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    ReactiveFormsModule,
    Ng2TelInputModule,
    SharedModule,
    PipeModule.forRoot(),
    NbToggleModule,
  ]
})
export class NotificationModule { }
