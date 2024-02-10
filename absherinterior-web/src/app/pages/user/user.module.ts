import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { UserRoutingModule } from "./user-routing.module";
import { ListUsersComponent } from "./list-users/list-users.component";
import { AddUserComponent } from "./add-user/add-user.component";
import { EditUserComponent } from "./edit-user/edit-user.component";
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
  NbAlertModule,
} from "@nebular/theme";
import { ThemeModule } from "../../@theme/theme.module";
import { SharedModule } from "../../shared/components/shared.module";
import { Ng2TelInputModule } from "ng2-tel-input";
import { PipeModule } from "../../shared/pipes/pipes.module";

@NgModule({
  declarations: [ListUsersComponent, AddUserComponent, EditUserComponent],
  imports: [
    CommonModule,
    UserRoutingModule,
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
    NbAlertModule,
    PipeModule.forRoot(),
  ],
})
export class UserModule {}
