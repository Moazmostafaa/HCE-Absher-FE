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
} from "@nebular/theme";
import { ThemeModule } from "../../@theme/theme.module";
import { SharedModule } from "../../shared/components/shared.module";
import { Ng2TelInputModule } from "ng2-tel-input";
import { ListChatsComponent } from "./list-chats/list-chats.component";
import { ChatRoutingModule } from "./chat-routing.module";
import { MychatsComponent } from './list-chats/mychats/mychats.component';
import { UserschatsComponent } from './list-chats/userschats/userschats.component';
import { GroupschatsComponent } from './list-chats/groupschats/groupschats.component';

@NgModule({
  declarations: [ListChatsComponent, MychatsComponent, UserschatsComponent, GroupschatsComponent],
  imports: [
    CommonModule,
    ChatRoutingModule,
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
  ],
})
export class ChatModule {}
