import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatButtonModule } from "@angular/material/button";
import { MatIconModule } from "@angular/material/icon";
import { MatPaginatorModule } from "@angular/material/paginator";
import { MatSortModule } from "@angular/material/sort";
import { MatTableModule } from "@angular/material/table";
import {
  NbInputModule,
  NbCardModule,
  NbButtonModule,
  NbActionsModule,
  NbUserModule,
  NbCheckboxModule,
  NbRadioModule,
  NbDatepickerModule,
  NbSelectModule,
  NbIconModule,
  NbFormFieldModule,
  NbToggleModule,
  NbTabsetModule,
  NbAutocompleteModule,
  NbSpinnerModule,
} from "@nebular/theme";
import { ThemeModule } from "../../@theme/theme.module";
import { SharedModule } from "../../shared/components/shared.module";
import { Ng2TelInputModule } from "ng2-tel-input";
import { EventRoutingModule } from "./event-routing.module";
import { ListEventsComponent } from "./list-events/list-events.component";
import { AddEventComponent } from "./add-event/add-event.component";
import { EditEventComponent } from './edit-event/edit-event.component';
import { ViewEventComponent } from './view-event/view-event.component';
import { AttendancePendingRequestsComponent } from './attendance-pending-requests/attendance-pending-requests.component';
import { AttendanceListComponent } from './attendance-list/attendance-list.component';
import { AttendanceSentRequestComponent } from './attendance-sent-request/attendance-sent-request.component';
import { AcceptedRequestsComponent } from './accepted-requests/accepted-requests.component';
import { DirectivesModule } from "../../shared/directives/directives.module";
import { PipeModule } from "../../shared/pipes/pipes.module";
import { RejectedRequestsComponent } from './rejected-requests/rejected-requests.component';

@NgModule({
  declarations: [ListEventsComponent , AddEventComponent, EditEventComponent, ViewEventComponent, AttendancePendingRequestsComponent, AttendanceListComponent, AttendanceSentRequestComponent, AcceptedRequestsComponent, RejectedRequestsComponent],
  imports: [
    CommonModule,
   EventRoutingModule,
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
    NbToggleModule,
    NbTabsetModule,
    NbSpinnerModule,
    PipeModule.forRoot(),
    DirectivesModule.forRoot(),
    NbAutocompleteModule
  ],
})
export class EventModule {}
