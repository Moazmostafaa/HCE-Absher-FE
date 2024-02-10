import { AccessTechnologyRoutingModule } from "./access-technology-routing.module";
import { ListAccessTechnologyComponent } from "./list-access-technology/list-access-technology.component";
import { CreateAccessTechnologyComponent } from "./create-access-technology/create-access-technology.component";
import { UpdateAccessTechnologyComponent } from "./update-access-technology/update-access-technology.component";
import { MatTableModule } from "@angular/material/table";
import { MatIconModule } from "@angular/material/icon";
import {
  NbInputModule,
  NbCardModule,
  NbButtonModule,
  NbActionsModule,
  NbFormFieldModule,
  NbIconModule,
} from "@nebular/theme";
import { ThemeModule } from "../../@theme/theme.module";
import { SharedModule } from "../../shared/components/shared.module";
import { PipeModule } from "../../shared/pipes/pipes.module";
import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatButtonModule } from "@angular/material/button";
import { MatSortModule } from "@angular/material/sort";
import { MatPaginatorModule } from "@angular/material/paginator";
import { DetailsAccessTechnologyComponent } from './details-access-technology/details-access-technology.component';
@NgModule({
  declarations: [
    ListAccessTechnologyComponent,
    CreateAccessTechnologyComponent,
    UpdateAccessTechnologyComponent,
    DetailsAccessTechnologyComponent,
  ],
  imports: [
    CommonModule,
    AccessTechnologyRoutingModule,
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
  ],
})
export class AccessTechnologyModule {}
